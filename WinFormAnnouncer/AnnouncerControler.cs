using System.Drawing;
using System.Windows.Forms;

namespace WinFormAnnouncer
{
	public class AnnouncerControler
	{
		private Control Control { get; set; }
		private Font Font { get; set; }
		private int SecToDie { get; set; }

		public AnnouncerControler(Control control, int secToDie, Font font = null)
		{
			this.Control = control;
			this.Font = font;
			this.SecToDie = secToDie;
		}
		public void SendMessage(string message)
		{
			Message mess = new Message(message, SecToDie, this, Font);
			Control.Controls.Add(mess);
		}
		public void DeleteMessage(Message message)
		{
			Control.Controls.Remove(message);
		}
	}
}
