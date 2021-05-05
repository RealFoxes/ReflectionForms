using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormAnnouncer
{
	public partial class Message : UserControl
	{
		public AnnouncerControler Announcer { get; set; }
		public Message(string Message, int SecToDie, AnnouncerControler announcer, Font font = null)
		{
			InitializeComponent();
			this.BringToFront();
			this.Dock = DockStyle.Top;
			this.Announcer = announcer;
			label.Text = Message;
			timer.Interval = SecToDie * 1000;
			if (font != null) label.Font = font;
		}

		private void Message_Load(object sender, EventArgs e)
		{
			timer.Start();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			timer.Stop();
			Announcer.DeleteMessage(this);
		}

		private void label_Click(object sender, EventArgs e)
		{
			MessageBox.Show(label.Text);
		}
	}
}
