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
			T entity = (T)Activator.CreateInstance(typeof(T));
			foreach (Control uc in panel.Controls)
			{
				var NameOfProp = uc.Tag.ToString();
				NameOfProp = NameOfProp.Remove(0, NameOfProp.LastIndexOf('.') + 1);
				PropertyInfo prop = entity.GetType().GetProperties().FirstOrDefault(p => p.Name == NameOfProp);
				var att = prop.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormRef");
				var converter = TypeDescriptor.GetConverter(prop.PropertyType);
				foreach (Control control in uc.Controls)
				{
					if (control is Label) continue;

					if (control is DateTimePicker)
					{

						prop.SetValue(entity, ((DateTimePicker)control).Value, null);
						continue;
					}
					if(att != null)
					{
						prop.SetValue(entity, ((ComboBox)control).SelectedItem, null);
						continue;
					}
					if (prop.PropertyType.IsEnum)
					{
						var _enum = Enum.Parse(prop.PropertyType, ((ComboBox)control).SelectedIndex.ToString());
						prop.SetValue(entity, _enum);
						continue;
					}
					
					
					prop.SetValue(entity, converter.ConvertTo(control.Text, prop.PropertyType), null);

				}

			}
			MainForm.AddMethod.Invoke(null, new object[] { entity });
			MainForm.UpdateTable();
			MessageBox.Show("Успешно добавлено");
		}
	}
}
