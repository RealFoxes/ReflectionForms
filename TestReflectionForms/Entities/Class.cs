using ReflectionForms;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestReflectionForms.Entities
{
	public class Class : ReflEntity<Class>
	{
		[Key, ReflFormName("ЫД")]
		public int Id { get; set; }

		[ReflFormName("Свойство")]
		public int MyProperty { get; set; }

		public List<SomeEntity> SomeEntities { get; set; }

		public Class()
		{
			SomeEntities = new List<SomeEntity>();
		}
	}
}
