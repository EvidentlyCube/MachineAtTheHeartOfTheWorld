using System.Diagnostics.Contracts;
using IrregularMachine.IrregularEngine.Data;

namespace IrregularMachine.IrregularEngine.Parser.Handlers {
    public static class StateParser_Multiply {
        public static void ParseTile(EngineParserState state, EngineTile tile) {
            Contract.Assert(tile.Type == EngineGlyphType.OperationMultiplier);

            StateParserUtils.PerformOperation(state);
            state.LastOperator = state.GetAndFlushInverting() ? EngineOperatorType.Division : EngineOperatorType.Multiplication;
            tile.TimesAccessed++;
            state.Position.MoveForward();
        }
    }
}