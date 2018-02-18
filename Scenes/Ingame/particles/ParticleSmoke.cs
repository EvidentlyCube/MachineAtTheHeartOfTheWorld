using System;
using IrregularMachine.Core;
using IrregularMachine.Core.Tweens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace IrregularMachine.Scenes.Ingame.particles {
    public class ParticleSmoke {
        private static readonly Random _random = new Random();
        private readonly TweenParallel _tween;

        private Vector2 _position;
        private readonly float _scale;
        private float _rotation;
        private float _alpha;
        private int _delay;

        public bool IsFinished { get; private set; }

        public ParticleSmoke() {
            _tween = new TweenParallel(() => IsFinished = true);

            _position = new Vector2(_random.Next(0, S.ViewportWidth), -396);
            _rotation = (float) (2 * Math.PI * _random.NextDouble());
            _scale = 1.5f + (float) _random.NextDouble() * 0.5f;
            _alpha = 1f;
            _delay = _random.Next(0, 5);

            var duration = _random.Next(40, 60);
            var targetY = 500 + (float)_random.NextDouble() * 400;
            var rotationDirection = _random.NextDouble() < 0.5 ? 1 : -1; 
            
            _tween.AddTween(new TweenFloatEaseOut(_position.Y, targetY, duration * 5 / 6, y => _position.Y = y));
            _tween.AddTween(new TweenFloatEaseOut(_rotation, _rotation + (float)Math.PI * 0.5f * rotationDirection, duration, x => _rotation = x));
            _tween.AddTween(new TweenFloatEaseOut(1, 0, duration, x => _alpha = x));
        }

        public void Update() {
            if (_delay > 0) {
                _delay--;
                return;
            }
            
            _tween.Update();
        }

        public void Draw(SpriteBatch batch, float offsetX) {
            var texture = Gfx.sheet_Particles["particle_dust"];
            batch.Draw(
                texture,
                new Vector2(_position.X + offsetX, _position.Y),
                new Color(1f, 1f, 1f, _alpha),
                _rotation,
                new Vector2(texture.Width / 2f, texture.Height / 2f),
                new Vector2(_scale),
                SpriteEffects.None,
                0
            );
        }
    }
}