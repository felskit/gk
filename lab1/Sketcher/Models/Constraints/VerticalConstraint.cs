using System;

namespace Sketcher.Models.Constraints
{
    public class VerticalConstraint : IConstraint
    {
        private const double Tangent10 = 0.1763;

        private readonly Segment _segment;
        private readonly Polygon _parentPolygon;

        public string ErrorMessage { get; } = @"This segment is not vertical enough or adjacent segment is vertical";

        public VerticalConstraint(Segment segment, Polygon parentPolygon)
        {
            _segment = segment;
            _parentPolygon = parentPolygon;
        }

        public bool CanApply()
        {
            var dX = Math.Abs(_segment.To.X - _segment.From.X);
            var dY = Math.Abs(_segment.To.Y - _segment.From.Y);
            if ((double)dX / dY > Tangent10) return false;

            var snode = _parentPolygon.Segments.Find(_segment);
            if (snode == null) return false;

            var pnode = snode.Previous ?? _parentPolygon.Segments.Last;
            var nnode = snode.Next ?? _parentPolygon.Segments.First;
            return !(pnode.Value.Constraint is VerticalConstraint) && !(nnode.Value.Constraint is VerticalConstraint);
        }

        public void Apply(bool forward)
        {
            if (forward)
            {
                _segment.To.X = _segment.From.X;
            }
            else
            {
                _segment.From.X = _segment.To.X;
            }
        }

        public bool Validate()
        {
            return _segment.To.X == _segment.From.X;
        }
    }
}
