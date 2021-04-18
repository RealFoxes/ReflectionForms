using MySql.Data.EntityFramework;
using ReflectionForms.Entities;
using System;
using System.Data.Entity;
using System.Linq;

namespace ReflectionForms
{
	[DbConfigurationType(typeof(MySqlEFConfiguration))]
	public class ModelDatabase : DbContext
	{
		public ModelDatabase()
			: base("server=localhost;port=3306;user id=root;password=root;characterset=utf8;database=refrectionforms")
		{
		}

		public virtual DbSet<SomeEntity> SomeEntities { get; set; }
		public virtual DbSet<Class> Classes { get; set; }
	}
}