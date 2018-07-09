namespace ColorExtractor
{
    partial class ColorExtractor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorExtractor));
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            this.ch1PictureBox = new System.Windows.Forms.PictureBox();
            this.ch2PictureBox = new System.Windows.Forms.PictureBox();
            this.ch3PictureBox = new System.Windows.Forms.PictureBox();
            this.ch1Label = new System.Windows.Forms.Label();
            this.ch2Label = new System.Windows.Forms.Label();
            this.ch3Label = new System.Windows.Forms.Label();
            this.loadImageButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.modeComboBox = new System.Windows.Forms.ComboBox();
            this.separateChannelsButton = new System.Windows.Forms.Button();
            this.colorProfileComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labGroupBox = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.gammaNumeric = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.illuminantComboBox = new System.Windows.Forms.ComboBox();
            this.wyNumeric = new System.Windows.Forms.NumericUpDown();
            this.wxNumeric = new System.Windows.Forms.NumericUpDown();
            this.byNumeric = new System.Windows.Forms.NumericUpDown();
            this.bxNumeric = new System.Windows.Forms.NumericUpDown();
            this.gyNumeric = new System.Windows.Forms.NumericUpDown();
            this.gxNumeric = new System.Windows.Forms.NumericUpDown();
            this.ryNumeric = new System.Windows.Forms.NumericUpDown();
            this.rxNumeric = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.greyscaleButton = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ch1PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ch2PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ch3PictureBox)).BeginInit();
            this.labGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gammaNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wyNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wxNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.byNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bxNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gyNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gxNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ryNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rxNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPictureBox
            // 
            this.mainPictureBox.BackColor = System.Drawing.Color.White;
            this.mainPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mainPictureBox.Location = new System.Drawing.Point(12, 12);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.Size = new System.Drawing.Size(600, 450);
            this.mainPictureBox.TabIndex = 0;
            this.mainPictureBox.TabStop = false;
            // 
            // ch1PictureBox
            // 
            this.ch1PictureBox.BackColor = System.Drawing.Color.White;
            this.ch1PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ch1PictureBox.Location = new System.Drawing.Point(12, 492);
            this.ch1PictureBox.Name = "ch1PictureBox";
            this.ch1PictureBox.Size = new System.Drawing.Size(400, 300);
            this.ch1PictureBox.TabIndex = 1;
            this.ch1PictureBox.TabStop = false;
            // 
            // ch2PictureBox
            // 
            this.ch2PictureBox.BackColor = System.Drawing.Color.White;
            this.ch2PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ch2PictureBox.Location = new System.Drawing.Point(418, 492);
            this.ch2PictureBox.Name = "ch2PictureBox";
            this.ch2PictureBox.Size = new System.Drawing.Size(400, 300);
            this.ch2PictureBox.TabIndex = 2;
            this.ch2PictureBox.TabStop = false;
            // 
            // ch3PictureBox
            // 
            this.ch3PictureBox.BackColor = System.Drawing.Color.White;
            this.ch3PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ch3PictureBox.Location = new System.Drawing.Point(824, 492);
            this.ch3PictureBox.Name = "ch3PictureBox";
            this.ch3PictureBox.Size = new System.Drawing.Size(400, 300);
            this.ch3PictureBox.TabIndex = 3;
            this.ch3PictureBox.TabStop = false;
            // 
            // ch1Label
            // 
            this.ch1Label.AutoSize = true;
            this.ch1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ch1Label.Location = new System.Drawing.Point(12, 471);
            this.ch1Label.Name = "ch1Label";
            this.ch1Label.Size = new System.Drawing.Size(27, 20);
            this.ch1Label.TabIndex = 4;
            this.ch1Label.Text = " R";
            // 
            // ch2Label
            // 
            this.ch2Label.AutoSize = true;
            this.ch2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ch2Label.Location = new System.Drawing.Point(415, 471);
            this.ch2Label.Name = "ch2Label";
            this.ch2Label.Size = new System.Drawing.Size(28, 20);
            this.ch2Label.TabIndex = 5;
            this.ch2Label.Text = " G";
            // 
            // ch3Label
            // 
            this.ch3Label.AutoSize = true;
            this.ch3Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ch3Label.Location = new System.Drawing.Point(821, 471);
            this.ch3Label.Name = "ch3Label";
            this.ch3Label.Size = new System.Drawing.Size(26, 20);
            this.ch3Label.TabIndex = 6;
            this.ch3Label.Text = " B";
            // 
            // loadImageButton
            // 
            this.loadImageButton.Location = new System.Drawing.Point(619, 135);
            this.loadImageButton.Name = "loadImageButton";
            this.loadImageButton.Size = new System.Drawing.Size(120, 26);
            this.loadImageButton.TabIndex = 7;
            this.loadImageButton.Text = "Load Image";
            this.loadImageButton.UseVisualStyleBackColor = true;
            this.loadImageButton.Click += new System.EventHandler(this.loadImageButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "Image files|*.bmp;*.png;*.jpg;*.jpeg";
            this.openFileDialog.InitialDirectory = "ColorExtractor/Images/";
            // 
            // modeComboBox
            // 
            this.modeComboBox.FormattingEnabled = true;
            this.modeComboBox.Items.AddRange(new object[] {
            "RGB",
            "HSV",
            "YCbCr",
            "Lab"});
            this.modeComboBox.Location = new System.Drawing.Point(619, 167);
            this.modeComboBox.Name = "modeComboBox";
            this.modeComboBox.Size = new System.Drawing.Size(120, 21);
            this.modeComboBox.TabIndex = 8;
            this.modeComboBox.SelectedIndexChanged += new System.EventHandler(this.modeComboBox_SelectedIndexChanged);
            // 
            // separateChannelsButton
            // 
            this.separateChannelsButton.Location = new System.Drawing.Point(619, 194);
            this.separateChannelsButton.Name = "separateChannelsButton";
            this.separateChannelsButton.Size = new System.Drawing.Size(120, 26);
            this.separateChannelsButton.TabIndex = 9;
            this.separateChannelsButton.Text = "Separate Channels";
            this.separateChannelsButton.UseVisualStyleBackColor = true;
            this.separateChannelsButton.Click += new System.EventHandler(this.separateChannelsButton_Click);
            // 
            // colorProfileComboBox
            // 
            this.colorProfileComboBox.FormattingEnabled = true;
            this.colorProfileComboBox.Items.AddRange(new object[] {
            "Custom",
            "sRGB",
            "AdobeRGB",
            "AppleRGB",
            "CIE RGB",
            "Wide Gamut",
            "PAL/SECAM"});
            this.colorProfileComboBox.Location = new System.Drawing.Point(127, 19);
            this.colorProfileComboBox.Name = "colorProfileComboBox";
            this.colorProfileComboBox.Size = new System.Drawing.Size(103, 21);
            this.colorProfileComboBox.TabIndex = 10;
            this.colorProfileComboBox.SelectedIndexChanged += new System.EventHandler(this.colorProfileComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Red primary";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Green primary";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Blue primary";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "White point";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(81, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = " x";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(157, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = " y";
            // 
            // labGroupBox
            // 
            this.labGroupBox.Controls.Add(this.label10);
            this.labGroupBox.Controls.Add(this.gammaNumeric);
            this.labGroupBox.Controls.Add(this.label9);
            this.labGroupBox.Controls.Add(this.illuminantComboBox);
            this.labGroupBox.Controls.Add(this.wyNumeric);
            this.labGroupBox.Controls.Add(this.wxNumeric);
            this.labGroupBox.Controls.Add(this.byNumeric);
            this.labGroupBox.Controls.Add(this.bxNumeric);
            this.labGroupBox.Controls.Add(this.gyNumeric);
            this.labGroupBox.Controls.Add(this.gxNumeric);
            this.labGroupBox.Controls.Add(this.ryNumeric);
            this.labGroupBox.Controls.Add(this.rxNumeric);
            this.labGroupBox.Controls.Add(this.label8);
            this.labGroupBox.Controls.Add(this.label7);
            this.labGroupBox.Controls.Add(this.label6);
            this.labGroupBox.Controls.Add(this.colorProfileComboBox);
            this.labGroupBox.Controls.Add(this.label5);
            this.labGroupBox.Controls.Add(this.label1);
            this.labGroupBox.Controls.Add(this.label4);
            this.labGroupBox.Controls.Add(this.label2);
            this.labGroupBox.Controls.Add(this.label3);
            this.labGroupBox.Location = new System.Drawing.Point(618, 226);
            this.labGroupBox.Name = "labGroupBox";
            this.labGroupBox.Size = new System.Drawing.Size(238, 236);
            this.labGroupBox.TabIndex = 18;
            this.labGroupBox.TabStop = false;
            this.labGroupBox.Text = "Lab Settings";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(111, 208);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 38;
            this.label10.Text = "Gamma";
            // 
            // gammaNumeric
            // 
            this.gammaNumeric.DecimalPlaces = 2;
            this.gammaNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.gammaNumeric.Location = new System.Drawing.Point(160, 206);
            this.gammaNumeric.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            65536});
            this.gammaNumeric.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            65536});
            this.gammaNumeric.Name = "gammaNumeric";
            this.gammaNumeric.Size = new System.Drawing.Size(70, 20);
            this.gammaNumeric.TabIndex = 37;
            this.gammaNumeric.Value = new decimal(new int[] {
            12,
            0,
            0,
            65536});
            this.gammaNumeric.ValueChanged += new System.EventHandler(this.colorProfileNumeric_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Predefined illuminant";
            // 
            // illuminantComboBox
            // 
            this.illuminantComboBox.FormattingEnabled = true;
            this.illuminantComboBox.Items.AddRange(new object[] {
            "Custom",
            "A",
            "B",
            "C",
            "D50",
            "D55",
            "D65",
            "D75",
            "9300K",
            "E",
            "F2",
            "F7",
            "F11"});
            this.illuminantComboBox.Location = new System.Drawing.Point(127, 46);
            this.illuminantComboBox.Name = "illuminantComboBox";
            this.illuminantComboBox.Size = new System.Drawing.Size(103, 21);
            this.illuminantComboBox.TabIndex = 35;
            this.illuminantComboBox.SelectedIndexChanged += new System.EventHandler(this.illuminantComboBox_SelectedIndexChanged);
            // 
            // wyNumeric
            // 
            this.wyNumeric.DecimalPlaces = 6;
            this.wyNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.wyNumeric.Location = new System.Drawing.Point(160, 180);
            this.wyNumeric.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.wyNumeric.Name = "wyNumeric";
            this.wyNumeric.Size = new System.Drawing.Size(70, 20);
            this.wyNumeric.TabIndex = 34;
            this.wyNumeric.ValueChanged += new System.EventHandler(this.illuminantNumeric_ValueChanged);
            // 
            // wxNumeric
            // 
            this.wxNumeric.DecimalPlaces = 6;
            this.wxNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.wxNumeric.Location = new System.Drawing.Point(85, 180);
            this.wxNumeric.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.wxNumeric.Name = "wxNumeric";
            this.wxNumeric.Size = new System.Drawing.Size(69, 20);
            this.wxNumeric.TabIndex = 33;
            this.wxNumeric.ValueChanged += new System.EventHandler(this.illuminantNumeric_ValueChanged);
            // 
            // byNumeric
            // 
            this.byNumeric.DecimalPlaces = 6;
            this.byNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.byNumeric.Location = new System.Drawing.Point(160, 154);
            this.byNumeric.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.byNumeric.Name = "byNumeric";
            this.byNumeric.Size = new System.Drawing.Size(70, 20);
            this.byNumeric.TabIndex = 32;
            this.byNumeric.ValueChanged += new System.EventHandler(this.colorProfileNumeric_ValueChanged);
            // 
            // bxNumeric
            // 
            this.bxNumeric.DecimalPlaces = 6;
            this.bxNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.bxNumeric.Location = new System.Drawing.Point(85, 154);
            this.bxNumeric.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.bxNumeric.Name = "bxNumeric";
            this.bxNumeric.Size = new System.Drawing.Size(69, 20);
            this.bxNumeric.TabIndex = 31;
            this.bxNumeric.ValueChanged += new System.EventHandler(this.colorProfileNumeric_ValueChanged);
            // 
            // gyNumeric
            // 
            this.gyNumeric.DecimalPlaces = 6;
            this.gyNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.gyNumeric.Location = new System.Drawing.Point(160, 128);
            this.gyNumeric.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.gyNumeric.Name = "gyNumeric";
            this.gyNumeric.Size = new System.Drawing.Size(70, 20);
            this.gyNumeric.TabIndex = 30;
            this.gyNumeric.ValueChanged += new System.EventHandler(this.colorProfileNumeric_ValueChanged);
            // 
            // gxNumeric
            // 
            this.gxNumeric.DecimalPlaces = 6;
            this.gxNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.gxNumeric.Location = new System.Drawing.Point(85, 128);
            this.gxNumeric.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.gxNumeric.Name = "gxNumeric";
            this.gxNumeric.Size = new System.Drawing.Size(69, 20);
            this.gxNumeric.TabIndex = 29;
            this.gxNumeric.ValueChanged += new System.EventHandler(this.colorProfileNumeric_ValueChanged);
            // 
            // ryNumeric
            // 
            this.ryNumeric.DecimalPlaces = 6;
            this.ryNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.ryNumeric.Location = new System.Drawing.Point(160, 102);
            this.ryNumeric.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ryNumeric.Name = "ryNumeric";
            this.ryNumeric.Size = new System.Drawing.Size(70, 20);
            this.ryNumeric.TabIndex = 28;
            this.ryNumeric.ValueChanged += new System.EventHandler(this.colorProfileNumeric_ValueChanged);
            // 
            // rxNumeric
            // 
            this.rxNumeric.DecimalPlaces = 6;
            this.rxNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.rxNumeric.Location = new System.Drawing.Point(84, 102);
            this.rxNumeric.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rxNumeric.Name = "rxNumeric";
            this.rxNumeric.Size = new System.Drawing.Size(70, 20);
            this.rxNumeric.TabIndex = 27;
            this.rxNumeric.ValueChanged += new System.EventHandler(this.colorProfileNumeric_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Chromaticity";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Predefined color profile";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(1104, 439);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(120, 23);
            this.saveButton.TabIndex = 19;
            this.saveButton.Text = "Save Output";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // greyscaleButton
            // 
            this.greyscaleButton.Location = new System.Drawing.Point(619, 13);
            this.greyscaleButton.Name = "greyscaleButton";
            this.greyscaleButton.Size = new System.Drawing.Size(120, 23);
            this.greyscaleButton.TabIndex = 20;
            this.greyscaleButton.Text = "To Greyscale";
            this.greyscaleButton.UseVisualStyleBackColor = true;
            this.greyscaleButton.Click += new System.EventHandler(this.greyscaleButton_Click);
            // 
            // ColorExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1236, 804);
            this.Controls.Add(this.greyscaleButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.labGroupBox);
            this.Controls.Add(this.separateChannelsButton);
            this.Controls.Add(this.modeComboBox);
            this.Controls.Add(this.loadImageButton);
            this.Controls.Add(this.ch3Label);
            this.Controls.Add(this.ch2Label);
            this.Controls.Add(this.ch1Label);
            this.Controls.Add(this.ch3PictureBox);
            this.Controls.Add(this.ch2PictureBox);
            this.Controls.Add(this.ch1PictureBox);
            this.Controls.Add(this.mainPictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ColorExtractor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Color Extractor";
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ch1PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ch2PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ch3PictureBox)).EndInit();
            this.labGroupBox.ResumeLayout(false);
            this.labGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gammaNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wyNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wxNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.byNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bxNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gyNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gxNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ryNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rxNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mainPictureBox;
        private System.Windows.Forms.PictureBox ch1PictureBox;
        private System.Windows.Forms.PictureBox ch2PictureBox;
        private System.Windows.Forms.PictureBox ch3PictureBox;
        private System.Windows.Forms.Label ch1Label;
        private System.Windows.Forms.Label ch2Label;
        private System.Windows.Forms.Label ch3Label;
        private System.Windows.Forms.Button loadImageButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ComboBox modeComboBox;
        private System.Windows.Forms.Button separateChannelsButton;
        private System.Windows.Forms.ComboBox colorProfileComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox labGroupBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown wyNumeric;
        private System.Windows.Forms.NumericUpDown wxNumeric;
        private System.Windows.Forms.NumericUpDown byNumeric;
        private System.Windows.Forms.NumericUpDown bxNumeric;
        private System.Windows.Forms.NumericUpDown gyNumeric;
        private System.Windows.Forms.NumericUpDown gxNumeric;
        private System.Windows.Forms.NumericUpDown ryNumeric;
        private System.Windows.Forms.NumericUpDown rxNumeric;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox illuminantComboBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown gammaNumeric;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button greyscaleButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

