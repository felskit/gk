using ColorExtractor.Exceptions;
using ColorExtractor.Helpers;
using ColorExtractor.Helpers.Strategies;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ColorExtractor
{
    public struct ColorProfile
    {
        public double Rx;
        public double Ry;
        public double Gx;
        public double Gy;
        public double Bx;
        public double By;
        public double Wx;
        public double Wy;
        public double Gamma;
    }
    public struct ColorSpace
    {
        public decimal Rx;
        public decimal Ry;
        public decimal Gx;
        public decimal Gy;
        public decimal Bx;
        public decimal By;
        public decimal Gamma;
        public string Illuminant;
    }
    public struct Illuminant
    {
        public decimal Wx;
        public decimal Wy;
    }
    
    public partial class ColorExtractor : Form
    {
        private bool _lock;
        private DirectBitmap _directOriginal;
        private readonly ChannelSeparator _channelSeparator;
        private readonly Dictionary<string, ColorSpace> _predefinedColorSpaces = new Dictionary<string, ColorSpace>();
        private readonly Dictionary<string, Illuminant> _predefinedIlluminants = new Dictionary<string, Illuminant>();

        public ColorExtractor()
        {
            InitializeComponent();
            InitializeDictionaries();

            _channelSeparator = new ChannelSeparator(ch1PictureBox, ch2PictureBox, ch3PictureBox);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\Images");
            openFileDialog.InitialDirectory = Path.GetFullPath(path);

            modeComboBox.SelectedIndex = 0;
            colorProfileComboBox.SelectedIndex = 1;
        }

        private void InitializeDictionaries()
        {
            #region Illuminants
            _predefinedIlluminants.Add("A", new Illuminant { Wx = 0.44757M, Wy = 0.40744M });
            _predefinedIlluminants.Add("B", new Illuminant { Wx = 0.3484M, Wy = 0.3516M });
            _predefinedIlluminants.Add("C", new Illuminant { Wx = 0.31006M, Wy = 0.31615M });
            _predefinedIlluminants.Add("D50", new Illuminant { Wx = 0.34567M, Wy = 0.3585M });
            _predefinedIlluminants.Add("D55", new Illuminant { Wx = 0.33242M, Wy = 0.34743M });
            _predefinedIlluminants.Add("D65", new Illuminant { Wx = 0.31273M, Wy = 0.32902M });
            _predefinedIlluminants.Add("D75", new Illuminant { Wx = 0.29902M, Wy = 0.31485M });
            _predefinedIlluminants.Add("9300K", new Illuminant { Wx = 0.2848M, Wy = 0.2932M });
            _predefinedIlluminants.Add("E", new Illuminant { Wx = 0.333333M, Wy = 0.333333M });
            _predefinedIlluminants.Add("F2", new Illuminant { Wx = 0.37207M, Wy = 0.37207M });
            _predefinedIlluminants.Add("F7", new Illuminant { Wx = 0.31285M, Wy = 0.32918M });
            _predefinedIlluminants.Add("F11", new Illuminant { Wx = 0.38054M, Wy = 0.37691M });
            #endregion

            #region Color Spaces
            _predefinedColorSpaces.Add("sRGB", new ColorSpace
            {
                Rx = 0.64M,
                Ry = 0.33M,
                Gx = 0.30M,
                Gy = 0.60M,
                Bx = 0.15M,
                By = 0.06M,
                Gamma = 2.2M,
                Illuminant = "D65"
            });
            _predefinedColorSpaces.Add("AdobeRGB", new ColorSpace
            {
                Rx = 0.64M,
                Ry = 0.33M,
                Gx = 0.21M,
                Gy = 0.71M,
                Bx = 0.15M,
                By = 0.06M,
                Gamma = 2.2M,
                Illuminant = "D65"
            });
            _predefinedColorSpaces.Add("AppleRGB", new ColorSpace
            {
                Rx = 0.625M,
                Ry = 0.34M,
                Gx = 0.28M,
                Gy = 0.595M,
                Bx = 0.155M,
                By = 0.07M,
                Gamma = 1.8M,
                Illuminant = "D65"
            });
            _predefinedColorSpaces.Add("CIE RGB", new ColorSpace
            {
                Rx = 0.735M,
                Ry = 0.265M,
                Gx = 0.274M,
                Gy = 0.717M,
                Bx = 0.167M,
                By = 0.007M,
                Gamma = 2.2M,
                Illuminant = "E"
            });
            _predefinedColorSpaces.Add("Wide Gamut", new ColorSpace
            {
                Rx = 0.7347M,
                Ry = 0.2653M,
                Gx = 0.1152M,
                Gy = 0.8264M,
                Bx = 0.1566M,
                By = 0.0177M,
                Gamma = 1.2M,
                Illuminant = "D50"
            });
            _predefinedColorSpaces.Add("PAL/SECAM", new ColorSpace
            {
                Rx = 0.64M,
                Ry = 0.33M,
                Gx = 0.29M,
                Gy = 0.60M,
                Bx = 0.15M,
                By = 0.06M,
                Gamma = 1.95M,
                Illuminant = "D65"
            });
            #endregion
        }

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _directOriginal?.Dispose();
                    _directOriginal = DirectBitmap.FromBitmap(new Bitmap(openFileDialog.FileName));
                    mainPictureBox.BackgroundImage = _directOriginal.Bitmap;
                }
                catch (Exception)
                {
                    MessageBox.Show(@"Could not create bitmap from file", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void separateChannelsButton_Click(object sender, EventArgs e)
        {
            if (_directOriginal == null) return;

            switch (modeComboBox.Text)
            {
                case "RGB":
                {
                    ch1Label.Text = @"R";
                    ch2Label.Text = @"G";
                    ch3Label.Text = @"B";
                    _channelSeparator.SeparateChannels(_directOriginal, new RgbStrategy());
                    break;
                }
                case "HSV":
                {
                    ch1Label.Text = @"H";
                    ch2Label.Text = @"S";
                    ch3Label.Text = @"V";
                    _channelSeparator.SeparateChannels(_directOriginal, new HsvStrategy());
                    break;
                }
                case "YCbCr":
                {
                    ch1Label.Text = @"Y";
                    ch2Label.Text = @"Cb";
                    ch3Label.Text = @"Cr";
                    _channelSeparator.SeparateChannels(_directOriginal, new YCbCrStrategy());
                    break;
                }
                case "Lab":
                {
                    ch1Label.Text = @"L";
                    ch2Label.Text = @"a";
                    ch3Label.Text = @"b";
                    try
                    {
                        _channelSeparator.SeparateChannels(_directOriginal, new LabStrategy(GetColorProfile()));
                    }
                    catch (InvalidColorProfileException)
                    {
                        MessageBox.Show(@"Singular transformation matrix (possible invalid color profile)", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                }
            }
        }

        private void modeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleNumerics(modeComboBox.SelectedItem?.ToString() == "Lab");
        }

        private void colorProfileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var key = colorProfileComboBox.Text;
            if (_predefinedColorSpaces.ContainsKey(key))
            {
                _lock = true;
                var colorSpace = _predefinedColorSpaces[key];
                rxNumeric.Value = colorSpace.Rx;
                ryNumeric.Value = colorSpace.Ry;
                gxNumeric.Value = colorSpace.Gx;
                gyNumeric.Value = colorSpace.Gy;
                bxNumeric.Value = colorSpace.Bx;
                byNumeric.Value = colorSpace.By;
                gammaNumeric.Value = colorSpace.Gamma;
                illuminantComboBox.SelectedIndex = illuminantComboBox.Items.IndexOf(colorSpace.Illuminant);
                _lock = false;
            }
        }

        private void illuminantComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var illuminant = illuminantComboBox.Text;
            if (_predefinedIlluminants.ContainsKey(illuminant))
            {
                _lock = true;
                wxNumeric.Value = _predefinedIlluminants[illuminant].Wx;
                wyNumeric.Value = _predefinedIlluminants[illuminant].Wy;
                _lock = false;
            }

            var colorSpace = colorProfileComboBox.Text;
            if (_predefinedColorSpaces.ContainsKey(colorSpace) &&
                _predefinedColorSpaces[colorSpace].Illuminant != illuminant)
            {
                colorProfileComboBox.SelectedIndex = 0;
            }
        }

        private void colorProfileNumeric_ValueChanged(object sender, EventArgs e)
        {
            var key = colorProfileComboBox.Text;
            if (!_lock && _predefinedColorSpaces.ContainsKey(key))
            {
                var colorSpace = _predefinedColorSpaces[key];
                if (rxNumeric.Value != colorSpace.Rx || ryNumeric.Value != colorSpace.Ry ||
                    gxNumeric.Value != colorSpace.Gx || gyNumeric.Value != colorSpace.Gy ||
                    bxNumeric.Value != colorSpace.Bx || byNumeric.Value != colorSpace.By ||
                    gammaNumeric.Value != colorSpace.Gamma)
                {
                    colorProfileComboBox.SelectedIndex = 0;
                }
            }
        }

        private void illuminantNumeric_ValueChanged(object sender, EventArgs e)
        {
            var key = illuminantComboBox.Text;
            if (!_lock && _predefinedIlluminants.ContainsKey(key))
            {
                var illuminant = _predefinedIlluminants[key];
                if (wxNumeric.Value != illuminant.Wx || wyNumeric.Value != illuminant.Wy)
                {
                    illuminantComboBox.SelectedIndex = 0;
                    colorProfileComboBox.SelectedIndex = 0;
                }
            }
        }

        private ColorProfile GetColorProfile()
        {
            return new ColorProfile
            {
                Rx = (double)rxNumeric.Value, Ry = (double)ryNumeric.Value,
                Gx = (double)gxNumeric.Value, Gy = (double)gyNumeric.Value,
                Bx = (double)bxNumeric.Value, By = (double)byNumeric.Value,
                Wx = (double)wxNumeric.Value, Wy = (double)wyNumeric.Value,
                Gamma = (double)gammaNumeric.Value
            };
        }

        private void ToggleNumerics(bool enabled)
        {
            new[] { rxNumeric, ryNumeric, gxNumeric, gyNumeric, bxNumeric, byNumeric, wxNumeric, wyNumeric, gammaNumeric }.ToList().ForEach(x => x.Enabled = enabled);
            colorProfileComboBox.Enabled = enabled;
            illuminantComboBox.Enabled = enabled;
        }

        private void greyscaleButton_Click(object sender, EventArgs e)
        {
            if (_directOriginal == null) return;
            var greyscaleOriginal = new DirectBitmap(_directOriginal.Width, _directOriginal.Height);

            for (int i = 0; i < _directOriginal.Width; i++)
            {
                for (int j = 0; j < _directOriginal.Height; j++)
                {
                    var color = _directOriginal.GetPixel(i, j);
                    color = General.RgbMean(color);
                    color = General.ToGrayscaleArgb(color);
                    greyscaleOriginal.SetPixel(i, j, color);
                }
            }

            _directOriginal.Dispose();
            _directOriginal = greyscaleOriginal;
            mainPictureBox.BackgroundImage = _directOriginal.Bitmap;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ch1PictureBox.BackgroundImage?.Save(saveFileDialog.FileName + "_" + ch1Label.Text + ".bmp");
                    ch2PictureBox.BackgroundImage?.Save(saveFileDialog.FileName + "_" + ch2Label.Text + ".bmp");
                    ch3PictureBox.BackgroundImage?.Save(saveFileDialog.FileName + "_" + ch3Label.Text + ".bmp");
                }
                catch (Exception)
                {
                    MessageBox.Show(@"Could not save images to bitmap files", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
