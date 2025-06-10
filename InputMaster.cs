using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Handlers;
using System;

namespace Project1
{
    internal class InputMaster
    {
        public bool IsPaused { get; private set; } = false;
        public MouseHandler MouseHandler { get; private set; }
        public KeyboardHandler KeyboardHandler { get; private set; }
        public InputMaster()
        {
            MouseHandler = new();
            KeyboardHandler = new();
        }

        public void Update(Grid grid, Brush brush)
        {
            MouseHandler.Update();
            KeyboardHandler.Update();

            if (!KeyboardHandler.State.IsKeyDown(Keys.LeftControl))
            {
                brush.Size += MouseHandler.ScrollDelta / 40;
            }

            if (KeyboardHandler.IsKeyJustDown(Keys.Space))
            {
                IsPaused = !IsPaused;
            }

            if (IsPaused)
            {
                if (KeyboardHandler.IsKeyJustDown(Keys.F))
                {
                    grid.Update();
                }
            }
            else
            {
                grid.Update();
            }

            if (KeyboardHandler.IsKeyJustDown(Keys.Tab))
            {
                BrushType[] allTypes = (BrushType[])Enum.GetValues(typeof(BrushType));
                int currentIndex = Array.IndexOf(allTypes, brush.Type);
                brush.Type = allTypes[(currentIndex + 1 + allTypes.Length) % allTypes.Length];
            }

            if (MouseHandler.ScrollDelta != 0 && KeyboardHandler.State.IsKeyDown(Keys.LeftControl))
            {
                ElementType[] allTypes = (ElementType[])Enum.GetValues(typeof(ElementType));
                int currentIndex = Array.IndexOf(allTypes, brush.PickedParticle);
                int direction = Math.Sign(MouseHandler.ScrollDelta);
                brush.PickedParticle = allTypes[(currentIndex + direction + allTypes.Length) % allTypes.Length];
            }

            if (MouseHandler.State.LeftButton == ButtonState.Pressed)
            {
                switch (brush.Type)
                {
                    case BrushType.Square:
                        for (int i = MouseHandler.State.Position.X / GameConstants.Scale - brush.Size; i < (MouseHandler.State.Position.X / GameConstants.Scale) + brush.Size; i++)
                        {
                            for (int j = MouseHandler.State.Position.Y / GameConstants.Scale - brush.Size; j < (MouseHandler.State.Position.Y / GameConstants.Scale) + brush.Size; j++)
                            {
                                if (grid.IsInsideGrid(i, j) && grid.Contents[i, j] == null)
                                {
                                    grid.Contents[i, j] = new Particle(brush.PickedParticle);
                                }
                            }

                        }
                        break;
                    case BrushType.Circle:
                        for (int i = MouseHandler.State.Position.X / GameConstants.Scale - brush.Size; i < (MouseHandler.State.Position.X / GameConstants.Scale) + brush.Size; i++)
                        {
                            for (int j = MouseHandler.State.Position.Y / GameConstants.Scale - brush.Size; j < (MouseHandler.State.Position.Y / GameConstants.Scale) + brush.Size; j++)
                            {
                                if (Helpers.IsInTheCircle(new Point(i, j), new Point(MouseHandler.State.Position.X / GameConstants.Scale, MouseHandler.State.Position.Y / GameConstants.Scale), brush.Size))
                                {
                                    if (grid.IsInsideGrid(i, j) && grid.Contents[i, j] == null)
                                    {
                                        grid.Contents[i, j] = new Particle(brush.PickedParticle);
                                    }

                                }
                            }

                        }
                        break;
                }

            }
            if (MouseHandler.State.RightButton == ButtonState.Pressed)
            {
                switch (brush.Type)
                {
                    case BrushType.Square:
                        for (int i = MouseHandler.State.Position.X / GameConstants.Scale - brush.Size; i < (MouseHandler.State.Position.X / GameConstants.Scale) + brush.Size; i++)
                        {
                            for (int j = MouseHandler.State.Position.Y / GameConstants.Scale - brush.Size; j < (MouseHandler.State.Position.Y / GameConstants.Scale) + brush.Size; j++)
                            {
                                if (grid.IsInsideGrid(i, j))
                                {
                                    grid.Contents[i, j] = null;
                                }
                            }

                        }
                        break;
                    case BrushType.Circle:
                        for (int i = MouseHandler.State.Position.X / GameConstants.Scale - brush.Size; i < (MouseHandler.State.Position.X / GameConstants.Scale) + brush.Size; i++)
                        {
                            for (int j = MouseHandler.State.Position.Y / GameConstants.Scale - brush.Size; j < (MouseHandler.State.Position.Y / GameConstants.Scale) + brush.Size; j++)
                            {
                                if (Helpers.IsInTheCircle(new Point(i, j), new Point(MouseHandler.State.Position.X / GameConstants.Scale, MouseHandler.State.Position.Y / GameConstants.Scale), brush.Size))
                                {
                                    if (grid.IsInsideGrid(i, j))
                                    {
                                        grid.Contents[i, j] = null;
                                    }

                                }
                            }

                        }
                        break;
                }
            }
        }
    }
}
