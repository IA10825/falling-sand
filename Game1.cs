using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Handlers;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private Texture2D _pixel;
        private SpriteFont font;

        private readonly MouseHandler mouseHandler = new();
        private readonly KeyboardHandler keyboardHandler = new();
        private readonly InputMaster inputMaster = new();
        private Grid grid;
        private readonly Debug debug = new();
        private readonly Brush brush = new();

        public Point ScreenSize { get; private set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Powder Toy";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            grid = new(GraphicsDevice.Viewport.Width / GameConstants.Scale, GraphicsDevice.Viewport.Height / GameConstants.Scale);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            _pixel.SetData([Color.White]);
            font = Content.Load<SpriteFont>("main_font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            inputMaster.Update(grid, brush);

            debug.Update("Pause", inputMaster.IsPaused ? "Paused" : "");
            debug.Update("Screen size", ScreenSize.ToString());
            debug.Update("Mouse position", mouseHandler.State.Position.ToString());
            debug.Update("Brush size", brush.Size.ToString());
            debug.Update("Brush type", brush.Type.ToString());
            debug.Update("Picked element", brush.PickedParticle.ToString());
            if (grid.TryGetCell(mouseHandler.State.Position.X / GameConstants.Scale, mouseHandler.State.Position.Y / GameConstants.Scale, out Particle particle))
            {
                if (particle != null)
                {
                    debug.Update("Temperature", particle.Temperature.ToString() + "C");
                }
            }

            ScreenSize = new Point(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            Renderer.Draw(gameTime, _spriteBatch, grid, _pixel);
            debug.Draw(gameTime, _spriteBatch, font);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
