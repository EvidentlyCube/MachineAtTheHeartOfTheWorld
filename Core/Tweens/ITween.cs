namespace IrregularMachine.Core.Tweens {
    public interface ITween {
        bool IsFinished { get; }
        void Update();
        void GoToEnd();
    }
}