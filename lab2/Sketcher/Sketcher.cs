using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Sketcher.Helpers;
using Sketcher.Helpers.LightProviders;
using Sketcher.Helpers.NormalVectorProviders;
using Sketcher.Models;
using Sketcher.Models.States;
using System.IO;
using System.Runtime.InteropServices;

namespace Sketcher
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeMessage
    {
        public IntPtr Handle;
        public uint Message;
        public IntPtr WParameter;
        public IntPtr LParameter;
        public uint Time;
        public Point Location;
    }

    public partial class Sketcher : Form
    {
        private readonly Stopwatch _stopwatch;

        public IState CurrentState { get; set; }
        public DirectBitmap Background { get; set; }
        public DirectBitmap PolygonBackground { get; set; }
        public DirectBitmap Heightmap { get; set; }

        public Color LightColor { get; set; } = Color.White;
        public ILightProvider LightProvider { get; set; } = new StaticInfinityLightProvider();
        public INormalVectorProvider NormalVectorProvider { get; set; }

        public Vertex ClickedVertex { get; set; }
        public Segment ClickedSegment { get; set; }
        public Polygon ClickedPolygon { get; set; }
        public List<Polygon> SelectedPolygons { get; } = new List<Polygon>();
        public LinkedList<Polygon> Polygons { get; } = new LinkedList<Polygon>();

        public double Kd { get; set; } = 0;
        public double Ks { get; set; } = 1;
        public double H { get; set; } = 50;
        public int M { get; set; } = 20;
        public Func<int, int, Vector3> ObserverVectorFormula { get; }

        public Sketcher()
        {
            InitializeComponent();
            CurrentState = new IdleState(this);
            Background = new DirectBitmap(DrawArea.Width, DrawArea.Height);
            NormalVectorProvider = new PlainNormalVectorProvider(DrawArea.Width, DrawArea.Height, Heightmap);
            ObserverVectorFormula = (x, y) => new Vector3(DrawArea.Width / 2 - x, DrawArea.Height / 2 - y, H);

            Application.Idle += HandleApplicationIdle;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\Images");
            openFileDialog.InitialDirectory = Path.GetFullPath(path);
            lightColorButton.BackColor = LightColor;

            General.PopulatePolygons(Polygons, DrawArea.Width, DrawArea.Height);
        }

        #region Main application loop

        [DllImport("user32.dll")]
        private static extern int PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax, uint remove);

        private static bool IsApplicationIdle()
        {
            NativeMessage result;
            return PeekMessage(out result, IntPtr.Zero, 0, 0, 0) == 0;
        }

        private void HandleApplicationIdle(object sender, EventArgs e)
        {
            while (IsApplicationIdle())
            {
                DrawArea.Invalidate();
                LightProvider.MoveLightSource();

                if (_stopwatch.ElapsedMilliseconds != 0)
                {
                    Text = $@"Sketcher - {1000 / _stopwatch.ElapsedMilliseconds} FPS";
                }
                _stopwatch.Restart();
            }
        }

        #endregion

        #region Key/Mouse Events

        private void DrawArea_KeyDown(object sender, KeyEventArgs e)
        {
            CurrentState.KeyDown(e);
        }

        private void DrawArea_MouseDown(object sender, MouseEventArgs e)
        {
            CurrentState.MouseDown(e);
        }

        private void DrawArea_MouseMove(object sender, MouseEventArgs e)
        {
            CurrentState.MouseMove(e);
        }

        private void DrawArea_MouseUp(object sender, MouseEventArgs e)
        {
            CurrentState.MouseUp(e);
        }

        #endregion

        #region Rendering

        private void DrawArea_SizeChanged(object sender, EventArgs e)
        {
            if (DrawArea.Width > 0 && DrawArea.Height > 0)
            {
                Background.Dispose();
                Background = new DirectBitmap(DrawArea.Width, DrawArea.Height);
                NormalVectorProvider.CalculateNormalVectors(DrawArea.Width, DrawArea.Height, Heightmap);
            }
        }

        private void DrawArea_Paint(object sender, PaintEventArgs e)
        {
            Renderer.RenderPolygons(this, Polygons);
            e.Graphics.DrawImage(Background.Bitmap, Point.Empty);
        }

        #endregion

        #region Showing Context Menus

        public void ShowVertexMenu(Point cursorPosition)
        {
            vertexMenu.Show(cursorPosition);
        }

        public void ShowSegmentMenu(Point cursorPosition)
        {
            segmentMenu.Show(cursorPosition);
        }

        public void ShowPolygonMenu(Point cursorPosition)
        {
            polygonMenu.Show(cursorPosition);
        }

        #endregion

        #region Context Menu Events

        private void deleteVertexMenuItem_Click(object sender, EventArgs e)
        {
            ClickedPolygon.DeleteVertex(ClickedVertex);
        }

        private void splitSegmentMenuItem_Click(object sender, EventArgs e)
        {
            ClickedPolygon.SplitSegment(ClickedSegment);
        }

        private void deletePolygonMenuItem_Click(object sender, EventArgs e)
        {
            SelectedPolygons.Remove(ClickedPolygon);
            Polygons.Remove(ClickedPolygon);
        }

        private void fillPolygonMenuItem_Click(object sender, EventArgs e)
        {
            ClickedPolygon.Filled = !ClickedPolygon.Filled;
        }

        private void selectPolygonMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedPolygons.Contains(ClickedPolygon))
            {
                SelectedPolygons.Remove(ClickedPolygon);
                ClickedPolygon.Deselect();
            }
            else if (SelectedPolygons.Count < 2)
            {
                SelectedPolygons.Add(ClickedPolygon);
                ClickedPolygon.Select();
            }
        }

        private void unionPolygonMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedPolygons.Count != 2)
            {
                MessageBox.Show(@"You must select two polygons", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = Geometry.Union(SelectedPolygons[0], SelectedPolygons[1]);
            if (result != null)
            {
                SelectedPolygons.ForEach(x => Polygons.Remove(x));
                SelectedPolygons.Clear();
                Polygons.AddLast(result);

                result.Filled = false;
                result.Deselect();
            }
            else
            {
                MessageBox.Show(@"Polygons are separable or self-intersecting", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Menu Opening

        private void polygonMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            fillMenuItem.Checked = ClickedPolygon.Filled;
            selectMenuItem.Checked = SelectedPolygons.Contains(ClickedPolygon);
        }

        #endregion

        #region Load/Remove Bitmaps

        private void loadBackgroundButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    PolygonBackground?.Dispose();
                    PolygonBackground = DirectBitmap.FromBitmap(new Bitmap(openFileDialog.FileName));
                }
                catch (Exception)
                {
                    MessageBox.Show(@"Could not create bitmap from file", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void loadHeightmapButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Heightmap?.Dispose();
                    Heightmap = DirectBitmap.FromBitmap(new Bitmap(openFileDialog.FileName));
                    NormalVectorProvider.CalculateNormalVectors(DrawArea.Width, DrawArea.Height, Heightmap);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"Could not create bitmap from file", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void removeBackgroundButton_Click(object sender, EventArgs e)
        {
            PolygonBackground?.Dispose();
            PolygonBackground = null;
        }

        private void removeHeightmapButton_Click(object sender, EventArgs e)
        {
            Heightmap?.Dispose();
            Heightmap = null;
            NormalVectorProvider.CalculateNormalVectors(DrawArea.Width, DrawArea.Height, Heightmap);
        }

        #endregion

        #region Light Modes

        private void staticInfinityLightButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!staticInfinityLightButton.Checked) return;
            LightProvider = new StaticInfinityLightProvider();
        }

        private void dynamicLightButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!dynamicLightButton.Checked) return;
            int x, y, z, cycle, radius;
            GetParams(out x, out y, out z, out cycle, out radius);
            LightProvider = new DynamicCircleLightProvider(x, y, z, cycle, radius);
        }

        private void lightTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && LightProvider is DynamicCircleLightProvider)
            {
                int x, y, z, cycle, radius;
                GetParams(out x, out y, out z, out cycle, out radius);
                ((DynamicCircleLightProvider)LightProvider).SetParams(x, y, z, cycle, radius);

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void GetParams(out int x, out int y, out int z, out int cycle, out int radius)
        {
            if (!int.TryParse(xTextBox.Text, out x))
            {
                x = DrawArea.Width / 2;
                xTextBox.Text = x.ToString();
            }
            if (!int.TryParse(yTextBox.Text, out y))
            {
                y = DrawArea.Height / 2;
                yTextBox.Text = y.ToString();
            }
            if (!int.TryParse(zTextBox.Text, out z))
            {
                z = Math.Min(x, y) / 4;
                zTextBox.Text = z.ToString();
            }
            if (!int.TryParse(cycleTextBox.Text, out cycle))
            {
                cycle = 5000;
                cycleTextBox.Text = cycle.ToString();
            }
            if (!int.TryParse(radiusTextBox.Text, out radius))
            {
                radius = z * 2;
                radiusTextBox.Text = radius.ToString();
            }
        }

        private void lightColorButton_Click(object sender, EventArgs e)
        {
            colorDialog.Color = LightColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                LightColor = colorDialog.Color;
                lightColorButton.BackColor = colorDialog.Color;
            }
        }

        #endregion

        #region Normal Vector Modes

        private void plainNormalVectorButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!plainNormalVectorButton.Checked) return;
            NormalVectorProvider = new PlainNormalVectorProvider(DrawArea.Width, DrawArea.Height, Heightmap);
        }

        private void paraboloidNormalVectorButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!paraboloidNormalVectorButton.Checked) return;
            NormalVectorProvider = new ParaboloidNormalVectorProvider(DrawArea.Width, DrawArea.Height, Heightmap);
        }

        private void trippyCirclesNormalVectorButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!trippyCirclesNormalVectorButton.Checked) return;
            NormalVectorProvider = new TrippyCirclesNormalVectorProvider(DrawArea.Width, DrawArea.Height, Heightmap);
        }

        private void pyramidNormalVectorButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!pyramidNormalVectorButton.Checked) return;
            NormalVectorProvider = new PyramidNormalVectorProvider(DrawArea.Width, DrawArea.Height, Heightmap);
        }

        #endregion

        #region Light Reflections

        private void kdTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            double kd;
            if (!double.TryParse(kdTextBox.Text, out kd))
            {
                kd = 0;
                kdTextBox.Text = kd.ToString();
            }
            if (kd < 0)
            {
                kd = 0;
                kdTextBox.Text = kd.ToString();
            }
            else if (kd > 1)
            {
                kd = 1;
                kdTextBox.Text = kd.ToString();
            }
            Kd = kd;
        }

        private void ksTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            double ks;
            if (!double.TryParse(ksTextBox.Text, out ks))
            {
                ks = 1;
                ksTextBox.Text = ks.ToString();
            }
            if (ks < 0)
            {
                ks = 0;
                ksTextBox.Text = ks.ToString();
            }
            else if (ks > 1)
            {
                ksTextBox.Text = ks.ToString();
                ks = 1;
            }
            Ks = ks;
        }

        private void hTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            double h;
            if (!double.TryParse(hTextBox.Text, out h))
            {
                h = 50;
                hTextBox.Text = h.ToString();
            }
            H = h;
        }

        private void mTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            int m;
            if (!int.TryParse(mTextBox.Text, out m))
            {
                m = 20;
                mTextBox.Text = m.ToString();
            }
            M = m;
        }

        #endregion
    }
}
