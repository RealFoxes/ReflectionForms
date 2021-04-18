
namespace ReflectionForms
{
	partial class EntityForm<T>
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonChange = new System.Windows.Forms.Button();
			this.panel = new System.Windows.Forms.Panel();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.panelAnnounce = new System.Windows.Forms.Panel();
			this.comboBoxSearch = new System.Windows.Forms.ComboBox();
			this.textBoxSearch = new System.Windows.Forms.TextBox();
			this.labelSearch = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Location = new System.Drawing.Point(12, 35);
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(707, 548);
			this.dataGridView.TabIndex = 0;
			this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(725, 560);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(75, 23);
			this.buttonDelete.TabIndex = 1;
			this.buttonDelete.Text = "Удалить";
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Visible = false;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonChange
			// 
			this.buttonChange.Location = new System.Drawing.Point(919, 560);
			this.buttonChange.Name = "buttonChange";
			this.buttonChange.Size = new System.Drawing.Size(75, 23);
			this.buttonChange.TabIndex = 2;
			this.buttonChange.Text = "Изменить";
			this.buttonChange.UseVisualStyleBackColor = true;
			this.buttonChange.Visible = false;
			this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
			// 
			// panel
			// 
			this.panel.AutoScroll = true;
			this.panel.Location = new System.Drawing.Point(725, 35);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(269, 519);
			this.panel.TabIndex = 3;
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(838, 560);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(75, 23);
			this.buttonAdd.TabIndex = 4;
			this.buttonAdd.Text = "Добавить";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Visible = false;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// panelAnnounce
			// 
			this.panelAnnounce.AutoScroll = true;
			this.panelAnnounce.Location = new System.Drawing.Point(12, 589);
			this.panelAnnounce.Name = "panelAnnounce";
			this.panelAnnounce.Size = new System.Drawing.Size(707, 28);
			this.panelAnnounce.TabIndex = 6;
			// 
			// comboBoxSearch
			// 
			this.comboBoxSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSearch.FormattingEnabled = true;
			this.comboBoxSearch.Location = new System.Drawing.Point(61, 11);
			this.comboBoxSearch.Name = "comboBoxSearch";
			this.comboBoxSearch.Size = new System.Drawing.Size(171, 21);
			this.comboBoxSearch.TabIndex = 7;
			// 
			// textBoxSearch
			// 
			this.textBoxSearch.Location = new System.Drawing.Point(238, 12);
			this.textBoxSearch.Name = "textBoxSearch";
			this.textBoxSearch.Size = new System.Drawing.Size(161, 20);
			this.textBoxSearch.TabIndex = 8;
			this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
			// 
			// labelSearch
			// 
			this.labelSearch.AutoSize = true;
			this.labelSearch.Location = new System.Drawing.Point(12, 11);
			this.labelSearch.Name = "labelSearch";
			this.labelSearch.Size = new System.Drawing.Size(42, 13);
			this.labelSearch.TabIndex = 9;
			this.labelSearch.Text = "Поиск:";
			// 
			// EntityForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1006, 618);
			this.Controls.Add(this.labelSearch);
			this.Controls.Add(this.textBoxSearch);
			this.Controls.Add(this.comboBoxSearch);
			this.Controls.Add(this.panelAnnounce);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.panel);
			this.Controls.Add(this.buttonChange);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.dataGridView);
			this.Name = "EntityForm";
			this.Text = "EntityForm";
			this.DoubleClick += new System.EventHandler(this.EntityForm_DoubleClick);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonChange;
		private System.Windows.Forms.Panel panel;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.Panel panelAnnounce;
		private System.Windows.Forms.ComboBox comboBoxSearch;
		private System.Windows.Forms.TextBox textBoxSearch;
		private System.Windows.Forms.Label labelSearch;
	}
}