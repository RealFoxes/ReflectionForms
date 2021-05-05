using ReflectionForms.EntitiesForms.FieldsForEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReflectionForms.EntitiesForms
{
	public static class Utilities
	{
		private static Type[] TextBoxFieldTypes = { typeof(string), typeof(Int16), typeof(Int32), typeof(Int64), typeof(byte), typeof(char) };
		public static string GetColumnName(PropertyInfo property)
		{
			string columnName;
			if (property.IsReference(out CustomAttributeData att))
			{
				var propFromRef = property.PropertyType.GetProperties().FirstOrDefault(p => p.Name == att.ConstructorArguments[0].Value.ToString());
				//Getting property in reference

				columnName = propFromRef.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(ReflFormName))?.ConstructorArguments[0].Value.ToString()
								 ?? propFromRef.Name;
				//If attribute ReflFormName does not exist use name from assembly
			}
			else
			{
				columnName = property.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(ReflFormName))?.ConstructorArguments[0].Value.ToString()
					?? property.Name;
				//If attribute ReflFormName does not exist use name from assembly
			}

			return columnName;
		}
		public static object GetValueRef<Entity>(PropertyInfo property, Entity entity)
		{
			object value;

			if (property.IsReference(out CustomAttributeData att))
			{
				var propFromRef = property.PropertyType.GetProperties()
					.FirstOrDefault(p => p.Name == att.ConstructorArguments[0].Value.ToString());

				value = propFromRef.GetValue(property.GetValue(entity));
			}
			else
			{
				value = property.GetValue(entity);
			}
			return value;
		}
		public static void AddFields<T>(Control control)
		{
			foreach (PropertyInfo property in typeof(T).GetProperties().Reverse())
			{
				var propType = property.PropertyType;
				if (property.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormNotVisible") == null)
				{
					if (TextBoxFieldTypes.Contains(propType))
					{
						var uc = new StringAndIntNumbers(property);
						uc.Dock = DockStyle.Top;
						uc.BringToFront();
						control.Controls.Add(uc);
					}
					else if (propType == typeof(DateTime))
					{
						var uc = new DateTimeField(property);
						uc.Dock = DockStyle.Top;
						uc.BringToFront();
						control.Controls.Add(uc);
					}
					else if (property.IsReference(out _))
					{
						var uc = new ReferenceField(property);
						uc.Dock = DockStyle.Top;
						uc.BringToFront();
						control.Controls.Add(uc);
					}
					else if (propType.GetTypeInfo().IsEnum)
					{
						var uc = new EnumField(property);
						uc.Dock = DockStyle.Top;
						uc.BringToFront();
						control.Controls.Add(uc);
					}

				}
			}
		}
		public static void FillFields<T>(Control control, T entity)
		{
			if (!control.HasChildren) throw new ContorlHasNoChildren();

			foreach (Control uc in control.Controls)
			{
				if (!uc.HasChildren) throw new ContorlHasNoChildren();

				//Get currect prop for a UserControl
				var NameOfProp = uc.Tag.ToString();
				NameOfProp = NameOfProp.Remove(0, NameOfProp.LastIndexOf('.') + 1);
				PropertyInfo prop = entity.GetType().GetProperties().FirstOrDefault(p => p.Name == NameOfProp);
				foreach (Control itemOfField in uc.Controls) // Переписать это условие, а именно добавить инстанс листа из вне что бы сохранялись ссылки и не приходилось искать в новом листе по проперти
				{

					if (itemOfField is Label) continue;
					if (itemOfField is DateTimePicker dateTimePicker)
					{
						dateTimePicker.Value = (DateTime)prop.GetValue(entity);
						continue;
					}
					if (prop.IsReference(out _)) // If prop is a reference
					{
						ComboBox comboBox = (ComboBox)itemOfField;
						comboBox.SelectedItem = prop.GetValue(entity);
						continue;
					}
					if (prop.PropertyType.IsEnum)
					{
						((ComboBox)itemOfField).SelectedIndex = (int)prop.GetValue(entity);
						continue;
					}


					itemOfField.Text = prop.GetValue(entity).ToString();
				}
			}
		}

		public static bool IsReference(this PropertyInfo property, out CustomAttributeData attributeData)
		{
			attributeData = property.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormRef");
			return attributeData != null;
		}
		public static void AddRowToDataSource<T>(T entity, DataTable dt)
		{
				DataRow row = dt.NewRow();
				row[0] = entity;
				foreach (PropertyInfo property in typeof(T).GetProperties())
				{
					var columnId = dt.Columns.Cast<DataColumn>()
						.FirstOrDefault(c => c.ColumnName == Utilities.GetColumnName(property)).Ordinal;

					if (property.PropertyType.IsEnum)
					{
						string nameOfEnum = Utilities.GetValueRef(property, entity).ToString();
						FieldInfo field = property.PropertyType.GetField(Utilities.GetValueRef(property, entity).ToString());

						var att = field.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormName");
						if (att != null)
						{
							nameOfEnum = att.ConstructorArguments[0].Value.ToString();
						}

						row[columnId] = nameOfEnum;
					}
					else
					{
						row[columnId] = Utilities.GetValueRef(property, entity);
					}
				}
				dt.Rows.Add(row);
		}
		
		public static T GetEntityFromField<T>(Control control)
		{
			T entity = (T)Activator.CreateInstance(typeof(T));
			foreach (Control uc in control.Controls)
			{
				var NameOfProp = uc.Tag.ToString();
				NameOfProp = NameOfProp.Remove(0, NameOfProp.LastIndexOf('.') + 1);
				PropertyInfo prop = entity.GetType().GetProperties().FirstOrDefault(p => p.Name == NameOfProp);
				var converter = TypeDescriptor.GetConverter(prop.PropertyType);

				foreach (Control ucControl in uc.Controls)
				{
					if (ucControl is Label) continue;

					if (ucControl is DateTimePicker dateTimePicker)
					{

						prop.SetValue(entity, dateTimePicker.Value, null);
						continue;
					}

					if (prop.IsReference(out _))
					{
						prop.SetValue(entity, ((ComboBox)ucControl).SelectedItem, null);
						continue;
					}

					if (prop.PropertyType.IsEnum)
					{
						var _enum = Enum.Parse(prop.PropertyType, ((ComboBox)ucControl).SelectedIndex.ToString());
						prop.SetValue(entity, _enum);
						continue;
					}

					prop.SetValue(entity, converter.ConvertTo(ucControl.Text, prop.PropertyType), null);
				}

			}
			return entity;
		}
	}
}
