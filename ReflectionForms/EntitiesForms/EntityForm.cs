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
		private MethodInfo DeleteMethod, EditMethod;
		private DataTable dt;
		public EntityForm()
		{
			InitializeComponent();
			UpdateTable();
			AddFields();
			EditMethod = typeof(T).GetMethod("EditEntity");
			if (EditMethod == null)
			{
				buttonChange.Visible = false;
			}
			DeleteMethod = typeof(T).GetMethod("DeleteEntity");
			if(DeleteMethod == null)
			{
				buttonDelete.Visible = false;
			}
			
		}
		private void AddFields()
		{

			foreach (PropertyInfo property in typeof(T).GetProperties().Reverse())
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
					else if (property.CustomAttributes.FirstOrDefault(a =>a.AttributeType.Name == "ReflFormRef") != null)
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
			dt = new DataTable();
			//Creting header 

			bool[] IndexesToHide = new bool[typeof(T).GetProperties().Length+1]; // Recording column which need to hide
			int iToHide = 0;

			dt.Columns.Add(new DataColumn("Entity", typeof(T)));
			IndexesToHide[iToHide++] = true; // Hide first column with instance entity

			foreach (PropertyInfo property in typeof(T).GetProperties())
			{
				iToHide++;
				bool ReflFormNameWasFound = false;
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
							IndexesToHide[iToHide] = true;
							break;
					}
				}
				if(!ReflFormNameWasFound) dt.Columns.Add(new DataColumn(property.Name, property.PropertyType));
			}

			//Filling body
			List<T> entities = (List<T>)typeof(T).GetMethod("GetEntities").Invoke(null,null);
			for (int i = 0; i < entities.Count; i++)
			{
				T entity = entities[i];

				DataRow row = dt.NewRow();
				row[0] = entity;
				foreach (PropertyInfo property in typeof(T).GetProperties())
				{
					var columnId = dt.Columns.Cast<DataColumn>().FirstOrDefault(c => c.ColumnName == Utilities.GetColumnName(property)).Ordinal;
					row[columnId] = Utilities.GetValue(property, entity);
				}
				dt.Rows.Add(row);
			}
			dataGridView.DataSource = dt;

			//Hide column with att and entity
			iToHide = 0;
			foreach (DataGridViewColumn Column in dataGridView.Columns)
			{
				Column.Visible = !IndexesToHide[iToHide];
				iToHide++;
			}

		}

		private void buttonChange_Click(object sender, EventArgs e)
		{
			//Добавить форму измения сущности
		}


		private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			//e.RowIndex.ToString();
				//Реализовать добавление в строки инфы
			//foreach (Control item in panel.Controls)
			//{
			//	Console.WriteLine(item.Tag);
			//}
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			object entity = dataGridView.SelectedRows[0].Cells[0].Value;
			DeleteMethod.Invoke(null, new object[] { entity }); //Call delete method from entity class 
			UpdateTable();
		}
	}
}
