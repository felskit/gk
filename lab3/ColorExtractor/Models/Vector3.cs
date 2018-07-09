namespace ColorExtractor.Models
{
    public class Vector3
    {
        private readonly double[] _values;

        public double this[int i]
        {
            get { return _values[i]; }
            set { _values[i] = value; }
        }

        public Vector3()
        {
            _values = new double[3];
        }

        public Vector3(double x, double y, double z)
        {
            _values = new double[3];
            _values[0] = x;
            _values[1] = y;
            _values[2] = z;
        }

        public static Vector3 operator *(Vector3 v, double d)
        {
            var retVal = new Vector3();
            for (int i = 0; i < 3; i++)
            {
                retVal[i] = v[i] * d;
            }

            return retVal;
        }
    }
}