using IrregularMachine.BitmapTexts;
using IrregularMachine.Core;
using IrregularMachine.Core.Tweens;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace IrregularMachine.Scenes.Ingame.Widgets {
    public class SubtitleWidget {
        private readonly TweenSequence _tween;
        private readonly BitmapText _body;

        public SubtitleWidget() {
            _tween = new TweenSequence();
            _body = new BitmapText(Gfx.BitmapFontOutline);

            _body.FontScale = 0.75f;
            _body.HorizontalAlign = BitmapTextHorizontalAlign.Center;
            _body.VerticalAlign = BitmapTextVerticalAlign.Middle;

            _body.Width = S.ViewportWidth - ControlsWidget.Padding * 4 - ControlsWidget.TextureWidth * 2;
            _body.Height = _body.EffectiveLineHeight * _body.FontScale * 3.4f;
            
            _body.X = ControlsWidget.Padding * 2 + ControlsWidget.TextureWidth;
            _body.Y = S.ViewportHeight - _body.Height;
        }

        public void AddDelay() {
            _tween.AddTween(new TweenSleep(60));
        }

        public void AddSubtitle(string text, SoundEffect voiceOver = null) {
            _tween.AddTween(new TweenCallback(() => _body.Text = text));
            if (voiceOver != null) {
                _tween.AddTween(new TweenCallback(() => voiceOver.Play(Save.VoiceOverVolume, 0, 0)));
            }

            _tween.AddTween(new TweenSleep(voiceOver != null ? (int) (voiceOver.Duration.TotalSeconds * 61) : 240));
            _tween.AddTween(new TweenCallback(() => _body.Text = ""));
        }

        public void Update() {
            _tween.Update();
        }

        public void Draw(SpriteBatch batch) {
            if (_body.Text != "") {
                _body.Draw(batch);
            }
        }
    }
}