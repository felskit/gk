using System;
using System.Diagnostics;
using Sketcher.Models;

namespace Sketcher.Helpers.LightProviders
{
    public class DynamicCircleLightProvider : ILightProvider
    {
        private readonly Stopwatch _stopwatch;
        private readonly Vector3 _lightSourcePosition = new Vector3(0, 0, 0);
        private double _lightAngle;

        public Func<int, int, Vector3> LightVectorFormula { get; }

        public int Cycle { get; set; }
        public int Radius { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public double Z
        {
            get { return _lightSourcePosition.Z; }
            set { _lightSourcePosition.Z = value; }
        }

        public DynamicCircleLightProvider(int x0, int y0, double z, int cycle, int radius)
        {
            LightVectorFormula = (x, y) => new Vector3(_lightSourcePosition.X - x, _lightSourcePosition.Y - y, _lightSourcePosition.Z);
            
            _lightSourcePosition.X = x0 + radius;
            _lightSourcePosition.Y = y0;
            _lightSourcePosition.Z = z;

            _stopwatch = new Stopwatch();
            _stopwatch.Start();

            Cycle = cycle / 2;
            Radius = radius;
            X = x0;
            Y = y0;
        }

        public void MoveLightSource()
        {
            var dt = _stopwatch.ElapsedMilliseconds;
            _stopwatch.Restart();

            _lightAngle = (_lightAngle + Math.PI * dt / Cycle) % (2 * Math.PI);
            _lightSourcePosition.X = X + Radius * Math.Cos(_lightAngle);
            _lightSourcePosition.Y = Y + Radius * Math.Sin(_lightAngle);
        }

        public void SetParams(int x, int y, double z, int cycle, int radius)
        {
            X = x;
            Y = y;
            Z = z;
            Cycle = cycle / 2;
            Radius = radius;
        }
    }
}
