using System;



namespace Project1
{
    internal class Particle
    {
        public ElementType Type;

        private float temperature;

        private Random random = new();

        public float Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }

        public Particle(ElementType type)
        {
            Type = type;
            temperature = 256;
        }
    }
}
