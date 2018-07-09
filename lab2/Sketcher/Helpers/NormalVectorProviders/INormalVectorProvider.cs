using Sketcher.Models;

namespace Sketcher.Helpers.NormalVectorProviders
{
    public interface INormalVectorProvider
    {
        Vector3 [,] NormalVectors { get; }
        void CalculateNormalVectors(int width, int height, DirectBitmap heightmap);
    }
}
