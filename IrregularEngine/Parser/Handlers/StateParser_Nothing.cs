using System.Diagnostics.Contracts;
using IrregularMachine.IrregularEngine.Data;

namespace IrregularMachine.IrregularEngine.Parser.Handlers {
    public static class StateParser_Nothing {
        public static void ParseTile(EngineParserState state, EngineTile tile) {
            Contract.Assert(tile.Type == EngineGlyphType.Nothing);

            tile.TimesAccessed++;
            state.Position.MoveForward();
        }
    }
}