using System;
using System.Collections;
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
	public partial class ReferenceField : UserControl
	{
		public List<object> EntitiesList { get; set; }
		public ReferenceField(PropertyInfo property)
		{
			InitializeComponent();
			this.Tag = property.DeclaringType.FullName + "." + property.Name;
			label.Text = Utilities.GetColumnName(property);
			EntitiesList = ((IEnumerable)property.PropertyType.GetMethod("GetEntities").Invoke(null, null)).Cast<object>().ToList();
			if (property.IsReference(out CustomAttributeData att))
			{
				comboBox.DisplayMember = att.ConstructorArguments[0].Value.ToString();
			}
			comboBox.DataSource = EntitiesList;
		}
	}
}
