using System;
using System.Collections;

namespace ReflectionForms
{
	public abstract class ReflEntity<T> where T : class
	{
		protected static IEnumerable GetEntities => throw new NotImplementedException();
		protected static void EditEntity(T oldItem, T newItem) => throw new NotImplementedException();
		protected static void DeleteEntity(T item) => throw new NotImplementedException();
		protected static void AddEntity(T item) => throw new NotImplementedException();
	}
}
