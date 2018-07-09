using System.Windows.Forms;
using Sketcher.Models;
using Sketcher.Models.Constraints;

namespace Sketcher.Helpers
{
    public static class ConstraintHelper
    {
        public static void AddConstraint(IConstraint constraint, Segment clickedSegment, Polygon parentPolygon)
        {
            if (constraint.CanApply())
            {
                clickedSegment.Constraint = constraint;
                parentPolygon.PreserveVertices();

                if (!parentPolygon.TryApplyConstraints(clickedSegment.From))
                {
                    clickedSegment.Constraint = null;
                    parentPolygon.RestoreVertices();
                    MessageBox.Show(@"Failed to add constraint", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(constraint.ErrorMessage, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
