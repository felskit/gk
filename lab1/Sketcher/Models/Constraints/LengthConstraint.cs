using System;

namespace Sketcher.Models.Constraints
{
    public class LengthConstraint : IConstraint
    {
        private readonly Segment _segment;

        public string ErrorMessage { get; } = @"Cannot apply this length";
        public int Length { get; }

        public LengthConstraint(Segment segment, int length)
        {
            _segment = segment;
            Length = length;
        }

        public bool CanApply()
        {
            return true;
        }

        public void Apply(bool forward)
        {
            #region offset
            //var dX = Math.Abs(_segment.To.X - _segment.From.X);
            //var dY = Math.Abs(_segment.To.Y - _segment.From.Y);
            //var len = Math.Sqrt(dX * dX + dY * dY);

            //var xx = (int)Math.Round(dX / len * Length - dX);
            //var yy = (int)Math.Round(dY / len * Length - dY);

            //if (_segment.From.X > _segment.To.X) xx *= -1;
            //if (_segment.From.Y > _segment.To.Y) yy *= -1;

            //if (forward)
            //{
            //    _segment.To.X += xx;
            //    _segment.To.Y += yy;
            //}
            //else
            //{
            //    _segment.From.X -= xx;
            //    _segment.From.Y -= yy;
            //}
#endregion

            var dX = _segment.To.X - _segment.From.X;
            var dY = _segment.To.Y - _segment.From.Y;
            var len = Math.Sqrt(dX * dX + dY * dY);

            var xx = (int)Math.Round(dX / len * Length);
            var yy = (int)Math.Round(dY / len * Length);

            if (forward)
            {
                _segment.To.X = _segment.From.X + xx;
                _segment.To.Y = _segment.From.Y + yy;
            }
            else
            {
                _segment.From.X = _segment.To.X - xx;
                _segment.From.Y = _segment.To.Y - yy;
            }
        }

        public bool Validate()
        {
            return Math.Abs(_segment.Length - Length) <= 1;
        }
    }
}
