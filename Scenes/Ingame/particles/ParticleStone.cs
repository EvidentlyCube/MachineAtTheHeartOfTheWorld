using System;
using IrregularMachine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace IrregularMachine.Scenes.Ingame.particles {
    public class ParticleStone {
        private const float Acceleration = 1f;
        private static Random _random = new Random();
        private TextureRegion2D _texture;
        private Vector2 _position;
        private float _scale;
        private float _rotation;
        private float _rotationSpeed;
        private float _speed;

        public bool IsFinished => _position.Y > S.ViewportHeight + _texture.Height;

        public ParticleStone() {
            _texture = GetRandomTexture();
            _position = new Vector2(_random.Next(0, S.ViewportWidth), _random.Next(-500, -100));
            _scale = 0.2f + (float)_random.NextDouble() * 0.8f;
            _rotation = (float) (Math.PI * _random.NextDouble() * 2);
            _rotationSpeed = (float) (Math.PI * _random.NextDouble() * 0.01f);

            _speed = 0;
            _rotationSpeed *= _random.NextDouble() < 0.5 ? 1 : -1;
        }

        public void Update() {
            _speed += Acceleration * _scale;
            _rotation += _rotationSpeed;
            _position.Y += _speed;
        }

        public void Draw(SpriteBatch batch, float offsetX) {
            batch.Draw(
                _texture,
                new Vector2(_position.X + offsetX, _position.Y),
                Color.White,
                _rotation,
                new Vector2(_texture.Width / 2f, _texture.Height /2f),
                new Vector2(_scale),
                SpriteEffects.None,
                0
            );
        }

        private TextureRegion2D GetRandomTexture() {
            switch (_random.Next(0, 4)) {
                    case 0: return Gfx.sheet_Particles["particle_rock1"];
                    case 1: return Gfx.sheet_Particles["particle_rock2"];
                    case 2: return Gfx.sheet_Particles["particle_rock3"];
                    case 3: return Gfx.sheet_Particles["particle_rock4"];
                        default:
                            throw new Exception("Invalid random for rock particle texture");
            }
        }
    }
}