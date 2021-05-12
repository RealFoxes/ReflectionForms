using ReflectionForms.Fields;
using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace ReflectionForms
{
	public static class Utilities
	{
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
		public static void InitializeDefalutFields()
		{
			FieldsController.AddNewTypeField(typeof(DateTime), typeof(DateTimeField));
			FieldsController.AddNewTypeField(typeof(String), typeof(StringField));
			FieldsController.AddNewTypeField(typeof(Int32), typeof(IntField));
			FieldsController.AddNewTypeField(typeof(Enum), typeof(EnumField));
			FieldsController.AddNewTypeField(typeof(ReflFormRef), typeof(ReferenceField));
		}
	}
}
