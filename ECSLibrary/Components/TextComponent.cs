using Microsoft.Xna.Framework;

namespace GM.ECSLibrary.Components
{
    public class TextComponent : ComponentBase
    {
        public string Text { get; set; }

        public Vector2 Offset { get; set; }

        public Color Color { get; set; }

        public TextComponent()
        {
            Text = string.Empty;
            Offset = Vector2.Zero;
            Color = Color.Black;
        }
    }
}
