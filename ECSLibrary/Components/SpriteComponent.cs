using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GM.ECSLibrary.Components
{
    /// <summary>
    /// Component containing the data required to draw a simple 2d image to the screen.
    /// </summary>
    public class SpriteComponent : ComponentBase
    {
        /// <summary>
        /// The texture used to draw the sprite.
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// The origin of rotation of the sprite.
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// The scale of the sprite.
        /// </summary>
        /// <example>
        /// (1, 1) is original size, (2, 2) is double both height and width.
        /// </example>
        public Vector2 Scale { get; set; }

        /// <summary>
        /// The rotation angle of the sprite, in degrees.
        /// </summary>
        public double RotationAngle { get; set; }
        
        /// <summary>
        /// The layer to draw the sprite on, with 0 being the back most layer, and 1 being the front most layer.
        /// </summary>
        public float LayerDepth { get; set; }

        /// <summary>
        /// The transparency of the sprite, from 0.0f (fully transparent) to 1.0f (fully opaque).
        /// </summary>
        public float Transparency { get; set; }

        /// <summary>
        /// The color to draw on any of the white parts of the sprite.
        /// </summary>
        public Color SpriteColor { get; set; }

        /// <summary>
        /// Default constructor, initializes all properties to default values. This does not generate a texture for the sprite.
        /// </summary>
        public SpriteComponent()
        {
            Origin = Vector2.Zero;
            Scale = Vector2.One;
            RotationAngle = 0;
            LayerDepth = 0;
            Transparency = 1;
            SpriteColor = Color.White;
        }
    }
}
