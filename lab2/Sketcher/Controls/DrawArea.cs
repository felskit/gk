using System.Windows.Forms;

namespace Sketcher.Controls
{
    public sealed partial class DrawArea : UserControl
    {
        public DrawArea()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }
    }
}
