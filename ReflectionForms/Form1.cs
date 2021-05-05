using MySql.Data.MySqlClient;
using ReflectionForms.Entities;
using ReflectionForms.EntitiesForms;
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
		public static ModelDatabase model = new ModelDatabase();
		public Form1()
		{
			EntityFormController.Initialize(model);

			var entityForm = EntityFormController.Instance.GetForm<SomeEntity>(Privileges.Add, Privileges.Remove, Privileges.Edit);

			entityForm.Show();
			InitializeComponent();
		}
	}
}
