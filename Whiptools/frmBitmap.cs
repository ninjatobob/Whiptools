using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Whiptools
{
    public partial class frmBitmap : Form
    {
        public string filename;

        public frmBitmap()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Portable Network Graphics (*.png)|*.png|Windows Bitmap (*.bmp)|*.bmp|All Files (*.*)|*.*",
                FileName = filename.Replace(frmMain.unmangledSuffix, ""),
                Title = "Save As"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(pictureBox1.Image);
                string ext = Path.GetExtension(saveFileDialog.FileName);
                switch (ext.ToLower())
                {
                    case ".png":
                        bitmap.Save(saveFileDialog.FileName, ImageFormat.Png);
                        MessageBox.Show("Saved " + saveFileDialog.FileName, "RACE OVER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case ".bmp":
                        bitmap.Save(saveFileDialog.FileName, ImageFormat.Bmp);
                        MessageBox.Show("Saved " + saveFileDialog.FileName, "RACE OVER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    default:
                        MessageBox.Show("FATALITY!", "NETWORK ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
                bitmap.Dispose();
            }
        }
    }
}