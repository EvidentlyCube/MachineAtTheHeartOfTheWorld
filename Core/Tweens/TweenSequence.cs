using System;
using System.Collections.Generic;

namespace IrregularMachine.Core.Tweens {
    public class TweenSequence {
        private readonly List<ITween> _tweens;
        private readonly Action<ITween> _updateCallback;
        private readonly Action _finishedCallback;

        public ITween CurrentTween => _tweens.Count > 0 ? _tweens[0] : null;

        public TweenSequence(Action finishedCallback = null, Action<ITween> updateCallback = null) {
            _tweens = new List<ITween>();
            _updateCallback = updateCallback;
            _finishedCallback = finishedCallback;
        }

        public void Clear() {
            _tweens.Clear();
        }

        public void AddTween(ITween tween) {
            _tweens.Add(tween);
        }

        public void GoToNext() {
            if (_tweens.Count == 0) {
                return;
            }
            
            _tweens[0].GoToEnd();
            Update();
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
                else {
                    Update();
                }
            }
        }
    }
}