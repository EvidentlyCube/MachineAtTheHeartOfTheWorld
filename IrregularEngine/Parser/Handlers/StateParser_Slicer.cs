using System.Diagnostics.Contracts;
using IrregularMachine.IrregularEngine.Data;

namespace IrregularMachine.IrregularEngine.Parser.Handlers {
    public static class StateParser_Slicer {
        public static void ParseTile(EngineParserState state, EngineTile tile) {
            Contract.Assert(tile.Type.IsSlicer());

            var mustSeek = tile.Type == EngineGlyphType.SlicerEnd && tile.TimesAccessed == 0;
            if (state.GetAndFlushInverting() && tile.TimesAccessed == 0) {
                mustSeek = true;
            }
            
            tile.TimesAccessed++;

            if (mustSeek) {
                while (!state.Position.IsBeginning) {
                    state.Position.MoveBackward();
                    if (state[state.Position].Type == EngineGlyphType.SlicerStart) {
                        break;
                    }
                }
            }
            else {
                state.Position.MoveForward();
            }
        }
    }
}