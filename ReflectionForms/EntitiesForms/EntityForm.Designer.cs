﻿
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
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView
			// 
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Location = new System.Drawing.Point(12, 12);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.Size = new System.Drawing.Size(707, 594);
			this.dataGridView.TabIndex = 0;
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(725, 560);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(75, 23);
			this.buttonDelete.TabIndex = 1;
			this.buttonDelete.Text = "Удалить";
			this.buttonDelete.UseVisualStyleBackColor = true;
			// 
			// buttonChange
			// 
			this.buttonChange.Location = new System.Drawing.Point(865, 560);
			this.buttonChange.Name = "buttonChange";
			this.buttonChange.Size = new System.Drawing.Size(75, 23);
			this.buttonChange.TabIndex = 2;
			this.buttonChange.Text = "Изменить";
			this.buttonChange.UseVisualStyleBackColor = true;
			// 
			// panel
			// 
			this.panel.AutoScroll = true;
			this.panel.Location = new System.Drawing.Point(725, 12);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(269, 542);
			this.panel.TabIndex = 3;
			// 
			// EntityForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1006, 618);
			this.Controls.Add(this.panel);
			this.Controls.Add(this.buttonChange);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.dataGridView);
			this.Name = "EntityForm";
			this.Text = "EntityForm";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonChange;
		private System.Windows.Forms.Panel panel;
	}
}