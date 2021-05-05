using System.Data.Entity;
using System.Linq;

namespace ReflectionForms
{
	public static class IQueryableExtension
	{
		public static IQueryable<T> IncludeReferences<T>(this IQueryable<T> queryable) where T : class
		{
			var type = typeof(T);
			var properties = type.GetProperties();
			foreach (var property in properties)
			{
				if (property.CustomAttributes.FirstOrDefault(att => att.AttributeType.Name == "ReflFormRef") != null)
				{
					queryable = queryable.Include(property.Name);
				}
			}
			return queryable;
		}
		public static IQueryable IncludeReferences(this IQueryable queryable)
		{
			var type = queryable.ElementType;
			var properties = type.GetProperties();
			foreach (var property in properties)
			{
				if (property.CustomAttributes.FirstOrDefault(att => att.AttributeType.Name == "ReflFormRef") != null)
				{
					queryable = queryable.Include(property.Name);
				}
			}
			return queryable;

		}
	}
}
