using System.Reflection;

namespace ReflectionForms.Fields
{
	interface IField
	{
		void FillField<T>(PropertyInfo property, T instance);
		object GetValue(PropertyInfo property);
	}
}
