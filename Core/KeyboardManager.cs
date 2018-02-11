using System;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace IrregularMachine.Core
{
    public class KeyboardManager : IDisposable {
        public static KeyboardManager Instance { get; }

        static KeyboardManager() {
            Instance = new KeyboardManager();
        }
        
        private KeyboardState _lastState;
        private KeyboardState _currentState;

        public bool IsShiftDown { get; private set; }
        public bool IsCtrlDown { get; private set; }
        public bool IsAltDown { get; private set; }

        public bool IsCapsLock => _currentState.CapsLock;
        public bool IsNumLock => _currentState.NumLock;

        private KeyboardManager()
        {
            _currentState = _lastState = Keyboard.GetState();
        }

        public void Update()
        {
            _lastState = _currentState;
            _currentState = Keyboard.GetState();

            IsShiftDown = _currentState.IsKeyDown(Keys.LeftShift) || _currentState.IsKeyDown(Keys.RightShift);
            IsCtrlDown = _currentState.IsKeyDown(Keys.LeftControl) || _currentState.IsKeyDown(Keys.RightControl);
            IsAltDown = _currentState.IsKeyDown(Keys.LeftAlt) || _currentState.IsKeyDown(Keys.RightAlt);
        }

        public bool IsKeyDown(Keys key)
        {
            return _currentState.IsKeyDown(key);
        }

        public bool IsKeyJustPressed(Keys key)
        {
            return _currentState.IsKeyDown(key) && !_lastState.IsKeyDown(key);
        }

        public bool IsKeyJustReleased(Keys key)
        {
            return !_currentState.IsKeyDown(key) && _lastState.IsKeyDown(key);
        }

        public Keys[] GetAllKeysDown() => _currentState.GetPressedKeys();

        public Keys[] GetAllKeysJustPressed() {
            var lastPressedKeys = _lastState.GetPressedKeys();
            return _currentState.GetPressedKeys().Where(x => !lastPressedKeys.Contains(x)).ToArray();
        }

        public bool IsAnyKeyDown() => GetAllKeysDown().Length > 0;
        public bool IsAnyKeyJustPressed() => GetAllKeysJustPressed().Length > 0;

        public void Dispose()
        {
            // Nothing happens
        }
    }
}