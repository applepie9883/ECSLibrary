using Microsoft.Xna.Framework;

namespace GM.ECSLibrary.Components
{
    public class PositionComponent : ComponentBase
    {
        public Vector2 UpperLeft { get; set; }

        /// <summary>
        /// Used to set the offset of Position from UpperLeft.
        /// </summary>
        public Vector2 PositionOffset { get; set; }

        public Vector2 Position
        {
            get
            {
                return UpperLeft + PositionOffset;
            }
            set
            {
                UpperLeft += value - Position;
            }
        }

        public PositionComponent()
        {
            UpperLeft = Vector2.Zero;
            PositionOffset = Vector2.Zero;
        }
    }
}
