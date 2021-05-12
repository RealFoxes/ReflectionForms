using System.Reflection;
using System.Windows.Forms;

namespace ReflectionForms.Fields
{
	public partial class StringField : UserControl, IField
	{
		public StringField(PropertyInfo property)
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
			return textBox.Text;
		}
	}
}
