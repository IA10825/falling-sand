using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Project1
{
    internal class Debug
    {
        Dictionary<string, string> debug = new Dictionary<string, string>();
        public Debug() { }

        public void Update(string name, string value)
        {
            if (debug.ContainsKey(name))
            {
                debug[name] = value;
            }
            else
            {
                debug.Add(name, value);
            }

        }
        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch, SpriteFont _font)
        {
            int i = 0;
            foreach (var (key, value) in debug)
            {
                if (value != "")
                {
                    _spriteBatch.DrawString(_font, value, new Vector2(0, i * 15), Color.White);
                    i++;
                }
            }
        }
    }
}
