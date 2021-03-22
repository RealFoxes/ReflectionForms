using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReflectionForms
{
	public delegate List<T> GetEntities<T>() where T : class;
	public partial class EntityForm<T> : Form where T : class
	{
		private GetEntities<T> getEntities { get; set; }
	
		public EntityForm(GetEntities<T> getEntities)
		{
			this.getEntities = new GetEntities<T>(getEntities);
			InitializeComponent();
			UpdateTable();
			//Add controls with labels...
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
			List<T> entities = getEntities();
			for (int i = 0; i < entities.Count; i++)
			{
				T entity = entities[i];

				DataRow row = dt.NewRow();
				foreach (PropertyInfo property in typeof(T).GetProperties())
				{
					var columnId = dt.Columns.Cast<DataColumn>().FirstOrDefault(c => c.ColumnName == GetColumnName(property)).Ordinal;
					row[columnId] = GetValue(property, entity);
				}
				dt.Rows.Add(row);
			}
			dataGridView.DataSource = dt;

		}

		private string GetColumnName(PropertyInfo property)
		{
			string columnName;
			var att = property.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormRef");
			if(att != null)
			{
				var propFromRef = property.PropertyType.GetProperties().FirstOrDefault(p => p.Name == att.ConstructorArguments[0].Value.ToString());
				columnName = propFromRef.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormName")?.ConstructorArguments[0].Value.ToString()
								 ?? propFromRef.Name;
			}
			else
			{
				columnName = property.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormName")?.ConstructorArguments[0].Value.ToString() ?? property.Name;
			}
			
			return columnName;
		}
		private object GetValue<Entity>(PropertyInfo property, Entity entity)
		{
			object value;
			var att = property.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormRef");
			if(att != null)
			{
				var propFromRef = property.PropertyType.GetProperties().FirstOrDefault(p => p.Name == att.ConstructorArguments[0].Value.ToString());
				value = propFromRef.GetValue(property.GetValue(entity));
			}
			else
			{
				value = property.GetValue(entity);
			}
			return value;
		}
	}
}
