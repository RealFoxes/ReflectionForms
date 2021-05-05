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
	public partial class ChangeEntityForm <T> : Form where T : class
	{
		private EntityForm<T> MainForm { get; set; }
		private DataGridViewRow Row { get; set; }
		private T Entity { get; set; }
		public ChangeEntityForm(EntityForm<T> mainForm, DataGridViewRow row)
		{
			this.MainForm = mainForm;
			this.Row = row;
			this.Entity = (T)row.Cells[0].Value;
			InitializeComponent();
			Utilities.AddFields<T>(panel);
			Utilities.FillFields(panel, row.Cells[0].Value);
		}

		private void buttonChange_Click(object sender, EventArgs e)
		{
			T newEntity = Utilities.GetEntityFromField<T>(panel);

			foreach (var prop in Entity.GetType().GetProperties())
			{
				foreach (var newProp in newEntity.GetType().GetProperties())
				{
					if (prop.Name == newProp.Name)
					{
						prop.SetValue(Entity, newProp.GetValue(newEntity));
					}
				}
			}
			
			EntityFormController.Instance.Save();

			MainForm.Dt.Rows.Remove(((DataRowView)Row.DataBoundItem).Row); // remove row from local table
			Utilities.AddRowToDataSource(newEntity, MainForm.Dt);

			this.DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
