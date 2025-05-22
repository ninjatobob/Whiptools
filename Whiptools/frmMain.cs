using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Whiptools
{
    public partial class frmMain : Form
    {
        // bitmap viewer
        private byte[] bitmapData;
        private Color[] paletteData;
        private string bitmapName, paletteName;
        private frmBitmap frmBitmap;

        // bitmap creator
        private Bitmap newBitmap;
        private Color[] newPalette;
        private string newBitmapName;

        public const string unmangledSuffix = "_unmangled";

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // RUBBISH RACER
        }

    // file unmangling

        private void btnUnmangleFiles_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Mangled Files (*.BM;*.DRH;*.HMD;*.KC;*.RAW;*.TRK)|" +
                    "*.BM;*.DRH;*.HMD;*.KC;*.RAW;*.TRK|All Files (*.*)|*.*";
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
                            byte[] inputData = File.ReadAllBytes(filename);
                            outputfile = folderBrowserDialog.SelectedPath + "\\" +
                                Path.GetFileNameWithoutExtension(filename) + unmangledSuffix +
                                Path.GetExtension(filename);
                            File.WriteAllBytes(outputfile, clsUnmangler.Unmangle(inputData));
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

    // bitmap viewer

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

                    int countColors = 0;
                    foreach (byte b in bitmapData)
                    {
                        if (b > countColors)
                        {
                            countColors = b;
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
                    lblBitmapLoaded.Text = "Loaded " + bitmapData.Length + " bytes, " + (countColors + 1) + " colours";
                }
            }
            catch
            {
                lblBitmapLoaded.Text = "YOU NEED MORE PRACTICE!";
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

                    paletteData = clsBitmapper.ConvertByteToPalette(File.ReadAllBytes(filename));
                    paletteName = Path.GetFileName(filename);

                    txtPalettePath.Text = Path.GetFullPath(filename);
                    lblPaletteLoaded.Text = "Loaded " + paletteData.Length + " colours";
                }
            }
            catch
            {
                lblPaletteLoaded.Text = "YOU NEED MORE PRACTICE!";
            }
        }

        private void btnExportPalette_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = Path.GetFileNameWithoutExtension(paletteName) + "_palette";
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Portable Network Graphics (*.png)|*.png|Windows Bitmap (*.bmp)|*.bmp|All Files (*.*)|*.*";
                saveFileDialog.FileName = filename.Replace(frmMain.unmangledSuffix, "");
                saveFileDialog.Title = "Export Palette";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bitmap = clsBitmapper.ConvertPaletteToBitmap(paletteData);
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
            catch
            {
                MessageBox.Show("FATALITY!", "NETWORK ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewBitmap_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] rgbData = clsBitmapper.CreateRGBArray(bitmapData, paletteData);

                int bitmapWidth = Convert.ToInt32(double.Parse(txtDimWidth.Text));
                int bitmapHeight = Convert.ToInt32(Math.Ceiling(bitmapData.Length / (double)bitmapWidth));

                frmBitmap = new frmBitmap();
                frmBitmap.pictureBox1.Image = clsBitmapper.CreateBitmapFromRGB(bitmapWidth, bitmapHeight, rgbData);
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

    // bitmap creator

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.png;*.bmp)|*.png;*.bmp|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Image File";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    newBitmapName = openFileDialog.FileName;
                    using (Bitmap bitmap = new Bitmap(newBitmapName))
                    {
                        Bitmap tempBitmap = clsBitmapper.ConvertBitmapTo6Bit(bitmap);
                        Color[] tempPalette = clsBitmapper.GetPaletteFromBitmap(tempBitmap);
                        if (tempPalette.Length > 256)
                        {
                            MessageBox.Show("Too many colours! (" + Convert.ToString(tempPalette.Length) + ")",
                                "YOU NEED MORE PRACTICE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            newBitmap = tempBitmap;
                            newPalette = tempPalette;
                            txtImagePath.Text = newBitmapName;
                            lblImageLoaded.Text = "Loaded " + newBitmap.Width + " x " + newBitmap.Height + ", " +
                                    newPalette.Length + " colours";
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("FATALITY!", "NETWORK ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SavePalette(Color[] palette, string defaultFileName)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Palette Files (*.PAL)|*.PAL|All Files (*.*)|*.*";
            saveFileDialog.Title = "Save Palette As";
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(defaultFileName);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog.FileName;
                File.WriteAllBytes(filename, clsBitmapper.GetPaletteArray(palette));
                MessageBox.Show("Saved " + filename, "RACE OVER", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSaveNewPalette_Click(object sender, EventArgs e)
        {
            try
            {
                SavePalette(newPalette, newBitmapName);
            }
            catch
            {
                MessageBox.Show("FATALITY!", "NETWORK ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddToExistingPalette_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Whiplash Palettes (*.PAL)|*.PAL|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Palette File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = openFileDialog.FileName;

                    Color[] inputPalette = clsBitmapper.ConvertByteToPalette(File.ReadAllBytes(filename));
                    string userInput = Microsoft.VisualBasic.Interaction.InputBox("Add at position (0-255):", "Add to Palette", "0");
                    int offset = Convert.ToInt32(userInput);

                    Color[] outputPalette = inputPalette;
                    for (int i = 0; i < newPalette.Length; i++)
                    {
                        outputPalette[i + offset] = newPalette[i];
                    }
                    SavePalette(outputPalette, filename);
                }
            }
            catch
            {
                MessageBox.Show("FATALITY!", "NETWORK ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConvertImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Whiplash Palettes (*.PAL)|*.PAL|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Palette File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string paletteFilename = openFileDialog.FileName;

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "BM File (*.BM)|*.BM|DRH File (*.DRH)|*.DRH|All Files (*.*)|*.*";
                    saveFileDialog.FileName = Path.GetFileNameWithoutExtension(newBitmapName);
                    saveFileDialog.Title = "Save Bitmap As";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Color[] palette = clsBitmapper.ConvertByteToPalette(File.ReadAllBytes(paletteFilename));
                        string savefile = saveFileDialog.FileName;
                        File.WriteAllBytes(savefile, clsBitmapper.GetBitmapArray(newBitmap, palette));
                        MessageBox.Show("Saved " + savefile, "RACE OVER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch
            {
                MessageBox.Show("FATALITY!", "NETWORK ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // audio tools

        private void btnConvertRAWAudio_Click(object sender, EventArgs e)
        {
            ConvertAudio(false);
        }

        private void btnConvertCheatAudio_Click(object sender, EventArgs e)
        {
            ConvertAudio(true);
        }

        private void ConvertAudio(bool cheatMode)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Whiplash " +
                    (cheatMode ? "Cheat Audio (*.KC)|*.KC" : "Raw Audio (*.RAW)|*.RAW") + "|All Files (*.*)|*.*";
                openFileDialog.Title = "Select " + (cheatMode ? "Cheat" : "Raw") + " Audio Files";
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
                            byte[] wavData = clsAudioConverter.RawToWav(cheatMode ? clsUnmangler.DecodeKC(rawData) : rawData);
                            outputfile = folderBrowserDialog.SelectedPath + "\\" +
                                Path.GetFileName(filename) + ".WAV";
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
    }
}