using MySql.Data.Entity;
using ReflectionForms.Entities;
using System;
using System.Data.Entity;
using System.Linq;

namespace ReflectionForms
{
	[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
	public class Model : DbContext
	{
		// Your context has been configured to use a 'Model' connection string from your application's 
		// configuration file (App.config or Web.config). By default, this connection string targets the 
		// 'ReflectionForms.Model' database on your LocalDb instance. 
		// 
		// If you wish to target a different database and/or database provider, modify the 'Model' 
		// connection string in the application configuration file.
		public Model()
			: base("name=Model")
		{
		}


		public virtual DbSet<SomeEntity> SomeEntities { get; set; }
		public virtual DbSet<Class> Classes { get; set; }
	}
}