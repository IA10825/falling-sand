using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Project1
{
    internal static class Renderer
    {
        public static void Draw(GameTime gameTime, SpriteBatch _spriteBatch, Grid grid, Texture2D _pixel)
        {
            grid.Iterate((particle, x, y) =>
            {
                _spriteBatch.Draw(
                    _pixel,
                    new Rectangle(x * GameConstants.Scale, y * GameConstants.Scale, GameConstants.Scale, GameConstants.Scale),
                    particle == null ? Color.Black : Element.GetData(particle.Type).Color
                );
            });
        }
    }
}
