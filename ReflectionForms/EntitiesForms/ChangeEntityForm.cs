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
		public ChangeEntityForm(EntityForm<T> mainForm, DataGridViewRow row)
		{
			this.MainForm = mainForm;
			this.Row = row;
			InitializeComponent();
			Utilities.AddFields<T>(panel);
			Utilities.FillFields(panel, row.Cells[0].Value);
		}

		private void buttonChange_Click(object sender, EventArgs e)
		{
			T newEntity = (T)Activator.CreateInstance(typeof(T));
			foreach (Control uc in panel.Controls)
			{
				var NameOfProp = uc.Tag.ToString();
				NameOfProp = NameOfProp.Remove(0, NameOfProp.LastIndexOf('.') + 1);
				PropertyInfo prop = newEntity.GetType().GetProperties().FirstOrDefault(p => p.Name == NameOfProp);
				var converter = TypeDescriptor.GetConverter(prop.PropertyType);
				foreach (Control control in uc.Controls)
				{
					if (control is Label) continue;

					if (control is DateTimePicker dateTimePicker)
					{

						prop.SetValue(newEntity, dateTimePicker.Value, null);
						continue;
					}
					if (prop.IsReference(out CustomAttributeData att))
					{
						prop.SetValue(newEntity, ((ComboBox)control).SelectedItem, null);
						continue;
					}
					if (prop.PropertyType.IsEnum)
					{
						var _enum = Enum.Parse(prop.PropertyType, ((ComboBox)control).SelectedIndex.ToString());
						prop.SetValue(newEntity, _enum);
						continue;
					}


					prop.SetValue(newEntity, converter.ConvertTo(control.Text, prop.PropertyType), null);

				}

			}

			MainForm.EditMethod.Invoke(null, new object[] { Row.Cells[0].Value, newEntity});

			MainForm.dt.Rows.Remove(((DataRowView)Row.DataBoundItem).Row); // remove row from local table
			Utilities.AddRowToDataSource(newEntity, MainForm.dt);

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
