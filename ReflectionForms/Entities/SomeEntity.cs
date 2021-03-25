using ReflectionForms.EntitiesForms;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionForms.Entities
{
	public class SomeEntity : ReflEntity<SomeEntity>
	{
		[Key,ReflFormName("Тест")] //Колонка в таблице / Лейбл над тексто боксом или другим контролом
		public int SomeId { get; set; }
		public int SomeProp { get; set; }
		[ReflFormRef("MyProperty")]
		public Class SomeRef { get; set; }
		[ReflFormNotVisible]
		public SomeEnum someEnum { get; set; }
		public DateTime SomeDate { get; set; }
		public string SomeString { get; set; }
		public SomeEntity()
		{
		
		}

		public new static List<SomeEntity> GetEntities()
		{
			using (var model = new ModelDatabase())
			{
				return model.SomeEntities.Include(s => s.SomeRef.SomeEntities).ToList();
			}

		}
		public new static void DeleteEntity(SomeEntity someEntity)
		{
			using (var model = new ModelDatabase())
			{
				var toRemove = model.SomeEntities.FirstOrDefault(s => s.SomeId == someEntity.SomeId);
				model.SomeEntities.Remove(toRemove);
				model.SaveChanges();
			}
		}
	}
	public enum SomeEnum
	{
		SomeEnum1,
		SomeEnum2
	}
}
