using System;

namespace IrregularMachine.Core {
    public class StateMachine<T> where T : struct {
        public T CurrentState { get; private set; }

        public StateMachine() {
            CurrentState = Enum.Parse<T>(0.ToString());
        }

        public void GoToState(T state) {
            CurrentState = state;
        }

        public void SwitchToNextState() {
            int state = Convert.ToInt32(CurrentState);
            int newState = state + 1;

            CurrentState = Enum.Parse<T>(newState.ToString());
        }
    }
}