using System.Drawing;
using System.Windows.Forms;

namespace Sketcher.Models.States
{
    public class IdleState : IState
    {
        private readonly Sketcher _sketcher;

        public IdleState(Sketcher sketcher)
        {
            _sketcher = sketcher;
        }

        public void KeyUp(KeyEventArgs e)
        {

        }

        public void MouseDown(MouseEventArgs e)
        {
            var clicked = new Vertex(e.X, e.Y);

            if ((e.Button & MouseButtons.Right) != 0)
            {
                foreach (var poly in _sketcher.Polygons)
                {
                    foreach (var vertex in poly.Vertices)
                    {
                        if (!vertex.IsEqualTo(clicked)) continue;
                        _sketcher.ClickedVertex = vertex;
                        _sketcher.ParentPolygon = poly;
                        _sketcher.ShowVertexMenu(Cursor.Position);
                        return;
                    }

                    foreach (var segment in poly.Segments)
                    {
                        if (!segment.IsClicked(clicked)) continue;
                        _sketcher.ClickedSegment = segment;
                        _sketcher.ParentPolygon = poly;
                        _sketcher.ShowSegmentMenu(Cursor.Position);
                        return;
                    }
                }
                return;
            }

            if ((e.Button & MouseButtons.Middle) != 0 || (e.Button & MouseButtons.Left) != 0 && Control.ModifierKeys.HasFlag(Keys.Shift))
            {
                foreach (var poly in _sketcher.Polygons)
                {
                    if (!poly.IsGrabbed(clicked)) continue;
                    _sketcher.Cursor = Cursors.NoMove2D;
                    _sketcher.CurrentState = new MovePolygonState(_sketcher, poly, new Point(clicked.X, clicked.Y));
                    return;
                }
            }

            if ((e.Button & MouseButtons.Left) == 0) return;
            
            foreach (var poly in _sketcher.Polygons)
            {
                foreach (var vertex in poly.Vertices)
                {
                    if (!clicked.IsEqualTo(vertex)) continue;
                    _sketcher.Cursor = Cursors.NoMove2D;
                    _sketcher.CurrentState = new MoveVertexState(_sketcher, poly, vertex);
                    return;
                }
            }

            var newPoly = new Polygon();
            newPoly.Vertices.AddLast(new Vertex(e.X, e.Y));
            _sketcher.Polygons.Add(newPoly);
            _sketcher.CurrentState = new DrawPolygonState(_sketcher, newPoly);
        }

        public void MouseMove(MouseEventArgs e)
        {

        }

        public void MouseUp(MouseEventArgs e)
        {

        }
    }
}
