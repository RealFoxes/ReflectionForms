using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormAnnouncement
{
    public class AnnouncementContorler
    {
		public enum AnnouncementPosition
		{
			UpLeft,
			UpRight,
			DownLeft,
			DownRight
		}
		public Control Control { get; set; }
		public AnnouncementPosition Position { get; set; }
		public List<MessageUC> messages { get; set; }
		public AnnouncementContorler(Control control, AnnouncementPosition position)
		{
			this.Control = control;
			this.Position = position;
		}
		public async void SendMessage(string message,int seconds)
		{
			MessageUC uc = new MessageUC(message);
			uc.BringToFront();
			Control.Controls.Add(uc);
			messages.Add(uc);

			using (var timer = new TaskTimer(seconds).Start())
			{
				foreach (var task in timer)
				{
					await task;

					Control.Controls.Remove(uc);
				}
			}
		}
	}
}
