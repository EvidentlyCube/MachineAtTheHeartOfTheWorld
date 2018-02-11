using System.Diagnostics.Contracts;
using IrregularMachine.IrregularEngine.Data;

namespace IrregularMachine.IrregularEngine.Parser.Handlers {
    public static class StateParser_Invert {
        public static void ParseTile(EngineParserState state, EngineTile tile) {
            Contract.Assert(tile.Type == EngineGlyphType.ModifierInvert);

            tile.TimesAccessed++;
            state.Position.MoveForward();
            state.ToggleInverting();
        }
    }
}