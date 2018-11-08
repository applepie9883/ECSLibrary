using Microsoft.Xna.Framework;
using System;

namespace GM.ECSLibrary.Components
{
    public class VelocityComponent : ComponentBase
    {
        public float MovementMilliseconds { get; set; }

        public Vector2 Velocity { get; set; }

        public double Speed { get => Velocity.Length(); }

        public double DirectionRadians { get => Math.Atan2(-Velocity.Y, Velocity.X); }

        public VelocityComponent()
        {
            MovementMilliseconds = (1f / 60f) * 1000f;
            Velocity = Vector2.Zero;
        }

        public void SetSpeedAndDirection(double speed, double directionRadians)
        {
            // Multiply sin by -1 to convert from cartesian to computer coordinates
            Vector2 newVelocity = new Vector2((float)(speed * Math.Cos(directionRadians)), (float)(speed * -Math.Sin(directionRadians)));

            Velocity = newVelocity;
        }

        public void Accelerate(double speed, double directionRadians)
        {
            Vector2 oldVelocity = Velocity;

            SetSpeedAndDirection(speed, directionRadians);
            Velocity += oldVelocity;
        }

        public void Accelerate(GameTime gTime, double speed, double direction)
        {
            double elapsedMilliseconds = gTime.ElapsedGameTime.TotalMilliseconds;

            Accelerate(speed, direction);
        }
    }
}
