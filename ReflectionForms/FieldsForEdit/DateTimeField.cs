using System.Reflection;
using System.Windows.Forms;

namespace ReflectionForms.FieldsForEdit
{
	public partial class DateTimeField : UserControl
	{
		public DateTimeField(PropertyInfo property)
		{
			InitializeComponent();
			this.Tag = property.DeclaringType.FullName + "." + property.Name;
			label.Text = Utilities.GetColumnName(property);
		}
	}
}
