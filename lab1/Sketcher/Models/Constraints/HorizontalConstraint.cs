using System;

namespace Sketcher.Models.Constraints
{
    public class HorizontalConstraint : IConstraint
    {
        private const double Tangent80 = 5.6713;

        private readonly Segment _segment;
        private readonly Polygon _parentPolygon;

        public string ErrorMessage { get; } = @"This segment is not horizontal enough or adjacent segment is horizontal";

        public HorizontalConstraint(Segment segment, Polygon parentPolygon)
        {
            _segment = segment;
            _parentPolygon = parentPolygon;
        }

        public bool CanApply()
        {
            var dX = Math.Abs(_segment.To.X - _segment.From.X);
            var dY = Math.Abs(_segment.To.Y - _segment.From.Y);
            if ((double)dX / dY < Tangent80) return false;

            var snode = _parentPolygon.Segments.Find(_segment);
            if (snode == null) return false;

            var pnode = snode.Previous ?? _parentPolygon.Segments.Last;
            var nnode = snode.Next ?? _parentPolygon.Segments.First;
            return !(pnode.Value.Constraint is HorizontalConstraint) && !(nnode.Value.Constraint is HorizontalConstraint);
        }

        public void Apply(bool forward)
        {
            if (forward)
            {
                _segment.To.Y = _segment.From.Y;
            }
            else
            {
                _segment.From.Y = _segment.To.Y;
            }
        }

        public bool Validate()
        {
            return _segment.To.Y == _segment.From.Y;
        }
    }
}
