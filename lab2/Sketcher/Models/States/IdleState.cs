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

        public void KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && Keys.Modifiers.HasFlag(Keys.Control) && Keys.Modifiers.HasFlag(Keys.Shift))
            {
                _sketcher.SelectedPolygons.ForEach(x => x.Deselect());
                _sketcher.SelectedPolygons.Clear();
            }
        }

        public void MouseDown(MouseEventArgs e)
        {
            var clicked = new Vertex(e.X, e.Y);

            if ((e.Button & MouseButtons.Right) != 0)
            {
                foreach (var polygon in _sketcher.Polygons)
                {
                    foreach (var vertex in polygon.Vertices)
                    {
                        if (!vertex.IsEqualTo(clicked)) continue;
                        _sketcher.ClickedVertex = vertex;
                        _sketcher.ClickedPolygon = polygon;
                        _sketcher.ShowVertexMenu(Cursor.Position);
                        return;
                    }

                    foreach (var segment in polygon.Segments)
                    {
                        if (!segment.IsClicked(clicked)) continue;
                        _sketcher.ClickedSegment = segment;
                        _sketcher.ClickedPolygon = polygon;
                        _sketcher.ShowSegmentMenu(Cursor.Position);
                        return;
                    }

                    if (!polygon.Contains(clicked)) continue;
                    _sketcher.ClickedPolygon = polygon;
                    _sketcher.ShowPolygonMenu(Cursor.Position);
                    return;
                }

                return;
            }

            if ((e.Button & MouseButtons.Middle) != 0 || (e.Button & MouseButtons.Left) != 0 && Control.ModifierKeys.HasFlag(Keys.Shift))
            {
                foreach (var polygon in _sketcher.Polygons)
                {
                    if (!polygon.Contains(clicked)) continue;
                    _sketcher.Cursor = Cursors.NoMove2D;
                    _sketcher.CurrentState = new MovePolygonState(_sketcher, polygon, new Point(clicked.X, clicked.Y));
                    return;
                }
            }

            if ((e.Button & MouseButtons.Left) == 0) return;

            if (Control.ModifierKeys.HasFlag(Keys.Control))
            {
                foreach (var polygon in _sketcher.Polygons)
                {
                    if (!polygon.Contains(clicked)) continue;
                    if (_sketcher.SelectedPolygons.Contains(polygon))
                    {
                        _sketcher.SelectedPolygons.Remove(polygon);
                        polygon.Deselect();
                        return;
                    }
                    if (_sketcher.SelectedPolygons.Count >= 2) return;
                    _sketcher.SelectedPolygons.Add(polygon);
                    polygon.Select();
                    return;
                }
            }

            foreach (var polygon in _sketcher.Polygons)
            {
                foreach (var vertex in polygon.Vertices)
                {
                    if (!clicked.IsEqualTo(vertex)) continue;
                    _sketcher.Cursor = Cursors.NoMove2D;
                    _sketcher.CurrentState = new MoveVertexState(_sketcher, vertex);
                    return;
                }
            }

            var newPoly = new Polygon();
            newPoly.Vertices.AddLast(new Vertex(e.X, e.Y));
            _sketcher.Polygons.AddFirst(newPoly);
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
