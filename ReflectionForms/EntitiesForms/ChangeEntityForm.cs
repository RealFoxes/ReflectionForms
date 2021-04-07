using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReflectionForms.EntitiesForms
{
	public partial class ChangeEntityForm <T> : Form where T : class
	{
		private EntityForm<T> MainForm { get; set; }
		private T EntityToChange { get; set; }
		public ChangeEntityForm(EntityForm<T> mainForm, T entityToChange)
		{
			this.MainForm = mainForm;
			this.EntityToChange = entityToChange;
			InitializeComponent();
			Utilities.AddFields<T>(panel);
			Utilities.FillFields<T>(panel, entityToChange);
		}

		private void buttonChange_Click(object sender, EventArgs e)
		{

		}
	}
}
