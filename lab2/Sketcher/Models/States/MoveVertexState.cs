using System.Windows.Forms;

namespace Sketcher.Models.States
{
    public class MoveVertexState : IState
    {
        private readonly Sketcher _sketcher;
        private readonly Vertex _vertexToMove;

        public MoveVertexState(Sketcher sketcher, Vertex vertexToMove)
        {
            _sketcher = sketcher;
            _vertexToMove = vertexToMove;
        }

        public void KeyDown(KeyEventArgs e)
        {

        }

        public void MouseDown(MouseEventArgs e)
        {

        }

        public void MouseMove(MouseEventArgs e)
        {
            _vertexToMove.X = e.X;
            _vertexToMove.Y = e.Y;
        }

        public void MouseUp(MouseEventArgs e)
        {
            _sketcher.Cursor = Cursors.Cross;
            _sketcher.CurrentState = new IdleState(_sketcher);
        }
    }
}
