using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ReflectionForms.Fields
{
	public static class FieldsController
	{

		private static Dictionary<Type, Type> availableFields = new Dictionary<Type, Type>();
		public static void AddNewTypeField(Type typeOfValueField, Type field)
		{
			if (!typeof(IField).IsAssignableFrom(field))
				throw new Exception($"The Field: {field.FullName} does not implemented the interface \"IField\"");
			availableFields[typeOfValueField] = field;
		}

		public static void AddFields<T>(Control panel)
		{
			foreach (PropertyInfo property in typeof(T).GetProperties().Reverse())
			{
				if (property.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormNotVisible") == null)
				{
					Type type = property.PropertyType;


					if (property.IsReference(out _)) type = typeof(ReflFormRef);
					if (property.PropertyType.IsEnum) type = typeof(Enum);
					if (property.CustomAttributes
						.FirstOrDefault(att => att.AttributeType==typeof(ReflFormImage)) != null) type = typeof(Image);

					availableFields.TryGetValue(type, out Type typeOfField);

					
					if (typeOfField == null) 
						throw new NotImplementedException($"This type: {type} is not implemented");

					UserControl field = (UserControl)Activator.CreateInstance(typeOfField, property);
					//Get new instance of the UserControl field
					field.Dock = DockStyle.Top;
					field.BringToFront();
					field.Tag = property.Name;
					panel.Controls.Add(field);

				}
			}
		}
		public static void FillFields<T>(Control panel, T entity)
		{
			if (!panel.HasChildren) throw new Exception("ContorlHasNoChildren");

			foreach (UserControl fieldUc in panel.Controls)
			{

				if (!fieldUc.HasChildren) throw new Exception("ContorlHasNoChildren");


				PropertyInfo property = entity.GetType().GetProperties().FirstOrDefault(p => p.Name == fieldUc.Tag.ToString());
				((IField)fieldUc).FillField(property, entity);
			}
		}
		public static T GetEntityFromFields<T>(Control panelWithFields)
		{
			T entity = (T)Activator.CreateInstance(typeof(T));
			foreach (UserControl fieldUc in panelWithFields.Controls)
			{

				PropertyInfo prop = entity.GetType().GetProperties().FirstOrDefault(p => p.Name == fieldUc.Tag.ToString());
				object value = ((IField)fieldUc).GetValue(prop);
				try
				{
					prop.SetValue(entity, value);
				}
				catch (Exception)
				{
					var converter = TypeDescriptor.GetConverter(prop.PropertyType);
					prop.SetValue(entity, converter.ConvertTo(value, prop.PropertyType));
				}

			}
			return entity;
		}
	}
}
