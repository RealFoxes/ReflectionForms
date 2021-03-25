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
		// Your context has been configured to use a 'ModelDatabase' connection string from your application's 
		// configuration file (App.config or Web.config). By default, this connection string targets the 
		// 'ReflectionForms.ModelDatabase' database on your LocalDb instance. 
		// 
		// If you wish to target a different database and/or database provider, modify the 'ModelDatabase' 
		// connection string in the application configuration file.
		public ModelDatabase()
			: base("server=localhost;port=3306;user id=root;password=root;characterset=utf8;database=refrectionforms")
		{
		}

		// Add a DbSet for each entity type that you want to include in your model. For more information 
		// on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

		public virtual DbSet<SomeEntity> SomeEntities { get; set; }
		public virtual DbSet<Class> Classes { get; set; }
	}

	//public class MyEntity
	//{
	//    public int Id { get; set; }
	//    public string Name { get; set; }
	//}
}