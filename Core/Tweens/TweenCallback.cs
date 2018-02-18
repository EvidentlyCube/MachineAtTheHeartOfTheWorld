using System;

namespace IrregularMachine.Core.Tweens {
    public class TweenCallback : ITween{
        private readonly Action _callback;
        public bool IsFinished { get; private set; }
        
        public TweenCallback(Action finishedCallback) {
            _callback = finishedCallback ?? throw new ArgumentNullException(nameof(finishedCallback));
        }

        public void Update() {
            if (!IsFinished) {
                _callback?.Invoke();
                IsFinished = true;
            }
        }

        public void GoToEnd() {
            Update();
        }
    }
}