using ReflectionForms.EntitiesForms;
using ReflectionForms.EntitiesForms.FieldsForEdit;
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

namespace ReflectionForms
{

	
	public partial class EntityForm<T> : Form where T : class
	{
		public Type[] TextBoxFieldTypes = { typeof(string), typeof(Int16), typeof(Int32), typeof(Int64),typeof(byte),typeof(char) };
	
		public EntityForm()
		{
			InitializeComponent();
			UpdateTable();
			AddFields();
			
		}
		private void AddFields()
		{

			foreach (PropertyInfo property in typeof(T).GetProperties())
			{
				var propType = property.PropertyType;
				if (property.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormNotVisible") == null)
				{
					if (TextBoxFieldTypes.Contains(propType))
					{
						var uc = new StringAndIntNumbers(property);
						uc.Dock = DockStyle.Top;
						uc.BringToFront();
						panel.Controls.Add(uc);
					}
					else if (propType == typeof(DateTime))
					{
						var uc = new DateTimeField(property);
						uc.Dock = DockStyle.Top;
						uc.BringToFront();
						panel.Controls.Add(uc);
					}
					else if (propType.GetTypeInfo().IsClass)
					{
						var uc = new ReferenceField(property);
						uc.Dock = DockStyle.Top;
						uc.BringToFront();
						panel.Controls.Add(uc);
					}
					else if (propType.GetTypeInfo().IsEnum)
					{
						var uc = new EnumField(property);
						uc.Dock = DockStyle.Top;
						uc.BringToFront();
						panel.Controls.Add(uc);
					}
				}
			}
		}
		private void UpdateTable()
		{
			DataTable dt = new DataTable();
			//header 
			foreach (PropertyInfo property in typeof(T).GetProperties())
			{
				bool ReflFormNameWasFound = false;
				bool ToHide = false;
				foreach (CustomAttributeData att in property.CustomAttributes)
				{
					switch (att.AttributeType.Name)
					{
						case "ReflFormName":
							dt.Columns.Add(new DataColumn(att.ConstructorArguments[0].Value.ToString(), property.PropertyType));
							ReflFormNameWasFound = true;
							break;
						case "ReflFormRef":
							var propFromRef = property.PropertyType.GetProperties().FirstOrDefault(p => p.Name == att.ConstructorArguments[0].Value.ToString());
							string columnName = propFromRef.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormName")?.ConstructorArguments[0].Value.ToString()
											 ?? propFromRef.Name;
							dt.Columns.Add(new DataColumn(columnName, propFromRef.PropertyType));
							ReflFormNameWasFound = true;
							break;
						case "ReflFormNotVisible":
							var test = dt.Columns[dt.Columns.Count - 1];
							ToHide = true;
							break;
					}
				}
				if(!ReflFormNameWasFound) dt.Columns.Add(new DataColumn(property.Name, property.PropertyType));
				if(ToHide) dt.Columns[dt.Columns.Count - 1].ColumnMapping = MappingType.Hidden;
			}

			//body
			List<T> entities = (List<T>)typeof(T).GetMethod("GetEntities").Invoke(null,null);
			for (int i = 0; i < entities.Count; i++)
			{
				T entity = entities[i];

				DataRow row = dt.NewRow();
				foreach (PropertyInfo property in typeof(T).GetProperties())
				{
					var columnId = dt.Columns.Cast<DataColumn>().FirstOrDefault(c => c.ColumnName == Utilities.GetColumnName(property)).Ordinal;
					row[columnId] = Utilities.GetValue(property, entity);
				}
				dt.Rows.Add(row);
			}
			dataGridView.DataSource = dt;

		}

		
	}
}
