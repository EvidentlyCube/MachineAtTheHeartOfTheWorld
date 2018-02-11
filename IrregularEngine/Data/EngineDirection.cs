using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IrregularMachine.IrregularEngine.Data {
    public sealed class EngineDirection {
        public static readonly ReadOnlyCollection<EngineDirection> Directions;
        public static EngineDirection North { get; }
        public static EngineDirection NorthEast { get; }
        public static EngineDirection East { get; }
        public static EngineDirection SouthEast { get; }
        public static EngineDirection South { get; }
        public static EngineDirection SouthWest { get; }
        public static EngineDirection West { get; }
        public static EngineDirection NorthWest { get; }

        static EngineDirection() {
            North = new EngineDirection(0, "north", 0, -1);
            NorthEast = new EngineDirection(1, "north-east", 1, -1);
            East = new EngineDirection(2, "east", 1, 0);
            SouthEast = new EngineDirection(3, "south-east", 1, 1);
            South = new EngineDirection(4, "south", 0, 1);
            SouthWest = new EngineDirection(5, "south-west", -1, 1);
            West = new EngineDirection(6, "west", -1, 0);
            NorthWest = new EngineDirection(7, "north-west", -1, -1);

            North.NextCw = NorthEast;
            NorthEast.NextCw = East;
            East.NextCw = SouthEast;
            SouthEast.NextCw = South;
            South.NextCw = SouthWest;
            SouthWest.NextCw = West;
            West.NextCw = NorthWest;
            NorthWest.NextCw = North;

            North.NextCcw = NorthWest;
            NorthWest.NextCcw = West;
            West.NextCcw = SouthWest;
            SouthWest.NextCcw = South;
            South.NextCcw = SouthEast;
            SouthEast.NextCcw = East;
            East.NextCcw = NorthEast;
            NorthEast.NextCcw = North;

            North.Opposite = South;
            NorthEast.Opposite = SouthWest;
            East.Opposite = West;
            SouthEast.Opposite = NorthWest;
            South.Opposite = North;
            SouthWest.Opposite = NorthEast;
            West.Opposite = East;
            NorthWest.Opposite = SouthEast;

            Directions = new ReadOnlyCollection<EngineDirection>(new List<EngineDirection> {
                North,
                NorthEast,
                East,
                SouthEast,
                South,
                SouthWest,
                West,
                NorthWest
            });
        }

        public readonly int Id;
        public readonly string Name;
        public readonly int X;
        public readonly int Y;
        public EngineDirection NextCw { get; private set; }
        public EngineDirection NextCcw { get; private set; }
        public EngineDirection Opposite { get; private set; }

        public bool IsHorizontal => X != 0;
        public bool IsVertical => Y != 0;
        public bool IsDiagonal => X != 0 && Y != 0;

        // ReSharper disable once NotNullMemberIsNotInitialized
        private EngineDirection(int id, string name, int x, int y) {
            Id = id;
            Name = name;
            X = x;
            Y = y;
        }
    }
}