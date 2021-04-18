
namespace WinFormAnnouncement
{
	partial class MessageUC
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label
			// 
			this.label.AutoSize = true;
			this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
			this.label.Location = new System.Drawing.Point(3, 3);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(111, 24);
			this.label.TabIndex = 0;
			this.label.Text = "TextExamle";
			// 
			// MessageUC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label);
			this.Name = "MessageUC";
			this.Size = new System.Drawing.Size(415, 30);
			this.ResumeLayout(false);
			this.PerformLayout();

			// MessageUC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "MessageUC";
			this.Size = new System.Drawing.Size(415, 30);
			this.ResumeLayout(false);

		}
		System.Windows.Forms.Label label;
		#endregion
	}
}
