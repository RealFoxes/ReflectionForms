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
	public partial class DateTimeField : UserControl
	{
		public DateTimeField(PropertyInfo property)
		{
			InitializeComponent();
			label.Text = Utilities.GetColumnName(property);
		}
	}
}
