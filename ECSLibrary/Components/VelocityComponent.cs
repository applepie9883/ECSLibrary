using Microsoft.Xna.Framework;

namespace GM.ECSLibrary.Components
{
    public class VelocityComponent : ComponentBase
    {
        public Vector2 Velocity { get; set; }

        public VelocityComponent()
        {
            Velocity = Vector2.Zero;
        }
    }
}
