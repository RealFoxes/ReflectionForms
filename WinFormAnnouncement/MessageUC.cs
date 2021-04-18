using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormAnnouncement
{
	public partial class MessageUC : UserControl
	{
		public MessageUC(string Message,Font font = null)
		{
			InitializeComponent();
			label.Text = Message;
			if (font != null) label.Font = font;
			
		}
	}
}
