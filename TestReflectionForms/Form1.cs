using ReflectionForms;
using System;
using System.Windows.Forms;
using TestReflectionForms.Entities;

namespace TestReflectionForms
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

		private void button1_Click(object sender, EventArgs e)
		{

		}
	}
}
