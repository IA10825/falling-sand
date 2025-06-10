using Microsoft.Xna.Framework;
using System;

namespace Project1
{
    internal class Helpers
    {
        public static Color MixColors(Color c1, Color c2)
        {
            int r = Math.Min((c1.R + c2.R), 255);
            int g = Math.Min((c1.G + c2.G), 255);
            int b = Math.Min((c1.B + c2.B), 255);

            return new Color(r, g, b);
        }
        public static Color GetRandomColor()
        {
            Random random = new Random();

            return new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        }

        public static bool IsInTheCircle(Point targetPosition, Point centerPosition, int radius)
        {
            return Math.Pow(centerPosition.X - targetPosition.X, 2) + Math.Pow(centerPosition.Y - targetPosition.Y, 2) <= Math.Pow(radius, 2);
        }
    }
}
