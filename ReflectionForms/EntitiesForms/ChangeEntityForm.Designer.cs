
namespace ReflectionForms.EntitiesForms
{
	partial class ChangeEntityForm <T> where T : class
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
			this.panel = new System.Windows.Forms.Panel();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonChange = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// panel
			// 
			this.panel.Location = new System.Drawing.Point(12, 20);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(294, 418);
			this.panel.TabIndex = 1;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(12, 444);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Отменить";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonChange
			// 
			this.buttonChange.Location = new System.Drawing.Point(231, 444);
			this.buttonChange.Name = "buttonChange";
			this.buttonChange.Size = new System.Drawing.Size(75, 23);
			this.buttonChange.TabIndex = 2;
			this.buttonChange.Text = "Изменить";
			this.buttonChange.UseVisualStyleBackColor = true;
			this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
			// 
			// ChangeEntityForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 477);
			this.Controls.Add(this.buttonChange);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.panel);
			this.Name = "ChangeEntityForm";
			this.Text = "Изменение";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonChange;
	}
}