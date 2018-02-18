using System.Diagnostics.Contracts;
using IrregularMachine.IrregularEngine.Data;

namespace IrregularMachine.IrregularEngine.Parser.Handlers {
    public static class StateParser_Ignore {
        public static void ParseTile(EngineParserState state, EngineTile tile) {
            Contract.Assert(tile.Type == EngineGlyphType.ModifierIgnore);

            tile.TimesAccessed++;
            state.Position.MoveForward();
            if (!state.GetAndFlushInverting()) {
                StateParserUtils.SkipEmptyTiles(state);                
                state.Position.MoveForward();
            }
        }
    }
}