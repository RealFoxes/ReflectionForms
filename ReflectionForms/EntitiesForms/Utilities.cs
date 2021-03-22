using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionForms.EntitiesForms
{
	public static class Utilities
	{
		public static string GetColumnName(PropertyInfo property)
		{
			string columnName;
			var att = property.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormRef");
			if (att != null)
			{
				var propFromRef = property.PropertyType.GetProperties().FirstOrDefault(p => p.Name == att.ConstructorArguments[0].Value.ToString());
				columnName = propFromRef.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormName")?.ConstructorArguments[0].Value.ToString()
								 ?? propFromRef.Name;
			}
			else
			{
				columnName = property.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormName")?.ConstructorArguments[0].Value.ToString() ?? property.Name;
			}

			return columnName;
		}
		public static object GetValue<Entity>(PropertyInfo property, Entity entity)
		{
			object value;
			var att = property.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormRef");
			if (att != null)
			{
				var propFromRef = property.PropertyType.GetProperties().FirstOrDefault(p => p.Name == att.ConstructorArguments[0].Value.ToString());
				value = propFromRef.GetValue(property.GetValue(entity));
			}
			else
			{
				value = property.GetValue(entity);
			}
			return value;
		}
	}
}
