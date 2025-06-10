using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Project1
{
    public enum ElementType { SAND, WATER, STONE, BRICK, LAVA, GAS }
    internal struct ElementData()
    {
        public Color Color { get; set; }
        public int Viscosity { get; set; } = 1;
        public Action<Grid, int, int> Behavior { get; set; }
    }
    internal static class Element
    {
        private static readonly Random rnd = new();

        public static void GenericBehavior(Grid grid, int x, int y)
        {

        }

        public static void PowderBehavior(Grid grid, int x, int y)
        {
            if (grid.IsCellEmpty(x, y + 1))
            {
                grid.SetCell(x, y + 1, grid.GetCell(x, y));
                grid.SetCell(x, y, null);
            }
            else if (grid.IsCellEmpty(x - 1, y + 1))
            {
                grid.SetCell(x - 1, y + 1, grid.GetCell(x, y));
                grid.SetCell(x, y, null);
            }
            else if (grid.IsCellEmpty(x + 1, y + 1))
            {
                grid.SetCell(x + 1, y + 1, grid.GetCell(x, y));
                grid.SetCell(x, y, null);
            }
            else if (grid.IsCellOfType(ElementType.WATER, x, y + 1))
            {
                Particle swapBuffer = grid.GetCell(x, y + 1);
                grid.SetCell(x, y + 1, grid.GetCell(x, y));
                grid.SetCell(x, y, swapBuffer);
            }
            else if (grid.IsCellOfType(ElementType.WATER, x - 1, y + 1))
            {
                Particle swapBuffer = grid.GetCell(x - 1, y + 1);
                grid.SetCell(x - 1, y + 1, grid.GetCell(x, y));
                grid.SetCell(x, y, null);
            }
            else if (grid.IsCellOfType(ElementType.WATER, x + 1, y + 1))
            {
                Particle swapBuffer = grid.GetCell(x + 1, y + 1);
                grid.SetCell(x + 1, y + 1, grid.GetCell(x, y));
                grid.SetCell(x, y, swapBuffer);
            }
        }
        public static void LiquidBehavior(Grid grid, int x, int y)
        {
            ElementData data = GetData(grid.GetCell(x, y).Type);

            if (grid.IsCellEmpty(x, y + 1))
            {
                grid.SetCell(x, y + 1, grid.GetCell(x, y));
                grid.SetCell(x, y, null);
            }
            else
            {
                int direction = rnd.Next(2) * 2 - 1;

                for (int i = 1; i < data.Viscosity + 1; i++)
                {
                    if (grid.IsCellEmpty(x + i * direction, y))
                    {
                        grid.SetCell(x + i * direction, y, grid.GetCell(x, y));
                        grid.SetCell(x, y, null);
                    }
                    else if (!(grid.IsCellOfType(ElementType.WATER, x + i * direction, y))) break;
                }
            }
        }
        public static void GaseousBehavior(Grid grid, int x, int y)
        {

        }

        private static readonly Dictionary<ElementType, ElementData> data = new()
        {
            {
                ElementType.SAND,
                new ElementData()
                {
                    Color = new Color(252,225,102),
                    Behavior = PowderBehavior
                }
            },
            {
                ElementType.STONE,
                new ElementData()
                {
                    Color = Color.LightGray,
                    Behavior = PowderBehavior
                }
            },
            {
                ElementType.BRICK,
                new ElementData()
                {
                    Color = Color.DarkGray,
                    Behavior = (grid, x, y) => {}
                }
            },
            {
                ElementType.WATER,
                new ElementData()
                {
                    Color = Color.Blue,
                    Viscosity = 16,
                    Behavior = LiquidBehavior
                }
            },
            {
                ElementType.LAVA,
                new ElementData()
                {
                    Color = Color.MonoGameOrange,
                    Viscosity = 1,
                    Behavior = LiquidBehavior
                }
            },
            {
                ElementType.GAS,
                new ElementData()
                {
                    Color = Color.GreenYellow,
                    Behavior = (grid, x, y) => {
                        if (rnd.Next(2)==0)
                        {
                            var positions = new List<(int x, int y)>() {
                            (0, 1), (1,0), (1, 1), (-1, -1), (-1, 0), (0, -1), (1, -1), (-1, 1)
                        };
                        var availablePositions = new List<(int x, int y)>();

                        foreach (var (px, py) in positions)
                        {
                            if (grid.TryGetCell(x+px, y+py, out Particle neighborParticle))
                            {
                                if (neighborParticle == null)
                                {
                                   availablePositions.Add((px,py));
                                }
                            }
                        }

                        if (availablePositions.Count != 0)
                        {
                            var randomPos = availablePositions[rnd.Next(availablePositions.Count)];
                            grid.SetCell(x+randomPos.x, y+randomPos.y, grid.GetCell(x, y));
                            grid.SetCell(x, y, null);
                        }
                        }
                    }
                }
            }
        };

        public static ElementData GetData(ElementType type)
        {
            if (data.TryGetValue(type, out ElementData value))
            {
                return value;
            }
            return new ElementData()
            {
                Color = Color.Magenta,
                Behavior = (grid, x, y) => { }
            };
        }
    }
}
