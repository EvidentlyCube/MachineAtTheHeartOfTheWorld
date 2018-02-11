using System;
using System.Diagnostics.Contracts;
using IrregularMachine.IrregularEngine.Data;

namespace IrregularMachine.IrregularEngine.Parser.Handlers {
    public static class StateParser_Action {
        public static void ParseTile(EngineParserState state, EngineTile tile) {
            Contract.Assert(tile.Type.IsAction());

            StateParserUtils.PerformOperation(state);

            AddAction(state, GetActionType(tile, state.GetAndFlushInverting()));
            tile.TimesAccessed++;
            state.Position.MoveForward();
        }

        private static EngineActionType GetActionType(EngineTile tile, bool isInverted) {
            switch (tile.Type) {
                case EngineGlyphType.ActionMoveN:
                case EngineGlyphType.ActionMoveNE:
                case EngineGlyphType.ActionMoveE:
                case EngineGlyphType.ActionMoveSE:
                case EngineGlyphType.ActionMoveS:
                case EngineGlyphType.ActionMoveSW:
                case EngineGlyphType.ActionMoveW:
                case EngineGlyphType.ActionMoveNW:
                    return EngineActionType.MoveN + (tile.Type.GetDirection().Id + (isInverted ? 4 : 0)) % 8;
                case EngineGlyphType.ActionSpecial:
                    return EngineActionType.Special;
                default:
                    throw new ArgumentException($"Can't be called with {tile.Type}");
            }
        }

        private static void AddAction(EngineParserState state, EngineActionType actionType) {
            if (state.LastNumberGroup < 0) return;
            if (state.LastNumberGroup == 0) state.LastNumberGroup = 1;

            state.AddAction(actionType, state.LastNumberGroup);
            state.LastNumberGroup = 0;
        }
    }
}