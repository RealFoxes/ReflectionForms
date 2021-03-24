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
		public ReferenceField(PropertyInfo property)
		{
			InitializeComponent();
			//Нужно реализовать добавление массива в комбобокс с сущностью и его отображаемым значением...
			this.Tag = property.DeclaringType.FullName + "." + property.Name;
			label.Text = Utilities.GetColumnName(property);
			var att = property.CustomAttributes?.FirstOrDefault(a => a.AttributeType.Name == "ReflFormRef");
			var List = ((IEnumerable)property.PropertyType.GetMethod("GetEntities").Invoke(null, null)).Cast<object>().ToList();
			foreach (var item in List)
			{
				if(att != null)
				{
					comboBox.Items.Add(item.GetType().GetProperties().FirstOrDefault(p => p.Name == att.ConstructorArguments[0].Value.ToString())?.GetValue(item).ToString());
				}
				else
				{
					comboBox.Items.Add(item);
				}
			}
		}
	}
}
