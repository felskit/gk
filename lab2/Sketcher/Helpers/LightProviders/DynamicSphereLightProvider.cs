using System;
using Sketcher.Models;

namespace Sketcher.Helpers.LightProviders
{
    public class DynamicSphereLightProvider : ILightProvider
    {
        private readonly Vector3 _lightSourcePosition = new Vector3(0, 0, 0);
        private readonly double _radius;
        private readonly int _width;
        private readonly double _step;
        private int _lightDirection = 1;

        public Func<int, int, Vector3> LightVectorFormula { get; }

        public DynamicSphereLightProvider(int width, int height)
        {
            LightVectorFormula = (x, y) => new Vector3(_lightSourcePosition.X - x, _lightSourcePosition.Y - y, _lightSourcePosition.Z);

            _radius = width / 2.0;
            _width = width;
            _step = width / 15.0;

            _lightSourcePosition.X = _radius;
            _lightSourcePosition.Y = height / 2.0;
            _lightSourcePosition.Z = _radius;
        }

        public void MoveLightSource()
        {
            if (_lightDirection == 1 && _lightSourcePosition.X + _step > _width ||
                _lightDirection == -1 && _lightSourcePosition.X - _step < 0) _lightDirection *= -1;

            var x = _lightSourcePosition.X + _step * _lightDirection - _radius;
            var z = (int)Math.Sqrt(_radius * _radius - x * x);

            _lightSourcePosition.X = x + _radius;
            _lightSourcePosition.Z = z;
        }
    }
}
