using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using IrregularMachine.IrregularEngine.Data;
using IrregularMachine.IrregularEngine.Parser.Handlers;

namespace IrregularMachine.IrregularEngine.Parser {
    public class EngineParser {
        public EngineTile[,] Tiles { get; }

        private readonly Dictionary<EngineGlyphType, Action<EngineParserState, EngineTile>> _tileParsers;
        private readonly EngineParserState _state;

        public EngineParser(EngineTile[,] tiles) {
            Tiles = CloneTiles(tiles);
            _state = new EngineParserState(this, Tiles);
            _tileParsers = new Dictionary<EngineGlyphType, Action<EngineParserState, EngineTile>> {
                {EngineGlyphType.Nothing, StateParser_Nothing.ParseTile},
                {EngineGlyphType.ActionMoveN, StateParser_Action.ParseTile},
                {EngineGlyphType.ActionMoveNE, StateParser_Action.ParseTile},
                {EngineGlyphType.ActionMoveE, StateParser_Action.ParseTile},
                {EngineGlyphType.ActionMoveSE, StateParser_Action.ParseTile},
                {EngineGlyphType.ActionMoveS, StateParser_Action.ParseTile},
                {EngineGlyphType.ActionMoveSW, StateParser_Action.ParseTile},
                {EngineGlyphType.ActionMoveW, StateParser_Action.ParseTile},
                {EngineGlyphType.ActionMoveNW, StateParser_Action.ParseTile},
                {EngineGlyphType.ActionSpecial, StateParser_Action.ParseTile},
                {EngineGlyphType.NumeralOne, StateParser_Numeral.ParseTile},
                {EngineGlyphType.NumeralTwo, StateParser_Numeral.ParseTile},
                {EngineGlyphType.NumeralFour, StateParser_Numeral.ParseTile},
                {EngineGlyphType.NumeralEight, StateParser_Numeral.ParseTile},
                {EngineGlyphType.OperationMultiplier, StateParser_Multiply.ParseTile},
                {EngineGlyphType.OperationSubtract, StateParser_Subtract.ParseTile},
                {EngineGlyphType.ModifierInvert, StateParser_Invert.ParseTile},
                {EngineGlyphType.ModifierIgnore, StateParser_Ignore.ParseTile},
                {EngineGlyphType.SpecialCameleonN, StateParser_Cameleon.ParseTile},
                {EngineGlyphType.SpecialCameleonNE, StateParser_Cameleon.ParseTile},
                {EngineGlyphType.SpecialCameleonE, StateParser_Cameleon.ParseTile},
                {EngineGlyphType.SpecialCameleonSE, StateParser_Cameleon.ParseTile},
                {EngineGlyphType.SpecialCameleonS, StateParser_Cameleon.ParseTile},
                {EngineGlyphType.SpecialCameleonSW, StateParser_Cameleon.ParseTile},
                {EngineGlyphType.SpecialCameleonW, StateParser_Cameleon.ParseTile},
                {EngineGlyphType.SpecialCameleonNW, StateParser_Cameleon.ParseTile},
                {EngineGlyphType.Bomb, StateParser_Bomb.ParseTile},
                {EngineGlyphType.SlicerStart, StateParser_Slicer.ParseTile},
                {EngineGlyphType.SlicerEnd, StateParser_Slicer.ParseTile}
            };
        }

        public List<EngineActionType> Parse() {
            _state.Reset();

            while (!_state.IsFinished) {
                var tile = _state[_state.Position];
                ParseTile(tile);
            }

            return _state.Actions;
        }

        public void ParseTile(EngineTile tile) {
            Contract.Assert(_tileParsers.ContainsKey(tile.Type), $"Failed to find parser for {tile.Type}");
            var callback = _tileParsers[tile.Type];
            callback.Invoke(_state, tile);
        }

        private EngineTile[,] CloneTiles(EngineTile[,] original) {
            var newTiles = new EngineTile[original.GetLength(0), original.GetLength(1)];

            for (var x = 0; x < original.GetLength(0); x++) {
                for (var y = 0; y < original.GetLength(1); y++) {
                    newTiles[x, y] = original[x, y].Clone();
                }
            }

            return newTiles;
        }
    }
}