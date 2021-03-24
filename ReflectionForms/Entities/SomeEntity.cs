using ReflectionForms.EntitiesForms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionForms.Entities
{
	public class SomeEntity : ReflEntity<SomeEntity>
	{
		[Key,ReflFormName("12111")] //Колонка в таблице / Лейбл над тексто боксом или другим контролом
		public int SomeId { get; set; }
		public int SomeProp { get; set; }
		[ReflFormRef("MyProperty")]
		public Class SomeRef { get; set; }
		[ReflFormNotVisible]
		public SomeEnum someEnum { get; set; }
		public DateTime SomeDate { get; set; }
		public string SomeString { get; set; }
		public char c { get; set; }
		public SomeEntity()
		{
		
		}

		public new static List<SomeEntity> GetEntities()
		{
			return Form1.someEntities;
		}
		public new static void DeleteEntity(SomeEntity someEntity)
		{
			Form1.someEntities.Remove(someEntity);
		}
	}
	public enum SomeEnum
	{
		SomeEnum1,
		SomeEnum2
	}
}
