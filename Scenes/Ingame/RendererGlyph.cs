using System;
using IrregularMachine.Core;
using IrregularMachine.IrregularEngine.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace IrregularMachine.Scenes.Ingame {
    public class RendererGlyph {
        private const int CogTraditional = 1;
        private const int CogBladed = 2;
        private const int CogThick = 3;
        private const int CogShort = 4;
        private const int CogRound = 5;
        private const int CogHalf = 6;
        
        private const int PlateSquare = 0;
        private const int PlateDiamond = 1;
        private const int PlateCircle = 2;
        private const int PlateDeltoid = 3;
        private const int PlatePentagon = 4;
        private const int PlateHexagon = 5;
        
        private EngineGlyphType _glyphType;
        private RendererAnimation _cog;
        private TextureRegion2D _plate;
        private TextureRegion2D _glyph;
        private Vector2 _position; 
        private Vector2 _glyphPosition; 

        public RendererGlyph(EngineGlyphType glyphType, int x, int y, int levelWidth, int levelHeight) {
            _glyphType = glyphType;
            _cog = RendererAnimation.GetCogAnimation(GetCogTypeForGlyph(glyphType), (x + y) % 2 == 0);

            var plateIndex = GetPlateTypeForGlyph(glyphType);
            Logger.Debug($"Loading plate texture 'glyph_plate_{plateIndex}'");
            _plate = Gfx.sheet_Ingame[$"glyph_plate_{plateIndex}"];
            
            Logger.Debug($"Loading glyph 'glyph_{(int)glyphType}'");
            _glyph = Gfx.sheet_Ingame[$"glyph_{(int)glyphType}"];
            
            _position = new Vector2(
                (S.GlyphAreaWidth - levelWidth * S.GlyphImageEdge) / 2f + S.GlyphAreaMonitorOffsetX + x * S.GlyphImageEdge,
                (S.GlyphAreaHeight - levelHeight * S.GlyphImageEdge) / 2f + S.GlyphAreaMonitorOffsetY + y * S.GlyphImageEdge
            );

            _glyphPosition = new Vector2(
                _position.X + (S.GlyphImageEdge - _glyph.Width) / 2f,
                _position.Y + (S.GlyphImageEdge - _glyph.Height) / 2f
            );
        }

        public void Update() {
            _cog.Update();
        }

        public void Draw(SpriteBatch batch, float offsetX) {
            _cog.Draw(batch, _position.X + offsetX - S.GlyphCogOffset, _position.Y - S.GlyphCogOffset);
            batch.Draw(_plate, new Vector2(_position.X + offsetX, _position.Y), Color.White);
            batch.Draw(_glyph, new Vector2(_glyphPosition.X + offsetX, _glyphPosition.Y), Color.White);
        }

        private int GetCogTypeForGlyph(EngineGlyphType glyphType) {
            switch (glyphType) {
                case EngineGlyphType.ActionSpecial: return CogTraditional;
                case EngineGlyphType.ActionMoveN: return CogTraditional;
                case EngineGlyphType.ActionMoveNE: return CogTraditional;
                case EngineGlyphType.ActionMoveE: return CogTraditional;
                case EngineGlyphType.ActionMoveSE: return CogTraditional;
                case EngineGlyphType.ActionMoveS: return CogTraditional;
                case EngineGlyphType.ActionMoveSW: return CogTraditional;
                case EngineGlyphType.ActionMoveW: return CogTraditional;
                case EngineGlyphType.ActionMoveNW: return CogTraditional;
                case EngineGlyphType.NumeralOne: return CogBladed;
                case EngineGlyphType.NumeralTwo: return CogBladed;
                case EngineGlyphType.NumeralFour: return CogBladed;
                case EngineGlyphType.NumeralEight: return CogBladed;
                case EngineGlyphType.SpecialCameleonN: return CogRound;
                case EngineGlyphType.SpecialCameleonNE: return CogRound;
                case EngineGlyphType.SpecialCameleonE: return CogRound;
                case EngineGlyphType.SpecialCameleonSE: return CogRound;
                case EngineGlyphType.SpecialCameleonS: return CogRound;
                case EngineGlyphType.SpecialCameleonSW: return CogRound;
                case EngineGlyphType.SpecialCameleonW: return CogRound;
                case EngineGlyphType.SpecialCameleonNW: return CogRound;
                case EngineGlyphType.OperationMultiplier: return CogBladed;
                case EngineGlyphType.OperationSubtract: return CogBladed;
                case EngineGlyphType.ModifierInvert: return CogShort;
                case EngineGlyphType.ModifierIgnore: return CogShort;
                case EngineGlyphType.SlicerStart: return CogThick;
                case EngineGlyphType.SlicerEnd: return CogThick;
                case EngineGlyphType.Bomb: return CogHalf;
                default:
                    throw new ArgumentOutOfRangeException(nameof(glyphType), glyphType, null);
            }
        }
        
        private int GetPlateTypeForGlyph(EngineGlyphType glyphType) {
            switch (glyphType) {
                case EngineGlyphType.ActionSpecial: return PlateSquare;
                case EngineGlyphType.ActionMoveN: return PlateSquare;
                case EngineGlyphType.ActionMoveNE: return PlateSquare;
                case EngineGlyphType.ActionMoveE: return PlateSquare;
                case EngineGlyphType.ActionMoveSE: return PlateSquare;
                case EngineGlyphType.ActionMoveS: return PlateSquare;
                case EngineGlyphType.ActionMoveSW: return PlateSquare;
                case EngineGlyphType.ActionMoveW: return PlateSquare;
                case EngineGlyphType.ActionMoveNW: return PlateSquare;
                case EngineGlyphType.NumeralOne: return PlateCircle;
                case EngineGlyphType.NumeralTwo: return PlateCircle;
                case EngineGlyphType.NumeralFour: return PlateCircle;
                case EngineGlyphType.NumeralEight: return PlateCircle;
                case EngineGlyphType.SpecialCameleonN: return PlateDiamond;
                case EngineGlyphType.SpecialCameleonNE: return PlateDiamond;
                case EngineGlyphType.SpecialCameleonE: return PlateDiamond;
                case EngineGlyphType.SpecialCameleonSE: return PlateDiamond;
                case EngineGlyphType.SpecialCameleonS: return PlateDiamond;
                case EngineGlyphType.SpecialCameleonSW: return PlateDiamond;
                case EngineGlyphType.SpecialCameleonW: return PlateDiamond;
                case EngineGlyphType.SpecialCameleonNW: return PlateDiamond;
                case EngineGlyphType.OperationMultiplier: return PlateCircle;
                case EngineGlyphType.OperationSubtract: return PlateCircle;
                case EngineGlyphType.ModifierInvert: return PlatePentagon;
                case EngineGlyphType.ModifierIgnore: return PlatePentagon;
                case EngineGlyphType.SlicerStart: return PlateDeltoid;
                case EngineGlyphType.SlicerEnd: return PlateDeltoid;
                case EngineGlyphType.Bomb: return PlateHexagon;
                default:
                    throw new ArgumentOutOfRangeException(nameof(glyphType), glyphType, null);
            }
        }
    }
}