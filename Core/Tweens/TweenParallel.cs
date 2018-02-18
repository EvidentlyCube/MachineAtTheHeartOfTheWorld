using System;
using System.Collections.Generic;
using System.Linq;

namespace IrregularMachine.Core.Tweens {
    public class TweenParallel {
        private readonly List<ITween> _tweens;
        private readonly Action _updateCallback;
        private readonly Action _finishedCallback;

        public ITween CurrentTween => _tweens.Count > 0 ? _tweens[0] : null;

        public TweenParallel(Action finishedCallback = null, Action updateCallback = null) {
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

        public void Update() {
            _tweens.ForEach(tween => tween.Update());
            _updateCallback?.Invoke();

            if (_tweens.All(x => x.IsFinished)) {
                _tweens.Clear();
                _finishedCallback?.Invoke();
            }
        }
    }
}