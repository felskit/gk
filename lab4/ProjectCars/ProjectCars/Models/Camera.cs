using Microsoft.Xna.Framework;

namespace ProjectCars.Models
{
    public class Camera
    {
        public readonly Vector3 AttachedOffset;
        public readonly Vector3 InitialStaticFixedPosition;
        public readonly Vector3 InitialStaticFollowingPosition;

        public CameraMode Mode = CameraMode.Attached;
        public float Angle;
        public Vector3 Offset;
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Up;

        public Camera(float xStatic, float zStatic, Vector3 attachedOffset)
        {
            AttachedOffset = attachedOffset;
            InitialStaticFixedPosition = new Vector3(xStatic, 8, zStatic);
            InitialStaticFollowingPosition = new Vector3(xStatic, 3, zStatic);
            Reset(true);
        }

        public void Reset(bool resetRotation = false)
        {
            Angle = 0.0f;
            Offset = new Vector3(0, 0, 0);
            if (resetRotation)
            {
                Rotation = Quaternion.Identity;
            }
        }

        public void ResetPosition()
        {
            switch (Mode)
            {
                case CameraMode.Attached:
                    Position = AttachedOffset;
                    break;
                case CameraMode.StaticFixed:
                    Position = InitialStaticFixedPosition;
                    break;
                case CameraMode.StaticFollowing:
                    Position = InitialStaticFollowingPosition;
                    break;
            }
        }
    }
}
