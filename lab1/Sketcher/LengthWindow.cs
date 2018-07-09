using System;
using System.Windows.Forms;
using Sketcher.Helpers;
using Sketcher.Models;
using Sketcher.Models.Constraints;

namespace Sketcher
{
    public partial class LengthWindow : Form
    {
        private readonly Segment _clickedSegment;
        private readonly Polygon _parentPolygon;

        public LengthWindow(Segment clickedSegment, Polygon parentPolygon)
        {
            InitializeComponent();
            _clickedSegment = clickedSegment;
            _parentPolygon = parentPolygon;
            lengthBox.Text = _clickedSegment.Length.ToString();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            int length;

            if (!int.TryParse((lengthBox.Text), out length))
            {
                MessageBox.Show(@"Please enter a valid number", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (length < 1 || length > 999)
            {
                MessageBox.Show(@"Please enter a number from 1 to 999", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var constraint = new LengthConstraint(_clickedSegment, length);
            ConstraintHelper.AddConstraint(constraint, _clickedSegment, _parentPolygon);

            Close();
        }
    }
}
