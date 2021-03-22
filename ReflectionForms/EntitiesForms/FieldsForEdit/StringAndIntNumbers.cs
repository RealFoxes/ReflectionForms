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

namespace ReflectionForms.EntitiesForms.FieldsForEdit
{
	public partial class StringAndIntNumbers : UserControl
	{
		public StringAndIntNumbers(PropertyInfo property)
		{
			this.Tag = property.DeclaringType.FullName;
			InitializeComponent();
			label.Text = Utilities.GetColumnName(property);
			if (property.PropertyType == typeof(char)) textBox.MaxLength = 1;
		}
	}
}
