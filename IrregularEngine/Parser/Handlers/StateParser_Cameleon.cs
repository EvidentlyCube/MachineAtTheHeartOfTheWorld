using System.Diagnostics.Contracts;
using IrregularMachine.IrregularEngine.Data;

namespace IrregularMachine.IrregularEngine.Parser.Handlers {
    public static class StateParser_Cameleon {
        public static void ParseTile(EngineParserState state, EngineTile tile) {
            Contract.Assert(tile.Type.IsCameleon());

            var baseDirection = tile.Type.GetDirection();
            var target = FindTarget(state, state.GetAndFlushInverting() ? baseDirection.Opposite : baseDirection, 0);

            if (target == null) {
                state.Position.MoveForward();
            } else {
                target = new EngineTile(target.Type) {
                    TimesAccessed = tile.TimesAccessed
                };
                state.EngineParser.ParseTile(target);
            }
            tile.TimesAccessed++;
        }

        private static EngineTile FindTarget(EngineParserState state, EngineDirection direction, int depth) {
            if (depth >= 16) return null;

            var foundTile = StateParserUtils.Raycast(state, state.Position, direction);
            if (foundTile == null) return null;
            return foundTile.Type.IsCameleon() ? FindTarget(state, foundTile.Type.GetDirection(), depth + 1) : foundTile;
        }
    }
}