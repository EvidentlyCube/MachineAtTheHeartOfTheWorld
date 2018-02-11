using IrregularMachine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IrregularMachine.Scenes.InMemoriam {
    public class InMemoriamScene : IScene {
        private const int PreWaitDuration = 30;
        private const int FadeDuration = 60;
        private const int WaitDuration = 120;
        private const int PostWaitDuration = 30;
        private Color _textColor;

        private TweenSequence _tween;

        public InMemoriamScene() {
            _tween = new TweenSequence(AfterTweenFinished);
        }

        public void OnSet() {
            _textColor = Color.White;
            _textColor.A = 0;
            
            _tween.Clear();
            _tween.AddTween(new Tween(0, 0, PreWaitDuration));
            _tween.AddTween(new Tween(0, 255, FadeDuration, v => _textColor.A = (byte)v));
            _tween.AddTween(new Tween(0, 0, WaitDuration));
            _tween.AddTween(new Tween(255, 0, FadeDuration, v => _textColor.A = (byte)v));
            _tween.AddTween(new Tween(0, 0, PostWaitDuration));
        }

        public void OnUnset() {
        }

        private void AfterTweenFinished() {
            
        }
        
        public void Update() {
            _tween.Update();
        }

        public void Draw(SpriteBatch batch) {
            batch.Begin(blendState: BlendState.NonPremultiplied);
            batch.Draw(Gfx.txt_InMemory, GetTextPosition(Gfx.txt_InMemory), _textColor);
            batch.End();
        }

        private Vector2 GetTextPosition(Texture2D texture) {
            return new Vector2(
                (S.ViewportWidth - texture.Width) / 2f,
                (S.ViewportHeight - texture.Height) / 2f
            );
        }
    }
}