using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GM.ECSLibrary
{
    public class Catalog
    {
        public GraphicsDevice SharedGraphicsDevice { get; set; }

        public SpriteBatch SharedSpriteBatch { get; set; }

        public KeyboardState CurrentKeyboardState { get; set; }

        public KeyboardState OldKeyboardState { get; set; }

        public MouseState CurrentMouseState { get; set; }

        public MouseState OldMouseState { get; set; }

        public GameTime CurrentGameTime { get; set; }

        public SpriteFont DefaultFont { get; set; }
    }
}
