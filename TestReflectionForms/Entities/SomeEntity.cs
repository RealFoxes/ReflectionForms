using ReflectionForms;
using System;
using System.ComponentModel.DataAnnotations;

namespace TestReflectionForms.Entities
{
	public class SomeEntity
	{
		public enum SomeEnum
		{
			[ReflFormName("НекийПеречеслитель1")]
			SomeEnum1,
			[ReflFormName("НекийПеречеслитель2")]
			SomeEnum2
		}
		[Key, ReflFormName("Тест")] //Колонка в таблице / Лейбл над тексто боксом или другим контролом
		public int SomeId { get; set; }

		public int SomeProp { get; set; }

		[ReflFormRef("Id")]
		public Class SomeRef { get; set; }

		[ReflFormName("Перечеслители")]
		public SomeEnum someEnum { get; set; }

		[ReflFormName("ДатаЕбать")]
		public DateTime SomeDate { get; set; }

		public string SomeString { get; set; }

		public SomeEntity()
		{

		}
	}
}
