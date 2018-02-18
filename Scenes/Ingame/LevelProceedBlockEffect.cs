using System;
using IrregularMachine.Core.Tweens;

namespace IrregularMachine.Scenes.Ingame {
    public class LevelProceedBlockEffect {
        private ITween _tween;

        public float Offset { get; private set; }
        public bool IsAnimating => !_tween?.IsFinished ?? false;

        public void Start() {
            if (_tween?.IsFinished ?? true) {
                _tween = new TweenFloat(0, (float)Math.PI, 20, x => Offset = (float)Math.Sin(x) * 0.02f);                
            }
        }

        public void Update() {
            _tween?.Update();
        }
    }
}