using System;

namespace IrregularMachine.Core.Tweens {
    public class TweenSleep : ITween{
        public readonly int Duration;

        private int _framesSpent;
        private readonly Action _updateCallback;
        private readonly Action _finishedCallback;
        
        public TweenSleep(int duration, Action updateCallback = null, Action finishedCallback = null) {
            Duration = duration;
            _framesSpent = 0;
            _updateCallback = updateCallback;
            _finishedCallback = finishedCallback;
        }

        public void Update() {
            if (!IsFinished) {
                _framesSpent++;

                _updateCallback?.Invoke();
                
                if (IsFinished) {
                    _finishedCallback?.Invoke();
                }
            }
        }

        public void GoToEnd() {
            _framesSpent = Duration;
            _updateCallback?.Invoke();
            _finishedCallback?.Invoke();
        }

        public bool IsFinished => _framesSpent >= Duration;
    }
}