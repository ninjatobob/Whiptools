namespace Whiptools
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDimHeight = new System.Windows.Forms.Label();
            this.txtDimWidth = new System.Windows.Forms.TextBox();
            this.txtPalettePath = new System.Windows.Forms.TextBox();
            this.txtBitmapPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPaletteLoaded = new System.Windows.Forms.Label();
            this.lblBitmapLoaded = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadPalette = new System.Windows.Forms.Button();
            this.btnLoadBitmap = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnUnmangle = new System.Windows.Forms.Button();
            this.btnConvertAudio = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDimHeight);
            this.groupBox1.Controls.Add(this.txtDimWidth);
            this.groupBox1.Controls.Add(this.txtPalettePath);
            this.groupBox1.Controls.Add(this.txtBitmapPath);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblPaletteLoaded);
            this.groupBox1.Controls.Add(this.lblBitmapLoaded);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnLoadPalette);
            this.groupBox1.Controls.Add(this.btnLoadBitmap);
            this.groupBox1.Controls.Add(this.btnView);
            this.groupBox1.Location = new System.Drawing.Point(12, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(556, 194);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bitmap Viewer";
            // 
            // lblDimHeight
            // 
            this.lblDimHeight.AutoSize = true;
            this.lblDimHeight.Location = new System.Drawing.Point(175, 70);
            this.lblDimHeight.Name = "lblDimHeight";
            this.lblDimHeight.Size = new System.Drawing.Size(21, 13);
            this.lblDimHeight.TabIndex = 21;
            this.lblDimHeight.Text = "x ?";
            // 
            // txtDimWidth
            // 
            this.txtDimWidth.Location = new System.Drawing.Point(128, 67);
            this.txtDimWidth.Name = "txtDimWidth";
            this.txtDimWidth.Size = new System.Drawing.Size(41, 20);
            this.txtDimWidth.TabIndex = 13;
            this.txtDimWidth.TextChanged += new System.EventHandler(this.txtDimWidth_TextChanged);
            // 
            // txtPalettePath
            // 
            this.txtPalettePath.Location = new System.Drawing.Point(64, 100);
            this.txtPalettePath.Name = "txtPalettePath";
            this.txtPalettePath.ReadOnly = true;
            this.txtPalettePath.Size = new System.Drawing.Size(372, 20);
            this.txtPalettePath.TabIndex = 15;
            // 
            // txtBitmapPath
            // 
            this.txtBitmapPath.Location = new System.Drawing.Point(64, 24);
            this.txtBitmapPath.Name = "txtBitmapPath";
            this.txtBitmapPath.ReadOnly = true;
            this.txtBitmapPath.Size = new System.Drawing.Size(372, 20);
            this.txtBitmapPath.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Dimensions:";
            // 
            // lblPaletteLoaded
            // 
            this.lblPaletteLoaded.AutoSize = true;
            this.lblPaletteLoaded.Location = new System.Drawing.Point(61, 123);
            this.lblPaletteLoaded.Name = "lblPaletteLoaded";
            this.lblPaletteLoaded.Size = new System.Drawing.Size(72, 13);
            this.lblPaletteLoaded.TabIndex = 17;
            this.lblPaletteLoaded.Text = "No file loaded";
            // 
            // lblBitmapLoaded
            // 
            this.lblBitmapLoaded.AutoSize = true;
            this.lblBitmapLoaded.Location = new System.Drawing.Point(61, 47);
            this.lblBitmapLoaded.Name = "lblBitmapLoaded";
            this.lblBitmapLoaded.Size = new System.Drawing.Size(72, 13);
            this.lblBitmapLoaded.TabIndex = 16;
            this.lblBitmapLoaded.Text = "No file loaded";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Palette file:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Bitmap file:";
            // 
            // btnLoadPalette
            // 
            this.btnLoadPalette.Location = new System.Drawing.Point(442, 100);
            this.btnLoadPalette.Name = "btnLoadPalette";
            this.btnLoadPalette.Size = new System.Drawing.Size(106, 36);
            this.btnLoadPalette.TabIndex = 18;
            this.btnLoadPalette.Text = "Load Palette";
            this.btnLoadPalette.UseVisualStyleBackColor = true;
            this.btnLoadPalette.Click += new System.EventHandler(this.btnLoadPalette_Click);
            // 
            // btnLoadBitmap
            // 
            this.btnLoadBitmap.Location = new System.Drawing.Point(442, 24);
            this.btnLoadBitmap.Name = "btnLoadBitmap";
            this.btnLoadBitmap.Size = new System.Drawing.Size(106, 36);
            this.btnLoadBitmap.TabIndex = 12;
            this.btnLoadBitmap.Text = "Load Bitmap";
            this.btnLoadBitmap.UseVisualStyleBackColor = true;
            this.btnLoadBitmap.Click += new System.EventHandler(this.btnLoadBitmap_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(6, 151);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(542, 34);
            this.btnView.TabIndex = 20;
            this.btnView.Text = "View Bitmap";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnUnmangle
            // 
            this.btnUnmangle.Location = new System.Drawing.Point(12, 12);
            this.btnUnmangle.Name = "btnUnmangle";
            this.btnUnmangle.Size = new System.Drawing.Size(275, 38);
            this.btnUnmangle.TabIndex = 0;
            this.btnUnmangle.Text = "Unmangle Files";
            this.btnUnmangle.UseVisualStyleBackColor = true;
            this.btnUnmangle.Click += new System.EventHandler(this.btnUnmangle_Click);
            // 
            // btnConvertAudio
            // 
            this.btnConvertAudio.Location = new System.Drawing.Point(293, 12);
            this.btnConvertAudio.Name = "btnConvertAudio";
            this.btnConvertAudio.Size = new System.Drawing.Size(275, 38);
            this.btnConvertAudio.TabIndex = 1;
            this.btnConvertAudio.Text = "Convert Raw Audio";
            this.btnConvertAudio.UseVisualStyleBackColor = true;
            this.btnConvertAudio.Click += new System.EventHandler(this.btnConvertAudio_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 267);
            this.Controls.Add(this.btnConvertAudio);
            this.Controls.Add(this.btnUnmangle);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Whiptools";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDimHeight;
        private System.Windows.Forms.TextBox txtDimWidth;
        private System.Windows.Forms.TextBox txtPalettePath;
        private System.Windows.Forms.TextBox txtBitmapPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPaletteLoaded;
        private System.Windows.Forms.Label lblBitmapLoaded;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoadPalette;
        private System.Windows.Forms.Button btnLoadBitmap;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnUnmangle;
        private System.Windows.Forms.Button btnConvertAudio;
    }
}

