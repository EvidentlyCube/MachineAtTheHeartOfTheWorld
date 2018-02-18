using System;
using System.Drawing;
using IrregularMachine.BitmapTexts;
using IrregularMachine.Core;
using IrregularMachine.Core.Tweens;
using IrregularMachine.Scenes.Ingame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;

namespace IrregularMachine.Scenes.Intro {
    public class IntroScene : IScene {
        private const int TextFadeInDuration = 60;
        private const int TextDisplayDuration = 180;
        private const int TextFadeOutDuration = 60;
        
        private BitmapText _text;
        private TweenSequence _tweenSequence;

        private Texture2D _loadedTexture;
        private float _textureAlpha;

        public IntroScene() {
            _text = new BitmapText(Gfx.BitmapFont, new SizeF(S.ViewportWidth, 400));
            _text.FontScale = 0.75f;
            _text.HorizontalAlign = BitmapTextHorizontalAlign.Center;
            _text.VerticalAlign = BitmapTextVerticalAlign.Middle;
            _text.Position = new PointF(0, S.ViewportHeight - _text.Height);
        }

        public void OnSet() {
            _text.Alpha = 0;
            _textureAlpha = 0;
            _tweenSequence = new TweenSequence(() => GameCore.Instance.SceneManager.SetScene(new IngameScene()));
            
            AddFadeTextureIn(0);
            AddTextTween("The world stopped. A change that rippled through the entire planet, maybe even all of reality and left the humanity confused.");
            AddTextTween("The time of day was frozen, plants wouldn't change, animals would stay around the same place, machines became indifferent.");
            AddTextTween("And whenever you changed something it would come back to the way it was when no one was looking.");
            AddFadeTextureOut();
            
            AddFadeTextureIn(1);
            AddTextTween("I was a researcher, one of many who tried to understand this change, but our work was fruitless.");
            AddTextTween("Our instruments wouldn't work and the results were inconsistent. While we worked in our laboratories, the world went through all types of chaos and apathy.");
            AddTextTween("After what seemed liked decades of wasted time I found myself travelling the world, reaching the barren deserts of Arctic.");
            AddTextTween("This is where I found the entrance to a cave that took me to the center of the earth.");
            AddFadeTextureOut();
            
            AddFadeTextureIn(2);
            AddTextTween("I did not how I knew it, but as soon as I laid my eyes on the enormous machine I knew it was the root of the problem.");
            AddTextTween("A mechanism that avoids understanding was ticking silently.");
            AddTextTween("The first one to greet me was its caretaker - a dried up body of a small man, his left temple horribly broken.");
            AddTextTween("The second to greet me were complex, stone tablets mounted on the walls, inviting me to touch them and discover their secrets.");
            AddFadeTextureOut();
        }

        public void Update() {
            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.Space)) {
                _tweenSequence.GoToNext();
                while (_tweenSequence.CurrentTween != null && !(_tweenSequence.CurrentTween is TweenSleep)) {
                    _tweenSequence.GoToNext();                    
                }
            } else if (KeyboardManager.Instance.IsKeyDown(Keys.Escape)) {
                GameCore.Instance.SceneManager.SetScene(new IngameScene());
            }

            _tweenSequence.Update();    
        }

        public void Draw(SpriteBatch batch) {
            batch.Begin(samplerState:SamplerState.AnisotropicClamp, blendState: CustomBlendStates.RegularWithAlpha);
            if (_loadedTexture != null) {
                batch.Draw(_loadedTexture, Vector2.Zero, new Color(1f, 1f, 1f, _textureAlpha));
            }
            _text.Draw(batch);
            batch.End();
        }

        public void OnUnset() {
            TryToUnloadCurrentTexture();
        }

        private void AddTextTween(string text, int textDisplayDuration = TextDisplayDuration) {
            _tweenSequence.AddTween(new TweenCallback(() => _text.Text = text));
            _tweenSequence.AddTween(new TweenFloat(0, 1, TextFadeInDuration, alpha => _text.Alpha = alpha));
            _tweenSequence.AddTween(new TweenSleep(textDisplayDuration));
            _tweenSequence.AddTween(new TweenFloat(1, 0, TextFadeOutDuration, alpha => _text.Alpha = alpha));
        }

        private void AddFadeTextureIn(int index) {
            _tweenSequence.AddTween(new TweenCallback(TryToUnloadCurrentTexture));
            _tweenSequence.AddTween(new TweenCallback(() => _loadedTexture = Gfx.LoadIntroTexture(index)));
            _tweenSequence.AddTween(new TweenFloat(0, 1, TextFadeInDuration, alpha => {
                Console.WriteLine(alpha);
                _textureAlpha = alpha;
            }));
        }

        private void AddFadeTextureOut() {
            _tweenSequence.AddTween(new TweenFloat(1, 0, TextFadeOutDuration, alpha => _textureAlpha = alpha));
        }

        private void TryToUnloadCurrentTexture() {
            _loadedTexture?.Dispose();
            _loadedTexture = null;
        }
    }
}