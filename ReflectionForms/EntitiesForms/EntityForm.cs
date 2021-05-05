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
using WinFormAnnouncer;

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
		public DataTable Dt;
		public AnnouncerControler Announcer;
		public EntityForm(EntityFormController controller, Privileges[] privileges) // Добавить реализацию прав скорее всего с помощью енамов / Добавить формы / Еще раз подумать над реализацией получаение всех инстансов из базы
		{
			InitializeComponent();
			Announcer = new AnnouncerControler(panelAnnounce, 5);

			buttonAdd.Visible = privileges.Contains(Privileges.Add);
			buttonDelete.Visible = privileges.Contains(Privileges.Remove);
			buttonChange.Visible = privileges.Contains(Privileges.Edit);

			UpdateTable();
			Utilities.AddFields<T>(panel);


			foreach (var item in typeof(T).GetProperties())
			{
				comboBoxSearch.Items.Add(Utilities.GetColumnName(item));
			}
		}
		public void UpdateTable()
		{
			Dt = new DataTable();
			//Creting header 

			bool[] IndexesToHide = new bool[typeof(T).GetProperties().Length+1]; // Recording column which need to hide
			int iToHide = 0;

			Dt.Columns.Add(new DataColumn("Entity", typeof(T)));
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
				Dt.Columns.Add(new DataColumn(columnName, columnType));
			}

			//Filling body
			//List<T> entities = (List<T>)typeof(T).GetMethod("GetEntities").Invoke(null,null);
			var entities = EntityFormController.Instance.GetAll<T>();


			foreach (var entity in entities)
			{
				Utilities.AddRowToDataSource(entity, Dt);
			}
				
			dataGridView.DataSource = Dt;

			//Hide column with att and entity
			iToHide = 0;
			foreach (DataGridViewColumn Column in dataGridView.Columns)
			{
				Column.Visible = !IndexesToHide[iToHide++];
			}

		}

		private void buttonChange_Click(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count < 1) return;
			var row = dataGridView.SelectedRows[0];
			if (row.Cells[0].Value == null) return;
			ChangeEntityForm<T> changeEntityForm = new ChangeEntityForm<T>(this, row);
			if(changeEntityForm.ShowDialog() == DialogResult.OK)
			{
				Announcer.SendMessage("Успешно изменено");
			}
			else
			{
				Announcer.SendMessage("Отмена изменения");
			}
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			AddEntityForm<T> addEntityForm = new AddEntityForm<T>(this);
			if (addEntityForm.ShowDialog() == DialogResult.OK)
			{
				Announcer.SendMessage("Успешно добавлено");
			}
			else
			{
				Announcer.SendMessage("Отмена добавления");
			}
		}

		private void textBoxSearch_TextChanged(object sender, EventArgs e)
		{

			if (comboBoxSearch.Text == "")
			{
				Announcer.SendMessage("Выберите колонку");
			}
			else
			{
				CurrencyManager currencyManager = (CurrencyManager)BindingContext[dataGridView.DataSource];
				currencyManager.SuspendBinding();

				foreach (DataGridViewRow row in dataGridView.Rows)
				{
					if (row.Cells[0].Value == null) continue;
					row.Visible = row.Cells[comboBoxSearch.Text].Value.ToString().Contains(textBoxSearch.Text);
				}

				currencyManager.ResumeBinding();
			}
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{

			if (MessageBox.Show("Вы уверены что ходите удалить выбранный элемент?", "Потверждение удаления", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				T entity = (T)dataGridView.SelectedRows[0].Cells[0].Value;
				EntityFormController.Instance.Remove(entity);
				EntityFormController.Instance.Save();
				//DeleteMethod.Invoke(null, new object[] { entity }); //Call delete method from entity class 
				dataGridView.Rows.RemoveAt(dataGridView.SelectedRows[0].Index);
				Announcer.SendMessage("Успешно удаленно");



			}
		}

		private void EntityForm_DoubleClick(object sender, EventArgs e)
		{
			UpdateTable();//!!!!!!!!!!!!!!!!!!!!
		}

		private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			//????
			//e.RowIndex.ToString();
				//Реализовать добавление в строки инфы
			//foreach (Control item in panel.Controls)
			//{
			//	Console.WriteLine(item.Tag);
			//}
		}

		
	}
}
