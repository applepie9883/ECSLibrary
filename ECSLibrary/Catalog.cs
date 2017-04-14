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






        // This old code isn't compile time safe, so I replaced it with the safer (but much less flexible) code above
        /*
        private Dictionary<string, object> Contents { get; set; }

        public Catalog()
        {
            Contents = new Dictionary<string, object>();
        }

        public void SetEntry(string key, object entry)
        {
            if (HasEntry(key))
            {
                Contents[key] = entry;
            }
            else
            {
                Contents.Add(key, entry);
            }
        }

        public bool HasEntry(string key)
        {
            return Contents.ContainsKey(key);
        }

        public T GetEntry<T>(string key)
        {
            return (T)Contents[key];
        }
        */
    }
}
