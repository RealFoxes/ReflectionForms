using MySql.Data.EntityFramework;
using System.Data.Entity;
using TestReflectionForms.Entities;

namespace TestReflectionForms
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