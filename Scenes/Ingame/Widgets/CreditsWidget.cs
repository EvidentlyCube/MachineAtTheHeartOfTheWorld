using System;
using IrregularMachine.BitmapTexts;
using IrregularMachine.Core;
using IrregularMachine.Core.Tweens;
using Microsoft.Xna.Framework.Graphics;

namespace IrregularMachine.Scenes.Ingame.Widgets {
    public class CreditsWidget {
        private const int Padding = 40;
        private readonly TweenSequence _tween;
        private readonly BitmapText _header;
        private readonly BitmapText _body;

        public bool IsRunning;
        public bool IsFinished;

        public CreditsWidget() {
            _tween = new TweenSequence(() => IsFinished = true);
            _header = new BitmapText(Gfx.BitmapFontOutline, "<header>");
            _body = new BitmapText(Gfx.BitmapFontOutline, "<body>");

            _header.FontScale = 0.75f;
            _body.FontScale = 0.5f;
            
            _header.Width = S.ViewportWidth;
            _body.Width = S.ViewportWidth;

            _body.Height = _body.EffectiveLineHeight * 3;
            _header.Alpha = 0;
            _body.Alpha = 0;
            _header.X = Padding;
            _header.Y = Padding;
            _body.X = Padding;
            _body.Y = _header.Y + _header.Height * _header.FontScale;
            
            AddCredit("A game by", "Evidently Cube");
            AddCredit("Graphics:", "Aleksander Kowalczyk");
            AddCredit("Music:", "Jakub \"AceMan\" Szelag\n\"The Eldest Tribe\"");
            AddCredit("Voice over:", "Alex Diener");
            AddCredit("Sound effects:", "freesfx.co.uk\nhttp://freesound.org");
            AddCredit("Story, programming, levels, game design:", "Maurycy Zarzycki");
            AddCredit("Machine at the Heart of the World", "Made for MonoGameJam 2018");
        }

        private void AddCredit(string header, string body) {
            _tween.AddTween(new TweenCallback(GetSetTextAction(header, body)));
            _tween.AddTween(new TweenFloat(0, 1, 30, x => {
                _header.Alpha = x;
                _body.Alpha = x;
            }));
            _tween.AddTween(new TweenSleep(160));
            _tween.AddTween(new TweenFloat(1, 0, 30, x => {
                _header.Alpha = x;
                _body.Alpha = x;
            }));
            _tween.AddTween(new TweenSleep(30));
        }

        private Action GetSetTextAction(string header, string body) {
            return () => {
                _header.Text = header;
                _body.Text = body;
            };
        }

        public void Update() {
            if (IsRunning && !IsFinished) {
                _tween.Update();
            }
        }

        public void Draw(SpriteBatch batch) {
            if (!IsFinished && IsRunning) {
                _header.Draw(batch);
                _body.Draw(batch);
            }
        }
    }
}