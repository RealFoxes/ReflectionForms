using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionForms.EntitiesForms
{
	public abstract class ReflEntity<T> where T : class
	{
		protected static List<T> GetEntities()
		{
			throw new NotImplementedException();
		}
		protected static void EditEntity(T item)
		{
			throw new NotImplementedException();
		}
		protected static void DeleteEntity(T item)
		{
			throw new NotImplementedException();
		}
	}
}
