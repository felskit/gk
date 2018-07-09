using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectCars.Models
{
    public class Vehicle
    {
        public readonly Vector3 InitialPosition;

        public Model Model;
        public Vector3 Position;
        public Quaternion Rotation;
        public float DrivingSpeed;

        public Vehicle(Vector3 initialPosition)
        {
            InitialPosition = initialPosition;
            Reset();
        }

        public void Drive()
        {
            var vector = Vector3.Transform(new Vector3(0, 0, -1), Rotation);
            Position += vector * DrivingSpeed;
        }

        public void Reset()
        {
            Position = InitialPosition;
            Rotation = Quaternion.Identity;
            DrivingSpeed = 0.0f;
        }
    }
}
