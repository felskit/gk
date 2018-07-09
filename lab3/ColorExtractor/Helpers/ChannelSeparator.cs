using System.Drawing;
using System.Windows.Forms;
using ColorExtractor.Helpers.Strategies;

namespace ColorExtractor.Helpers
{
    public class ChannelSeparator
    {
        private readonly PictureBox[] _channelPictureBoxes = new PictureBox[3];
        private readonly DirectBitmap[] _channels = new DirectBitmap[3];

        public ChannelSeparator(PictureBox ch1PictureBox, PictureBox ch2PictureBox, PictureBox ch3PictureBox)
        {
            _channelPictureBoxes[0] = ch1PictureBox;
            _channelPictureBoxes[1] = ch2PictureBox;
            _channelPictureBoxes[2] = ch3PictureBox;
        }

        public void SeparateChannels(DirectBitmap directOriginal, IStrategy strategy)
        {
            InitializeChannels(directOriginal.Width, directOriginal.Height);

            for (int i = 0; i < directOriginal.Width; i++)
            {
                for (int j = 0; j < directOriginal.Height; j++)
                {
                    var color = directOriginal.GetPixel(i, j);
                    var r = (byte)(color >> 16) / 255.0;
                    var g = (byte)(color >> 8) / 255.0;
                    var b = (byte)color / 255.0;

                    strategy.ProcessPixel(i, j, r, g, b, _channels);
                }
            }

            SetAndDisposeChannels();
        }
        
        private void InitializeChannels(int width, int height)
        {
            for (int i = 0; i < 3; i++)
            {
                _channels[i] = new DirectBitmap(width, height);
            }
        }

        private void SetAndDisposeChannels()
        {
            for (int i = 0; i < 3; i++)
            {
                _channelPictureBoxes[i].BackgroundImage?.Dispose();
                _channelPictureBoxes[i].BackgroundImage = new Bitmap(_channels[i].Bitmap);
                _channels[i].Dispose();
            }
        }
    }
}
