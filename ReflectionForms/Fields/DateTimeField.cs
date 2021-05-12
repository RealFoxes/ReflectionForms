using System;
using System.Reflection;
using System.Windows.Forms;

namespace ReflectionForms.Fields
{
	public partial class DateTimeField : UserControl, IField
	{
		public DateTimeField(PropertyInfo property)
		{
			InitializeComponent();
			label.Text = Utilities.GetColumnName(property);
		}
		public void FillField<T>(PropertyInfo property, T instance)
		{
			dateTimePicker.Value = (DateTime)property.GetValue(instance);
		}
		public object GetValue(PropertyInfo property)
		{
			return dateTimePicker.Value;
		}
	}
}
