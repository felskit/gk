namespace ColorExtractor.Helpers.Strategies
{
    public interface IStrategy
    {
        void ProcessPixel(int i, int j, double r, double g, double b, DirectBitmap[] channels);
    }
}
