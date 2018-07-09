using System;
using Sketcher.Models;

namespace Sketcher.Helpers.LightProviders
{
    public class StaticInfinityLightProvider : ILightProvider
    {
        public Func<int, int, Vector3> LightVectorFormula { get; }

        public StaticInfinityLightProvider()
        {
            LightVectorFormula = (x, y) => new Vector3(0, 0, 1);  
        }

        public void MoveLightSource()
        {
            
        }
    }
}
