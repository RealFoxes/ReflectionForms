using ReflectionForms.EntitiesForms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionForms.Entities
{
	public class Class
	{
		[Key, ReflFormName("ЫД")]
		public int Id { get; set; }
		public int MyProperty { get; set; }
		public List<SomeEntity> SomeEntities { get; set; }
		public Class()
		{
			SomeEntities = new List<SomeEntity>();
		}
	}
}
