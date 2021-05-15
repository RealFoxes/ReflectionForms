using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ReflectionForms.Fields
{
	public partial class ImageField : UserControl, IField
	{
		public ImageField(PropertyInfo property)
		{
			InitializeComponent();
			label.Text = Utilities.GetColumnName(property);
		}

		public void FillField<T>(PropertyInfo property, T instance)
		{
			pictureBox.Image = GetImageFromBytes((byte[])property.GetValue(instance));
		}

		public object GetValue(PropertyInfo property)
		{
			return GetBytesFromImage(pictureBox.Image);
		}

		private static Image GetImageFromBytes(byte[] bytes)
		{
			
			using (var stream = new MemoryStream(bytes))
			{
				Image image = Image.FromStream(stream);
				return image;
			}
		}
		private static byte[] GetBytesFromImage(Image image)
		{
			using (var memoryStream = new MemoryStream())
			{
				image.Save(memoryStream,ImageFormat.Png);
				return memoryStream.ToArray();
			}
		}

		private void pictureBox_DoubleClick(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "image files (*.png)|*.png|All files (*.*)|*.*";
			dialog.InitialDirectory = @"C:\";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				pictureBox.Image = Image.FromFile(dialog.FileName);
			}
		}
	}
}
