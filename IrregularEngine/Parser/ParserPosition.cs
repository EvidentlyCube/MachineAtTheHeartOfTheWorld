using Microsoft.Xna.Framework;

namespace IrregularMachine.IrregularEngine.Parser {
    public class ParserPosition {
        private readonly EngineTile[,] _tiles;

        public Point Position { get; private set; }
        public bool IsFinished => Position.Y >= _tiles.GetLength(1);
        public bool IsBeginning => Position == Point.Zero;

        public ParserPosition(EngineTile[,] tiles) {
            _tiles = tiles;
        }

        public ParserPosition Clone() => new ParserPosition(_tiles) {Position = Position};

        public void Reset() => Position = Point.Zero;

        public void SetTo(ParserPosition position) {
            Position = position.Position;
        }

        public void MoveForward() {
            if (IsFinished) return;

            Position = new Point(Position.X + 1, Position.Y);

            if (Position.X >= _tiles.GetLength(0)) {
                Position = new Point(0, Position.Y + 1);
            }
        }

        public void MoveBackward() {
            if (IsBeginning) return;

            Position = new Point(Position.X - 1, Position.Y);

            if (Position.X < 0) {
                Position = new Point(_tiles.GetLength(0) - 1, Position.Y - 1);
            }
        }

        public void Move(int delta) {
            while (delta > 0) {
                delta--;
                MoveForward();
            }
            while (delta < 0) {
                delta++;
                MoveBackward();
            }
        }
    }
}