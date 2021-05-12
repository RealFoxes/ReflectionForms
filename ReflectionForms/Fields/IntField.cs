using System;
using System.Reflection;
using System.Windows.Forms;

namespace ReflectionForms.Fields
{
	public partial class IntField : UserControl, IField
	{
		public IntField(PropertyInfo property)
		{
			InitializeComponent();
			label.Text = Utilities.GetColumnName(property);
		}

		public void FillField<T>(PropertyInfo property, T instance)
		{
			textBox.Text = property.GetValue(instance).ToString();
		}

		public object GetValue(PropertyInfo property)
		{
			return Int32.Parse(textBox.Text);
		}
		private void textBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
				(e.KeyChar != '.'))
			{
				e.Handled = true;
			}

			// only allow one decimal point
			if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
			{
				e.Handled = true;
			}
		}
	}
}
