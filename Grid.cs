using System;

namespace Project1
{
    internal class Grid
    {
        public readonly Particle[,] Contents;
        public Grid(int width, int height)
        {
            Contents = new Particle[width, height];
        }

        public void Update()
        {
            Iterate((particle, x, y) =>
            {
                if (particle != null)
                {
                    Element.GenericBehavior(this, x, y);
                    Element.GetData(particle.Type).Behavior(this, x, y);
                }
            });
        }

        public void SetCell(int x, int y, Particle particle)
        {
            if (IsInsideGrid(x, y))
            {
                Contents[x, y] = particle;
            }
        }
        public Particle GetCell(int x, int y)
        {
            if (IsInsideGrid(x, y))
            {
                return Contents[x, y];
            }
            return null;
        }
        public bool TryGetCell(int x, int y, out Particle particle)
        {
            if (IsInsideGrid(x, y))
            {
                particle = Contents[x, y];
                return true;
            }
            particle = null;
            return false;
        }
        public bool IsInsideGrid(int x, int y)
        {
            return x >= 0 && x < Contents.GetLength(0) && y >= 0 && y < Contents.GetLength(1);
        }
        public bool IsCellEmpty(int x, int y)
        {
            return IsInsideGrid(x, y) && Contents[x, y] == null;
        }
        public bool IsCellOfType(ElementType element, int x, int y)
        {
            return IsInsideGrid(x, y) && Contents[x, y]?.Type == element;
        }
        public void Iterate(Action<Particle, int, int> action, bool reverseRows = false, bool reverseColumns = true)
        {
            if (reverseRows)
            {
                for (int i = Contents.GetLength(0) - 1; i > -1; i--)
                {
                    if (reverseColumns)
                    {
                        for (int j = Contents.GetLength(1) - 1; j > -1; j--)
                        {
                            action(Contents[i, j], i, j);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < Contents.GetLength(1); j++)
                        {
                            action(Contents[i, j], i, j);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < Contents.GetLength(0) - 1; i++)
                {
                    if (reverseColumns)
                    {
                        for (int j = Contents.GetLength(1) - 1; j > -1; j--)
                        {
                            action(Contents[i, j], i, j);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < Contents.GetLength(1); j++)
                        {
                            action(Contents[i, j], i, j);
                        }
                    }
                }
            }

        }
    }
}
