using System.Diagnostics.Contracts;
using IrregularMachine.IrregularEngine.Data;

namespace IrregularMachine.IrregularEngine.Parser.Handlers {
    public static class StateParser_Numeral {
        public static void ParseTile(EngineParserState state, EngineTile tile) {
            Contract.Assert(tile.Type.IsNumeral());

            state.CurrentNumberGroup += StateParserUtils.GetNumeralValue(tile, state.GetAndFlushInverting());
            tile.TimesAccessed++;
            state.Position.MoveForward();
        }
    }
}