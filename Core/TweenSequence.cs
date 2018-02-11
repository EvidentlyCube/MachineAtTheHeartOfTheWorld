using System;
using System.Collections.Generic;

namespace IrregularMachine.Core {
    public class TweenSequence {
        private List<Tween> _tweens;
        
        private Action<Tween> _updateCallback;
        private Action _finishedCallback;

        public TweenSequence(Action finishedCallback = null, Action<Tween> updateCallback = null) {
            _tweens = new List<Tween>();
            _updateCallback = updateCallback;
            _finishedCallback = finishedCallback;
        }

        public void Clear() {
            _tweens.Clear();
        }

        public void AddTween(Tween tween) {
            _tweens.Add(tween);
        }

        public void Update() {
            if (_tweens.Count == 0) {
                return;
            }
            
            _updateCallback?.Invoke(_tweens[0]);
            _tweens[0].Update();
            if (_tweens[0].IsFinished) {
                _tweens.RemoveAt(0);

                if (_tweens.Count == 0) {
                    _finishedCallback?.Invoke();
                }
            }
        }
    }
}