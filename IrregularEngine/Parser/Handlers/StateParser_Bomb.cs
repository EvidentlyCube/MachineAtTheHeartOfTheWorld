using System.Diagnostics.Contracts;
using IrregularMachine.IrregularEngine.Data;

namespace IrregularMachine.IrregularEngine.Parser.Handlers {
    public static class StateParser_Bomb {
        public static void ParseTile(EngineParserState state, EngineTile tile) {
            Contract.Assert(tile.Type.IsBomb());

            if (!state.GetAndFlushInverting()) {
                state.Explode(1);
            }
            
            tile.TimesAccessed++;
            state.Position.MoveForward();
        }
    }
}