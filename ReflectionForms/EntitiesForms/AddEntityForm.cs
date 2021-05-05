using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReflectionForms.EntitiesForms
{
	public partial class AddEntityForm<T> : Form where T : class
	{
		public EntityForm<T> MainForm { get; set; }
		public AddEntityForm(EntityForm<T> mainForm)
		{
			this.MainForm = mainForm;
			InitializeComponent();
			Utilities.AddFields<T>(panel);
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			T entity = Utilities.GetEntityFromField<T>(panel);
			EntityFormController.Instance.Add(entity);
			EntityFormController.Instance.Save();
			//MainForm.AddMethod.Invoke(null, new object[] { entity });
			Utilities.AddRowToDataSource(entity, MainForm.Dt);
			this.DialogResult = DialogResult.OK;
			Close();
			MainForm.Announcer.SendMessage("Успешно добавлено"); // Реализовать анонсер
			
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
