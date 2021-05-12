using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ReflectionForms.Fields
{
	public partial class EnumField : UserControl, IField
	{
		public EnumField(PropertyInfo property)
		{
			InitializeComponent();
			label.Text = Utilities.GetColumnName(property);
			FieldInfo[] fieldsInfo = property.PropertyType.GetFields();
			for (int i = 0; i < fieldsInfo.Length; i++)
			{
				if (fieldsInfo[i].Name == "value__") continue;

				var att = fieldsInfo[i].CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormName");

				if (att != null)
					comboBox.Items.Add(att.ConstructorArguments[0].Value.ToString());
				else
					comboBox.Items.Add(fieldsInfo[i].Name);
			}
		}

		public void FillField<T>(PropertyInfo property, T instance)
		{
			comboBox.SelectedIndex = (int)property.GetValue(instance);
		}

		public object GetValue(PropertyInfo property)
		{
			return Enum.Parse(property.PropertyType, comboBox.SelectedIndex.ToString());
		}
	}
}
