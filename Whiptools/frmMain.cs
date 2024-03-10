using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Whiptools
{
    public partial class frmMain : Form
    {
        private byte[] bitmapData, paletteData;
        private string bitmapName, paletteName;
        private frmBitmap frmBitmap;

        public const string unmangledSuffix = "_unmangled";

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // RUBBISH RACER
        }

        private void btnUnmangle_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Mangled Files (*.BM;*.DRH;*.HMD;*.KC;*.RAW;*.TRK)|*.BM;*.DRH;*.HMD;*.KC;*.RAW;*.TRK|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Mangled Files";
                openFileDialog.Multiselect = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                    folderBrowserDialog.Description = "Save unmangled files in:";
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        string outputfile = "";
                        foreach (String filename in openFileDialog.FileNames)
                        {
                            byte[] mangledData = File.ReadAllBytes(filename);
                            byte[] unmangledData = clsUnmangler.Unmangle(mangledData);
                            outputfile = folderBrowserDialog.SelectedPath + "\\" +
                                Path.GetFileNameWithoutExtension(filename) + unmangledSuffix +
                                Path.GetExtension(filename);
                            File.WriteAllBytes(outputfile, unmangledData);
                        }
                        string msg = "";
                        if (openFileDialog.FileNames.Length == 1)
                        {
                            msg = "Saved " + outputfile;
                        }
                        else
                        {
                            msg = "Saved " + openFileDialog.FileNames.Length + " unmangled files in " +
                                folderBrowserDialog.SelectedPath;
                        }
                        MessageBox.Show(msg, "RACE OVER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch
            {
                MessageBox.Show("FATALITY!", "NETWORK ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConvertAudio_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Whiplash Audio (*.RAW)|*.RAW|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Raw Audio Files";
                openFileDialog.Multiselect = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                    folderBrowserDialog.Description = "Save WAV files in:";
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        string outputfile = "";
                        foreach (String filename in openFileDialog.FileNames)
                        {
                            byte[] rawData = File.ReadAllBytes(filename);
                            byte[] wavData = clsAudioConverter.RawToWav(rawData);
                            outputfile = folderBrowserDialog.SelectedPath + "\\" +
                                Path.GetFileNameWithoutExtension(filename) + ".WAV";
                            outputfile = outputfile.Replace(unmangledSuffix, "");
                            File.WriteAllBytes(outputfile, wavData);
                        }
                        string msg = "";
                        if (openFileDialog.FileNames.Length == 1)
                        {
                            msg = "Saved " + outputfile;
                        }
                        else
                        {
                            msg = "Saved " + openFileDialog.FileNames.Length + " WAV files in " +
                                folderBrowserDialog.SelectedPath;
                        }
                        MessageBox.Show(msg, "RACE OVER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch
            {
                MessageBox.Show("FATALITY!", "NETWORK ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadBitmap_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Whiplash Bitmaps (*.BM;*.DRH)|*.BM;*.DRH|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Bitmap File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = openFileDialog.FileName;

                    bitmapData = File.ReadAllBytes(filename);
                    bitmapName = Path.GetFileName(filename);

                    int numColours = 0;
                    foreach (byte b in bitmapData)
                    {
                        if (b > numColours)
                        {
                            numColours = b;
                        }
                    }

                    for (int i = (int)Math.Sqrt(bitmapData.Length); i > 1; i--)
                    {
                        double guessWidth = (double)bitmapData.Length / i;
                        if (guessWidth == (int)guessWidth)
                        {
                            txtDimWidth.Text = guessWidth.ToString();
                            lblDimHeight.Text = "x " + i.ToString();
                            break;
                        }
                    }

                    txtBitmapPath.Text = Path.GetFullPath(filename);
                    lblBitmapLoaded.Text = "Loaded " + bitmapData.Length + " bytes, " + (numColours + 1) + " colours";
                }
            }
            catch
            {
                lblBitmapLoaded.Text = "YOU NEED MORE PRACTICE!";
            }
        }
        
        private void btnLoadPalette_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Whiplash Palettes (*.PAL)|*.PAL|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Palette File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = openFileDialog.FileName;

                    paletteData = File.ReadAllBytes(filename);
                    paletteName = Path.GetFileName(filename);

                    txtPalettePath.Text = Path.GetFullPath(filename);
                    lblPaletteLoaded.Text = "Loaded " + (paletteData.Length / 3) + " colours";
                }
            }
            catch
            {
                lblPaletteLoaded.Text = "YOU NEED MORE PRACTICE!";
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] rgbData = clsBitmapper.CreateRGB(bitmapData, paletteData);

                int bitmapWidth = Convert.ToInt32(double.Parse(txtDimWidth.Text));
                int bitmapHeight = Convert.ToInt32(Math.Ceiling(bitmapData.Length / (double)bitmapWidth));

                frmBitmap = new frmBitmap();
                frmBitmap.pictureBox1.Image = clsBitmapper.CreateBitmap(bitmapWidth, bitmapHeight, rgbData);
                frmBitmap.pictureBox1.Location = new Point(0, 0);
                frmBitmap.pictureBox1.Size = new Size(bitmapWidth, bitmapHeight);
                frmBitmap.Width = Math.Max(320, Math.Min(bitmapWidth, Convert.ToInt32(Screen.PrimaryScreen.Bounds.Width * 0.95))) + 16;
                frmBitmap.Height = Math.Min(bitmapHeight, Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height * 0.95)) + 39;
                frmBitmap.Text = bitmapName + " | " + paletteName + " | " + bitmapWidth + " x " + bitmapHeight + " | Click on image to save";
                frmBitmap.filename = Path.GetFileNameWithoutExtension(bitmapName);
                frmBitmap.Show();
            }
            catch
            {
                MessageBox.Show("YOU'VE GOT TO TRY HARDER!", "NETWORK ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDimWidth_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int bitmapWidth = Convert.ToInt32(double.Parse(txtDimWidth.Text));
                int bitmapHeight = Convert.ToInt32(Math.Ceiling(bitmapData.Length / (double)bitmapWidth));
                lblDimHeight.Text = "x " + bitmapHeight;
            }
            catch
            {
                lblDimHeight.Text = "x ?";
            }
        }
    }
}