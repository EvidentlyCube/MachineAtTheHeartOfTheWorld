using System.Diagnostics.Contracts;
using IrregularMachine.IrregularEngine.Data;

namespace IrregularMachine.IrregularEngine.Parser.Handlers {
    public static class StateParser_Subtract {
        public static void ParseTile(EngineParserState state, EngineTile tile) {
            Contract.Assert(tile.Type == EngineGlyphType.OperationSubtract);

            StateParserUtils.PerformOperation(state);

            state.LastOperator = state.GetAndFlushInverting() ? EngineOperatorType.Addition : EngineOperatorType.Subtraction;
            tile.TimesAccessed++;
            state.Position.MoveForward();
        }
    }
}