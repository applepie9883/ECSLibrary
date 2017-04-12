using Microsoft.Xna.Framework;

namespace GM.ECSLibrary.Components
{
    public class PositionComponent : ComponentBase
    {
        private Vector2 _UpperLeft;

        public Vector2 UpperLeft
        {
            get
            {
                return _UpperLeft;
            }
            set
            {
                _UpperLeft = value;
            }
        }

        private Vector2 _PositionOffset;

        /// <summary>
        /// Used to set the offset of Position from UpperLeft.
        /// </summary>
        public Vector2 PositionOffset
        {
            get
            {
                return _PositionOffset;
            }
            set
            {
                _PositionOffset = value;
            }
        }

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
