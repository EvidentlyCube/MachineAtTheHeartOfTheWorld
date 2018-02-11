using System;
using IrregularMachine.IrregularEngine.Data;
using Microsoft.Xna.Framework;

namespace IrregularMachine.IrregularEngine.Parser {
    public static class StateParserUtils {
        public static int GetNumeralValue(EngineTile tile, bool isInverting) {
            var multiplier = isInverting ? -1 : 1;

            switch (tile.Type) {
                case EngineGlyphType.NumeralOne:
                    return 1 * multiplier;
                case EngineGlyphType.NumeralTwo:
                    return 2 * multiplier;
                case EngineGlyphType.NumeralFour:
                    return 4 * multiplier;
                case EngineGlyphType.NumeralEight:
                    return 8 * multiplier;
                default:
                    throw new ArgumentException($"Invalid tile type, Numeral expeced {tile.Type} given", nameof(tile));
            }
        }

        public static void PerformOperation(EngineParserState state) {
            switch (state.LastOperator) {
                case EngineOperatorType.NoOperator:
                    state.LastNumberGroup = state.CurrentNumberGroup;
                    break;
                case EngineOperatorType.Addition:
                    state.LastNumberGroup += state.CurrentNumberGroup;
                    break;
                case EngineOperatorType.Subtraction:
                    state.LastNumberGroup -= state.CurrentNumberGroup;
                    break;
                case EngineOperatorType.Multiplication:
                    state.LastNumberGroup *= state.CurrentNumberGroup;
                    break;
                case EngineOperatorType.Division:
                    state.LastNumberGroup = (int)Math.Floor((double)state.LastNumberGroup / state.CurrentNumberGroup);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            state.CurrentNumberGroup = 0;
            state.LastOperator = EngineOperatorType.NoOperator;
        }

        public static void SkipEmptyTiles(EngineParserState state) {
            while (!state.Position.IsFinished && state[state.Position].Type == EngineGlyphType.Nothing) {
                state.Position.MoveForward();
            }
        }

        public static EngineTile Raycast(EngineParserState state, ParserPosition startPosition, EngineDirection direction) {
            var currentPosition = startPosition.Position;

            currentPosition = new Point(currentPosition.X + direction.X, currentPosition.Y + direction.Y);
            while(state.IsValidPosition(currentPosition)) {
                var tile = state[currentPosition];

                if (tile.Type != EngineGlyphType.Nothing) {
                    return tile;
                }

                currentPosition = new Point(currentPosition.X + direction.X, currentPosition.Y + direction.Y);
            }

            return null;
        }
    }
}