using ReflectionForms.EntitiesForms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionForms.Entities
{
	public class SomeEntity
	{
		[Key,ReflFormName("НекотороеСвойство")] //Колонка в таблице / Лейбл над тексто боксом или другим контролом
		public int SomeId { get; set; }
		public int SomeProp { get; set; }
		[ReflFormRef("Id")]
		public Class SomeRef { get; set; }
		public SomeEnum someEnum { get; set; }
		public DateTime SomeDate { get; set; }
		public string SomeString { get; set; }
		public char c { get; set; }
		public SomeEntity()
		{

		} 
	}
	public enum SomeEnum
	{
		SomeEnum1,
		SomeEnum2
	}
}
