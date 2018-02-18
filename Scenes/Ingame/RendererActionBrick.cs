using System;
using IrregularMachine.Core;
using IrregularMachine.IrregularEngine.Data;
using IrregularMachine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace IrregularMachine.Scenes.Ingame {
    public class RendererActionBrick {
        private float _x;
        private float _y;
        private float _targetX;

        private readonly TextureRegion2D _texture;

        public readonly EngineActionType Action;
        public bool IsHiding = false;
        public int HidingDelay = 0;
        public float HideSpeed = 1;

        public bool IsFinished => _y > S.ActionInputClippingRectangle.Bottom;

        public RendererActionBrick(EngineActionType action, int index, int bricksCount = 0) {
            Action = action;
            _texture = GetTextureForAction(action);
            _x = S.ActionInputClippingRectangle.Right + _texture.Width * index;
            _y = S.ActionGlassLeftmostY;

            if (bricksCount > 0) {
                var actionsWidth = (_texture.Width + S.ActionGlassSpacing) * bricksCount - 6;
                _x = S.ActionInputClippingRectangle.X + (S.ActionInputClippingRectangle.Width - actionsWidth) / 2f + index * (_texture.Width + S.ActionGlassSpacing);

            }
        }

        public void Update(int bricksCount, int index) {
            if (IsHiding) {
                if (HidingDelay > 0) {
                    HidingDelay--;
                }
                else {
                    _y += HideSpeed;
                    HideSpeed *= 1.3f;
                }
            }
            var actionsWidth = (_texture.Width + S.ActionGlassSpacing) * bricksCount - 6;
            _targetX = S.ActionInputClippingRectangle.X + (S.ActionInputClippingRectangle.Width - actionsWidth) / 2f + index * (_texture.Width + S.ActionGlassSpacing);
            
            _x = (float)MathUtils.ApproachFactor(_x, _targetX, 0.2, 0.001, 100);
        }

        public void Draw(SpriteBatch batch, float offsetX) {
            batch.Draw(
                _texture,
                new Vector2(
                    _x + offsetX, 
                    _y
                ),
                Color.White,
                new Rectangle(
                    (int) (S.ActionInputClippingRectangle.X + offsetX),
                    S.ActionInputClippingRectangle.Y,
                    S.ActionInputClippingRectangle.Width,
                    S.ActionInputClippingRectangle.Height
                )
            );
        }

        private TextureRegion2D GetTextureForAction(EngineActionType action) {
            switch (action) {
                case EngineActionType.Special: return Gfx.sheet_Ingame["input_glyph_10"];
                case EngineActionType.MoveN: return Gfx.sheet_Ingame["input_glyph_11"];
                case EngineActionType.MoveNE: return Gfx.sheet_Ingame["input_glyph_12"];
                case EngineActionType.MoveE: return Gfx.sheet_Ingame["input_glyph_13"];
                case EngineActionType.MoveSE: return Gfx.sheet_Ingame["input_glyph_14"];
                case EngineActionType.MoveS: return Gfx.sheet_Ingame["input_glyph_15"];
                case EngineActionType.MoveSW: return Gfx.sheet_Ingame["input_glyph_16"];
                case EngineActionType.MoveW: return Gfx.sheet_Ingame["input_glyph_17"];
                case EngineActionType.MoveNW: return Gfx.sheet_Ingame["input_glyph_18"];
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }
    }
}