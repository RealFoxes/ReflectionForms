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
			EntityForm<SomeEntity> entityForm = new EntityForm<SomeEntity>(Privileges.Add,Privileges.Remove,Privileges.Edit);
			entityForm.Show();
			InitializeComponent();
			
			using (var model = new ModelDatabase())
			{

				model.SomeEntities.FirstOrDefault().SomeString = "zalupa";
				model.SaveChanges();
				//model.SomeEntities.Include(s => s.SomeRef.SomeEntities).First()
			}
			//Принимать в базу IEnumerable коллекцию, отказатся от методов получения листов внутри сущностей
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			
		}
	}
}
