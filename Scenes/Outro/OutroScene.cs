using IrregularMachine.Core;
using IrregularMachine.Core.Tweens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IrregularMachine.Scenes.Outro {
    public class OutroScene : IScene {
        private Texture2D _texture;
        private TweenSequence _tween;

        private float _alpha;
        
        public void OnSet() {
            _texture = Gfx.LoadOutroTexture();
            _tween = new TweenSequence();
            _tween.AddTween(new TweenFloat(0, 1, 60, x => _alpha = x));
            _tween.AddTween(new TweenSleep(40 * 60));
            _tween.AddTween(new TweenFloat(1, 0, 60, x => _alpha = x));
            _tween.AddTween(new TweenCallback(() => GameCore.Instance.Exit()));
            Sfx.MusicOutro.Play(Save.MusicVolume, 0, 0);
        }

        public void Update() {
            if (KeyboardManager.Instance.IsKeyDown(Keys.Escape)) {
                GameCore.Instance.Exit();
            }
            
            _tween.Update();
        }

        public void Draw(SpriteBatch batch) {
            batch.Begin(blendState: CustomBlendStates.RegularWithAlpha);
            batch.Draw(_texture, Vector2.Zero, new Color(1f, 1f, 1f, _alpha));
            batch.End();
        }

        public void OnUnset() {
        }
    }
}