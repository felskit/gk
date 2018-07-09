using System.Windows.Forms;

namespace Sketcher.Models.States
{
    public class MoveVertexState : IState
    {
        private readonly Sketcher _sketcher;
        private readonly Polygon _parentPolygon;
        private readonly Vertex _vertexToMove;

        public MoveVertexState(Sketcher sketcher, Polygon parentPolygon, Vertex vertexToMove)
        {
            _sketcher = sketcher;
            _vertexToMove = vertexToMove;
            _parentPolygon = parentPolygon;
        }

        public void KeyUp(KeyEventArgs e)
        {

        }

        public void MouseDown(MouseEventArgs e)
        {

        }

        public void MouseMove(MouseEventArgs e)
        {
            _parentPolygon.PreserveVertices();
            _vertexToMove.X = e.X;
            _vertexToMove.Y = e.Y;
            if (!_parentPolygon.TryApplyConstraints(_vertexToMove))
            {
                _parentPolygon.RestoreVertices();
            }
        }

        public void MouseUp(MouseEventArgs e)
        {
            _sketcher.Cursor = Cursors.Cross;
            _sketcher.CurrentState = new IdleState(_sketcher);
        }
    }
}
