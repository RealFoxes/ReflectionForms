using System.Reflection;
using System.Windows.Forms;

namespace ReflectionForms.FieldsForEdit
{
	public partial class StringAndIntNumbers : UserControl
	{
		public StringAndIntNumbers(PropertyInfo property)
		{
			this.Tag = property.DeclaringType.FullName + "." + property.Name;
			InitializeComponent();
			label.Text = Utilities.GetColumnName(property);
			if (property.PropertyType == typeof(char)) textBox.MaxLength = 1;
		}
	}
}
