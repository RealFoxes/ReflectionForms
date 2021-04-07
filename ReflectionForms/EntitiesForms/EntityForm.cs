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

	public enum Privileges
	{
		Edit,
		Remove,
		Add
	}
	public partial class EntityForm<T> : Form where T : class
	{
		private Type[] TextBoxFieldTypes = { typeof(string), typeof(Int16), typeof(Int32), typeof(Int64),typeof(byte),typeof(char) };
		public MethodInfo DeleteMethod, EditMethod, AddMethod;
		private DataTable dt;
		public EntityForm(params Privileges[] privileges) // Добавить реализацию прав скорее всего с помощью енамов / Добавить формы / Еще раз подумать над реализацией получаение всех инстансов из базы
		{
			InitializeComponent();
			foreach (var privilege in privileges)
			{
				switch (privilege)
				{
					case Privileges.Edit:
						buttonChange.Visible = true;
						break;
					case Privileges.Remove:
						buttonDelete.Visible = true;
						break;
					case Privileges.Add:
						buttonAdd.Visible = true;
						break;
				}
			}
			if (typeof(T).GetMethod("GetEntities") == null) throw new MethodGetEntitiesIsNotImplementedException();

			UpdateTable();
			Utilities.AddFields<T>(panel);

			EditMethod = typeof(T).GetMethod("EditEntity");
			if (EditMethod == null && privileges.Contains(Privileges.Edit))
			{
				buttonChange.Visible = false;
			}

			DeleteMethod = typeof(T).GetMethod("DeleteEntity");
			if(DeleteMethod == null && privileges.Contains(Privileges.Remove))
			{
				buttonDelete.Visible = false;
			}

			AddMethod = typeof(T).GetMethod("AddEntity");
			if (AddMethod == null && privileges.Contains(Privileges.Add))
			{
				buttonDelete.Visible = false;
			}
		}
		public void UpdateTable()
		{
			dt = new DataTable();
			//Creting header 

			bool[] IndexesToHide = new bool[typeof(T).GetProperties().Length+1]; // Recording column which need to hide
			int iToHide = 0;

			dt.Columns.Add(new DataColumn("Entity", typeof(T)));
			IndexesToHide[iToHide++] = true; // Hide first column with instance entity

			foreach (PropertyInfo property in typeof(T).GetProperties())
			{
				Type columnType = property.PropertyType;
				string columnName = property.Name;
				foreach (CustomAttributeData att in property.CustomAttributes)
				{
					
					switch (att.AttributeType.Name) // Возможно посмотреть другие реализации менее костыльные с указанием конкретного атрибута, а не его наим.
					{
						case "ReflFormName":
							columnName = att.ConstructorArguments[0].Value.ToString();
							break;
						case "ReflFormRef":
							var propFromRef = property.PropertyType
								.GetProperties()
								.FirstOrDefault(p => p.Name == att.ConstructorArguments[0].Value.ToString());
							columnName = propFromRef.CustomAttributes
								.FirstOrDefault(a => a.AttributeType.Name == "ReflFormName")?
								.ConstructorArguments[0].Value.ToString()
											 ?? propFromRef.Name;

							columnType = propFromRef.PropertyType;
							break;
						case "ReflFormNotVisible":
							IndexesToHide[iToHide] = true;
							break;
					}
				}
				iToHide++;
				if (property.PropertyType.IsEnum) columnType = typeof(string);
				dt.Columns.Add(new DataColumn(columnName, columnType));
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
					if (property.PropertyType.IsEnum)
					{
						string nameOfEnum = Utilities.GetValue(property, entity).ToString();
						FieldInfo field = property.PropertyType.GetField(Utilities.GetValue(property, entity).ToString());

						var att = field.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == "ReflFormName");
						if (att != null)
						{
							nameOfEnum = att.ConstructorArguments[0].Value.ToString();
						}

						row[columnId] = nameOfEnum;

					}
					else
					{
						row[columnId] = Utilities.GetValue(property, entity);
					}


			}
				dt.Rows.Add(row);
			}
			dataGridView.DataSource = dt;

			//Hide column with att and entity
			iToHide = 0;
			foreach (DataGridViewColumn Column in dataGridView.Columns)
			{
				Column.Visible = !IndexesToHide[iToHide++];
			}

		}

		private void buttonChange_Click(object sender, EventArgs e)
		{
			object entity = dataGridView.SelectedRows[0].Cells[0].Value;
			ChangeEntityForm<T> changeEntityForm = new ChangeEntityForm<T>(this,(T)entity);
			changeEntityForm.Show();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			AddEntityForm<T> addEntityForm = new AddEntityForm<T>(this);
			addEntityForm.Show();
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

			if (MessageBox.Show("Вы уверены что ходите удалить выбранный элемент?","Потверждение удаления", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				object entity = dataGridView.SelectedRows[0].Cells[0].Value;
				DeleteMethod.Invoke(null, new object[] { entity }); //Call delete method from entity class 
				UpdateTable();
			}
		}
	}
}
