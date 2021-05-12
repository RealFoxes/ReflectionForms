using System.Reflection;
using System.Windows.Forms;

namespace ReflectionForms.Fields
{
	public partial class ReferenceField : UserControl, IField
	{
		public ReferenceField(PropertyInfo property)
		{
			InitializeComponent();
			label.Text = Utilities.GetColumnName(property);
			var EntitiesList = EntityFormController.Instance.GetAll(property.PropertyType);
			if (property.IsReference(out CustomAttributeData att))
			{
				comboBox.DisplayMember = att.ConstructorArguments[0].Value.ToString();
			}
			comboBox.DataSource = EntitiesList;
		}

		public void FillField<T>(PropertyInfo property, T instance)
		{
			comboBox.SelectedItem = property.GetValue(instance);
		}

		public object GetValue(PropertyInfo property)
		{
			return comboBox.SelectedItem;
		}
	}
}
