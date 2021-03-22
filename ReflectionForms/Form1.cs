using MySql.Data.MySqlClient;
using ReflectionForms.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReflectionForms
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();
			stringBuilder.Server = "localhost";
			stringBuilder.Port = 3306;
			stringBuilder.Database = "RefrectionForms";
			stringBuilder.UserID = "root";
			stringBuilder.Password = "";
			Console.WriteLine(stringBuilder.ToString());
			Model model = new Model();
			EntityForm<SomeEntity> entityForm = new EntityForm<SomeEntity>(GetSomeEntities);
			entityForm.Show();
			InitializeComponent();
		}


		public List<SomeEntity> GetSomeEntities()
		{
			List<SomeEntity> someEntities = new List<SomeEntity>();
			for (int i = 0; i < 5; i++)
			{
				SomeEntity someEntity = new SomeEntity();
				someEntity.SomeId = i;
				someEntity.SomeProp = i * i;
				someEntity.SomeString = i.ToString() + "SomeString";
				someEntity.SomeDate = DateTime.Now;
				someEntity.someEnum = SomeEnum.SomeEnum1;
				someEntity.c = 'a';

				Class clas = new Class();
				clas.Id = i;
				clas.MyProperty = 2*i;
				someEntity.SomeRef = clas;

				someEntities.Add(someEntity);
			}
			return someEntities;
		}
	}
}
