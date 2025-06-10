using Microsoft.Xna.Framework.Input;

namespace Project1.Handlers
{
    internal class KeyboardHandler
    {
        private KeyboardState _currentState;
        private KeyboardState _previousState;

        public KeyboardState State
        {
            get { return _currentState; }
        }

        public bool IsKeyJustDown(Keys key)
        {
            return _currentState.IsKeyDown(key) && _previousState.IsKeyUp(key);
        }

        public void Update()
        {
            _previousState = _currentState;
            _currentState = Keyboard.GetState();
        }
    }
}
