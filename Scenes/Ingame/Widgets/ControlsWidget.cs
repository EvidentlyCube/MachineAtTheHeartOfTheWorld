using System;
using IrregularMachine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IrregularMachine.Scenes.Ingame.Widgets {
    public class ControlsWidget {
        public const int Padding = 20;
        public static int TextureWidth => Math.Max(Gfx.ControlsLeft.Width, Gfx.ControlsRight.Width);
        public bool IsHiding = false;
        private float _alpha;
        
        public void Update() {
            if (IsHiding) {
                if (_alpha > 0) {
                    _alpha -= 1/120f;
                }
            } else if (_alpha < 1) {
                _alpha += 1/120f;                
            }
        }

        public void Draw(SpriteBatch batch) {
            if (_alpha <= 0) {
                return;
            }
            
            batch.Draw(Gfx.ControlsLeft, new Vector2(40, S.ViewportHeight - Padding - Gfx.ControlsLeft.Height), new Color(1f, 1f, 1f, _alpha));
            batch.Draw(Gfx.ControlsRight, new Vector2(S.ViewportWidth - Padding - Gfx.ControlsRight.Width, S.ViewportHeight - Padding - Gfx.ControlsRight.Height), new Color(1f, 1f, 1f, _alpha));

        }
    }
}