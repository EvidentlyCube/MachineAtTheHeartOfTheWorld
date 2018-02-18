using System;
using System.Collections.Generic;
using System.Drawing;
using IrregularMachine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using Color = Microsoft.Xna.Framework.Color;

namespace IrregularMachine.Scenes.Ingame {
    public class RendererAnimation {
        private const float CogAnimationSpeed = 0.46f;
        
        private readonly List<TextureRegion2D> _frames;
        private float _currentFrame;
        private readonly float _animationSpeed;
        private readonly Size _expectedSize;

        public RendererAnimation(List<TextureRegion2D> frames, float currentFrame, float animationSpeed, Size expectedSize) {
            _frames = frames ?? throw new ArgumentNullException(nameof(frames));
            _currentFrame = currentFrame;
            _animationSpeed = animationSpeed;
            _expectedSize = expectedSize;
        }

        public void Update() {
            _currentFrame += _animationSpeed;
            
            if (_currentFrame < 0) {
                _currentFrame += _frames.Count;
            } else if (_currentFrame >= _frames.Count) {
                _currentFrame -= _frames.Count;                
            }
        }
        
        public void Draw(SpriteBatch batch, float x, float y) {
            var frame = _frames[(int) Math.Floor(_currentFrame)];
            batch.Draw(
                frame,
                new Vector2(
                    x + (_expectedSize.Width - frame.Width) / 2f, 
                    y + (_expectedSize.Height - frame.Height) / 2f
                ), 
                Color.White
                );
        }

        public static RendererAnimation GetCogAnimation(int cogType, bool isEven) {
            if (cogType == 6) {
                return new RendererAnimation(new List<TextureRegion2D> {
                    Gfx.sheet_Ingame[$"gear_{cogType}_00"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_01"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_02"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_03"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_04"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_05"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_06"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_07"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_08"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_09"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_10"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_11"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_12"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_13"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_14"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_15"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_16"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_17"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_18"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_19"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_20"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_21"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_22"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_23"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_24"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_25"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_26"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_27"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_28"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_29"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_30"],
                    Gfx.sheet_Ingame[$"gear_{cogType}_31"]
                }, isEven ? 8 : 0, isEven ? -CogAnimationSpeed : CogAnimationSpeed, new Size(S.GlyphCogSize, S.GlyphCogSize));
            }
            
            return new RendererAnimation(new List<TextureRegion2D> {
                Gfx.sheet_Ingame[$"gear_{cogType}_00"],
                Gfx.sheet_Ingame[$"gear_{cogType}_01"],
                Gfx.sheet_Ingame[$"gear_{cogType}_02"],
                Gfx.sheet_Ingame[$"gear_{cogType}_03"],
                Gfx.sheet_Ingame[$"gear_{cogType}_04"],
                Gfx.sheet_Ingame[$"gear_{cogType}_05"],
                Gfx.sheet_Ingame[$"gear_{cogType}_06"],
                Gfx.sheet_Ingame[$"gear_{cogType}_07"],
                Gfx.sheet_Ingame[$"gear_{cogType}_08"],
                Gfx.sheet_Ingame[$"gear_{cogType}_09"],
                Gfx.sheet_Ingame[$"gear_{cogType}_10"],
                Gfx.sheet_Ingame[$"gear_{cogType}_11"],
                Gfx.sheet_Ingame[$"gear_{cogType}_12"],
                Gfx.sheet_Ingame[$"gear_{cogType}_13"],
                Gfx.sheet_Ingame[$"gear_{cogType}_14"],
                Gfx.sheet_Ingame[$"gear_{cogType}_15"]
            }, isEven ? 8 : 0, isEven ? -CogAnimationSpeed : CogAnimationSpeed, new Size(S.GlyphCogSize, S.GlyphCogSize));
        }
    }
}