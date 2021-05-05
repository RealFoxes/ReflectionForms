using System.Reflection;
using System.Windows.Forms;

namespace ReflectionForms.FieldsForEdit
{
	public partial class ReferenceField : UserControl
	{
		public ReferenceField(PropertyInfo property)
		{
			InitializeComponent();
			this.Tag = property.DeclaringType.FullName + "." + property.Name;
			label.Text = Utilities.GetColumnName(property);
			var EntitiesList = EntityFormController.Instance.GetAll(property.PropertyType);
			if (property.IsReference(out CustomAttributeData att))
			{
				comboBox.DisplayMember = att.ConstructorArguments[0].Value.ToString();
			}
			comboBox.DataSource = EntitiesList;
		}
	}
}
