using System.Windows.Forms;

namespace Sketcher.Models.States
{
    public interface IState
    {
        void KeyDown(KeyEventArgs e);
        void MouseDown(MouseEventArgs e);
        void MouseMove(MouseEventArgs e);
        void MouseUp(MouseEventArgs e);
    }
}
