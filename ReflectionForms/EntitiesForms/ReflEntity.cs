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
			return null;
		}
	}
}
