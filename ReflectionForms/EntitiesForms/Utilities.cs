using ReflectionForms.EntitiesForms.FieldsForEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
		public static object GetValue<Entity>(PropertyInfo property, Entity entity)
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
					else if (property.IsReference(out CustomAttributeData att))
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
					if (prop.IsReference(out CustomAttributeData att)) // If prop is a reference
					{
						ComboBox comboBox = (ComboBox)itemOfField;
						var ListOfEntities = comboBox.Items.Cast<object>();

						object valueFromEntity = GetValue(prop,entity);
						object valToShow = null;

						foreach (object entityFromComboBox in ListOfEntities)
						{
							var valueFromPropertyFromRef = entityFromComboBox.GetType()
							 .GetProperties()
							 .FirstOrDefault(p => p.Name == att.ConstructorArguments[0].Value.ToString()).GetValue(entityFromComboBox);

							if (valueFromPropertyFromRef.Equals(valueFromEntity))
							{
								valToShow = entityFromComboBox;
							}
						}
						comboBox.SelectedItem = valToShow;
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
		public static string GetDescription<T>(this T e) where T : IConvertible
		{
			if (e is Enum)
			{
				Type type = e.GetType();
				Array values = System.Enum.GetValues(type);

				foreach (int val in values)
				{
					if (val == e.ToInt32(CultureInfo.InvariantCulture))
					{
						var memInfo = type.GetMember(type.GetEnumName(val));
						var descriptionAttribute = memInfo[0]
							.GetCustomAttributes(typeof(DescriptionAttribute), false)
							.FirstOrDefault() as DescriptionAttribute;

						if (descriptionAttribute != null)
						{
							return descriptionAttribute.Description;
						}
					}
				}
			}

			return null; // could also return string.Empty
		}

		public static bool IsReference(this PropertyInfo property, out CustomAttributeData attributeData)
		{
			attributeData = property.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormRef");
			return attributeData != null;
		}
}
}
