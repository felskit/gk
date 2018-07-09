using System.Linq;
using System.Windows.Forms;

namespace Sketcher.Models.States
{
    public class DrawPolygonState : IState
    {
        private readonly Sketcher _sketcher;
        private readonly Polygon _polygonToDraw;

        public DrawPolygonState(Sketcher sketcher, Polygon polygonToDraw)
        {
            _sketcher = sketcher;
            _polygonToDraw = polygonToDraw;
        }

        public void KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _sketcher.Polygons.Remove(_polygonToDraw);
                _sketcher.CurrentState = new IdleState(_sketcher);
            }
        }

        public void MouseDown(MouseEventArgs e)
        {

        }

        public void MouseMove(MouseEventArgs e)
        {
            _polygonToDraw.LastVertex.X = e.X;
            _polygonToDraw.LastVertex.Y = e.Y;
        }

        public void MouseUp(MouseEventArgs e)
        {
            if (_polygonToDraw.LastVertex.IsEqualTo(_polygonToDraw.FirstVertex) && _polygonToDraw.Vertices.Count > 3)
            {
                _polygonToDraw.Vertices.Remove(_polygonToDraw.LastVertex);
                _polygonToDraw.Segments.Last().To = _polygonToDraw.FirstVertex;
                _sketcher.CurrentState = new IdleState(_sketcher);
            }
            else
            {
                var clicked = new Vertex(e.X, e.Y);
                _polygonToDraw.Segments.AddLast(new Segment(_polygonToDraw.LastVertex, clicked));
                _polygonToDraw.Vertices.AddLast(clicked);
            }
        }
    }
}
