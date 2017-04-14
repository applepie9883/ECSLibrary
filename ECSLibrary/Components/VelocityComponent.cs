using Microsoft.Xna.Framework;
using System;

namespace GM.ECSLibrary.Components
{
    public class VelocityComponent : ComponentBase
    {
        public Vector2 Velocity { get; set; }

        public double Speed { get => Velocity.Length(); }

        public double DirectionDegrees { get => MathHelper.ToDegrees((float)Math.Atan2(-Velocity.Y, Velocity.X)); }

        public VelocityComponent()
        {
            Velocity = Vector2.Zero;
        }

        public void SetSpeedAndDirection(double speed, double directionDegrees)
        {
            double radians = MathHelper.ToRadians((float)directionDegrees);

            // Multiply sin by -1 to convert from conventional to computer coordinates
            Vector2 newVelocity = new Vector2((float)(speed * Math.Cos(radians)), (float)(speed * -Math.Sin(radians)));

            Velocity = newVelocity;
        }

        public void Accelerate(double speed, double directionDegrees)
        {
            Vector2 oldVelocity = Velocity;

            SetSpeedAndDirection(speed, directionDegrees);
            Velocity += oldVelocity;
        }
    }
}
