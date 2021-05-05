using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReflectionForms
{
	public class EntityFormController
	{
		private DbContext Context;
		private static EntityFormController _instance;
		public static EntityFormController Instance
		{
			get
			{
				if (_instance == null)
				{
					throw new Exception("First you need to initialize the class");
				}
				else
				{
					return _instance;
				}
			}
			private set
			{
				_instance = value;
			}
		}
		private EntityFormController(DbContext context)
		{
			Context = context;
		}
		public static void Initialize(DbContext context)
		{
			_instance = new EntityFormController(context);
		}
		public EntityForm<TEntity> GetForm<TEntity>(params Privileges[] privileges) where TEntity : class
		{
			return new EntityForm<TEntity>(this, privileges);
		}
		public List<TEntity> GetAll<TEntity>() where TEntity : class
		{
			return Context.Set<TEntity>().AsQueryable().IncludeReferences().ToList();
		}
		public List<object> GetAll(Type type)
		{
			return Context.Set(type).AsQueryable().IncludeReferences().ToListAsync().Result;
		}
		public void Add<TEntity>(TEntity entity) where TEntity : class
		{
			Context.Set<TEntity>().Add(entity);
		}
		public void Remove<TEntity>(TEntity entity) where TEntity : class
		{
			Context.Set<TEntity>().Remove(entity);
		}
		public void Save()
		{
			Context.SaveChanges();
		}
	}

}
