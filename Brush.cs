namespace Project1
{
    enum BrushType { Square, Circle }
    internal class Brush
    {
        private int _size;

        public int Size
        {
            get { return _size; }
            set { if (value > 0 && value < 256) { _size = value; } }
        }
        public ElementType PickedParticle;
        public BrushType Type;

        public Brush()
        {
            Size = 10;
            PickedParticle = ElementType.SAND;
            Type = BrushType.Circle;
        }
    }
}
