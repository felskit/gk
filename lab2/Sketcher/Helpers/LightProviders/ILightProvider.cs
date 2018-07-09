using System;
using Sketcher.Models;

namespace Sketcher.Helpers.LightProviders
{
    public interface ILightProvider
    {
        Func<int, int, Vector3> LightVectorFormula { get; }
        void MoveLightSource();
    }
}
