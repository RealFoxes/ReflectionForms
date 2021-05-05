﻿using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ReflectionForms.FieldsForEdit
{
	public partial class EnumField : UserControl
	{
		public EnumField(PropertyInfo property)
		{
			InitializeComponent();
			this.Tag = property.DeclaringType.FullName + "." + property.Name;
			label.Text = Utilities.GetColumnName(property);
			FieldInfo[] field_infos = property.PropertyType.GetFields();
			for (int i = 0; i < field_infos.Length; i++)
			{
				if (field_infos[i].Name == "value__") continue;
				var att = field_infos[i].CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormName");
				if (att != null)
				{
					comboBox.Items.Add(att.ConstructorArguments[0].Value.ToString());
				}
				else
				{

					comboBox.Items.Add(field_infos[i].Name);
				}
			}
		}
	}
}