using Microsoft.Xna.Framework.Input;

namespace Project1.Handlers
{
    internal class MouseHandler
    {
        private MouseState _currentState;
        private MouseState _previousState;

        public MouseState State
        {
            get { return _currentState; }
        }

        public int ScrollDelta { get; private set; }

        public void Update()
        {
            _previousState = _currentState;
            _currentState = Mouse.GetState();

            ScrollDelta = _currentState.ScrollWheelValue - _previousState.ScrollWheelValue;
        }
    }
}
