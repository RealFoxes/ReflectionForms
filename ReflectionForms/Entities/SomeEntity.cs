﻿using ReflectionForms.EntitiesForms;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
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
		public new static void EditEntity(SomeEntity someEntity)
		{
			using (var model = new ModelDatabase())
			{
				var toRemove = model.SomeEntities.FirstOrDefault(s => s.SomeId == someEntity.SomeId);
				model.SomeEntities.Remove(toRemove);
				model.SomeEntities.Add(someEntity);
				model.SaveChanges();
			}
		}
		public new static void AddEntity(SomeEntity someEntity)
		{
			using (var model = new ModelDatabase())
			{
				model.SomeEntities.Add(someEntity);
				model.SaveChanges();
			}
		}
	}
	public enum SomeEnum
	{
		[ReflFormName("НекийПеречеслитель1")]
		SomeEnum1,
		[ReflFormName("НекийПеречеслитель2")]
		SomeEnum2
	}
}
