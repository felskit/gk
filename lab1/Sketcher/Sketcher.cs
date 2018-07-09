using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Sketcher.Helpers;
using Sketcher.Models;
using Sketcher.Models.Constraints;
using Sketcher.Models.States;

namespace Sketcher
{
    public partial class Sketcher : Form
    {
        public IState CurrentState { get; set; }
        public Bitmap Background { get; set; }

        public Vertex ClickedVertex { get; set; }
        public Segment ClickedSegment { get; set; }
        public Polygon ParentPolygon { get; set; }
        public List<Polygon> Polygons { get; } = new List<Polygon>();

        public Sketcher()
        {
            InitializeComponent();
            CurrentState = new IdleState(this);
            Background = new Bitmap(ClientSize.Width, ClientSize.Height);
        }

        private void Sketcher_KeyUp(object sender, KeyEventArgs e)
        {
            CurrentState.KeyUp(e);
            Invalidate();
        }

        private void Sketcher_MouseDown(object sender, MouseEventArgs e)
        {
            CurrentState.MouseDown(e);
            Invalidate();
        }

        private void Sketcher_MouseMove(object sender, MouseEventArgs e)
        {
            CurrentState.MouseMove(e);
            Invalidate();
        }

        private void Sketcher_MouseUp(object sender, MouseEventArgs e)
        {
            CurrentState.MouseUp(e);
            Invalidate();
        }

        private void Sketcher_SizeChanged(object sender, EventArgs e)
        {
            if (ClientSize.Width > 0 && ClientSize.Height > 0)
            {
                Background.Dispose();
                Background = new Bitmap(ClientSize.Width, ClientSize.Height);
            }
        }

        private void Sketcher_Paint(object sender, PaintEventArgs e)
        {
            Renderer.RenderPolygons(this);
            e.Graphics.DrawImage(Background, new Point(0, 0));
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParentPolygon.DeleteVertex(ClickedVertex);
        }

        private void splitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParentPolygon.SplitSegment(ClickedSegment);
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClickedSegment.Constraint = null;
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var constraint = new HorizontalConstraint(ClickedSegment, ParentPolygon);
            ConstraintHelper.AddConstraint(constraint, ClickedSegment, ParentPolygon);
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var constraint = new VerticalConstraint(ClickedSegment, ParentPolygon);
            ConstraintHelper.AddConstraint(constraint, ClickedSegment, ParentPolygon);
        }

        private void lengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new LengthWindow(ClickedSegment, ParentPolygon).ShowDialog(this);
        }

        private void antialiasingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClickedSegment.Antialiased = !ClickedSegment.Antialiased;
        }

        private void segmentMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((ToolStripMenuItem)segmentMenu.Items[7]).Checked = ClickedSegment.Antialiased;
        }

        public void ShowVertexMenu(Point cursorPosition)
        {
            vertexMenu.Show(cursorPosition);
        }

        public void ShowSegmentMenu(Point cursorPosition)
        {
            segmentMenu.Show(cursorPosition);
        }
    }
}
