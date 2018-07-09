using System.Drawing;
using System.Windows.Forms;

namespace Sketcher.Models.States
{
    public class MovePolygonState : IState
    {
        private readonly Sketcher _sketcher;
        private readonly Polygon _polygonToMove;
        private Point _previousPosition;

        public MovePolygonState(Sketcher sketcher, Polygon polygonToMove, Point clickedVertex)
        {
            _sketcher = sketcher;
            _polygonToMove = polygonToMove;
            _previousPosition = clickedVertex;
        }

        public void KeyUp(KeyEventArgs e)
        {

        }

        public void MouseDown(MouseEventArgs e)
        {

        }

        public void MouseMove(MouseEventArgs e)
        {
            var xstep = e.X - _previousPosition.X;
            var ystep = e.Y - _previousPosition.Y;

            _previousPosition.X = e.X;
            _previousPosition.Y = e.Y;

            foreach (var vertex in _polygonToMove.Vertices)
            {
                vertex.X += xstep;
                vertex.Y += ystep;
            }
        }

        public void MouseUp(MouseEventArgs e)
        {
            _sketcher.Cursor = Cursors.Cross;
            _sketcher.CurrentState = new IdleState(_sketcher);
        }
    }
}
