using System;
using System.Numerics;
using IrregularMachine.Core;
using IrregularMachine.IrregularEngine;
using IrregularMachine.IrregularEngine.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace IrregularMachine.Scenes.Ingame {
    public class IngameScreen {
        private EngineScreen _engineScreen;
        private readonly float _renderOffset;

        private readonly Vector2 _precomputedMonitorPosition;
        private readonly float _precomputedGlyphScale;
        private readonly float _precomputedGlyphEdge;
        private readonly Vector2 _precomputedGlyphsOffset;

        public IngameScreen(float renderOffset, EngineScreen engineScreen) {
            _engineScreen = engineScreen ?? throw new ArgumentNullException(nameof(engineScreen));
            _renderOffset = renderOffset;

            _precomputedMonitorPosition = new Vector2((S.ViewportWidth - Gfx.bg_Monitor.Width) / 2f, 0);

            {
                // Calculate glyph scale
                var glyphWidth = Math.Min(S.GlyphMaxEdgeRenderSize, (float) S.GlyphAreaWidth / engineScreen.Width);
                var glyphHeight = Math.Min(S.GlyphMaxEdgeRenderSize, (float) S.GlyphAreaHeight / engineScreen.Height);

                _precomputedGlyphEdge = Math.Min(glyphWidth, glyphHeight);
                _precomputedGlyphScale = _precomputedGlyphEdge / S.GlyphImageEdge;

                var glyphsWidth = _precomputedGlyphEdge * engineScreen.Width;
                var glyphsHeight = _precomputedGlyphEdge * engineScreen.Height;

                _precomputedGlyphsOffset = new Vector2(
                    _precomputedMonitorPosition.X + (S.GlyphAreaWidth - glyphsWidth) / 2f + S.GlyphAreaMonitorOffsetX,
                    _precomputedMonitorPosition.Y + (S.GlyphAreaHeight - glyphsHeight) / 2f + S.GlyphAreaMonitorOffsetY
                );
            }
        }

        public void Update() {
            // 
        }

        public void Draw(float sceneOffset, SpriteBatch batch) {
            if (Math.Abs(sceneOffset - _renderOffset) >= 1) {
                return;
            }

            var offsetX = (_renderOffset - sceneOffset) * S.ViewportWidth;
            batch.Draw(Gfx.bg_Monitor, new Vector2(
                _precomputedMonitorPosition.X + offsetX, _precomputedMonitorPosition.Y
            ));

            for (var x = 0; x < _engineScreen.Width; x++) {
                for (var y = 0; y < _engineScreen.Height; y++) {
                    var glyph = _engineScreen.Tiles[x, y].Type;
                    if (glyph == EngineGlyphType.Nothing) {
                        continue;
                    }
                    
                    var position = new Vector2(
                        _precomputedGlyphsOffset.X + _precomputedGlyphEdge * x + offsetX, 
                        _precomputedGlyphsOffset.Y + _precomputedGlyphEdge * y 
                    );
                    var texture = Gfx.GlyphTextures[glyph];
                    batch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, _precomputedGlyphScale, SpriteEffects.None, 0);
                }
            }
        }
    }
}