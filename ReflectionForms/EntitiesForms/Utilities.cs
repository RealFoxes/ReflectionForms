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
			var att = property.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(ReflFormRef)); // Try to find reference attribute
			if (att != null)
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
			var att = property.CustomAttributes
				.FirstOrDefault(a => a.AttributeType == typeof(ReflFormRef)); // Try to find reference attribute

			if (att != null)
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
	}
}
