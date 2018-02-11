using System;
using System.Collections.Generic;
using IrregularMachine.IrregularEngine.Data;
using Microsoft.Xna.Framework;

namespace IrregularMachine.IrregularEngine.Parser {
    public class EngineParserState {
        public readonly EngineParser EngineParser;
        public EngineTile[,] Tiles { get; }
        public ParserPosition Position { get; }
        private List<EngineActionType> Actions { get; }

        public int LastNumberGroup;
        public int CurrentNumberGroup;
        public EngineOperatorType LastOperator;

        private bool _isInverting;

        public bool IsFinished => Position.IsFinished;

        public EngineParserState(EngineParser parser, EngineTile[,] tiles) {
            EngineParser = parser;
            Tiles = tiles;
            Position = new ParserPosition(tiles);
            Actions = new List<EngineActionType>();
        }

        public void Reset() {
            Position.Reset();
            Actions.Clear();
            LastOperator = EngineOperatorType.NoOperator;
            LastNumberGroup = 0;
            CurrentNumberGroup = 0;
            _isInverting = false;
        }

        public void AddAction(EngineActionType engineActionType, int count = 1) {
            while (count-- > 0) {
                Actions.Add(engineActionType);
            }
        }

        public List<Tuple<EngineActionType, int>> GetSquashedActions() {
            var list = new List<Tuple<EngineActionType, int>>();

            EngineActionType? lastType = null;
            int count = 0;
            foreach (var actionType in Actions) {
                if (lastType.HasValue && lastType.Value != actionType) {
                    list.Add(new Tuple<EngineActionType, int>(lastType.Value, count));
                    lastType = actionType;
                    count = 0;
                } else if (!lastType.HasValue) {
                    lastType = actionType;
                }

                count++;
            }

            if (lastType.HasValue)
                list.Add(new Tuple<EngineActionType, int>(lastType.Value, count));

            return list;
        }

        public bool GetAndFlushInverting() {
            var value = _isInverting;
            _isInverting = false;

            return value;
        }

        public void ToggleInverting() {
            _isInverting = !_isInverting;
        }

        public bool IsValidPosition(int x, int y) =>
            x >= 0 && y >= 0 && x < Tiles.GetLength(0) && y < Tiles.GetLength(1);

        public bool IsValidPosition(ParserPosition position) =>
            IsValidPosition(position.Position.X, position.Position.Y);
        
        public bool IsValidPosition(Point position) =>
            IsValidPosition(position.X, position.Y);
        
        public EngineTile this[Point position] => Tiles[position.X, position.Y];
        public EngineTile this[ParserPosition index] => Tiles[index.Position.X, index.Position.Y];
    }
}