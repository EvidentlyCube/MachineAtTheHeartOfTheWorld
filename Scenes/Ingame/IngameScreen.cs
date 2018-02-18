using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using IrregularMachine.BitmapTexts;
using IrregularMachine.Core;
using IrregularMachine.Core.Tweens;
using IrregularMachine.IrregularEngine;
using IrregularMachine.IrregularEngine.Data;
using IrregularMachine.IrregularEngine.Parser;
using IrregularMachine.Scenes.Ingame.particles;
using IrregularMachine.Scenes.Outro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;

namespace IrregularMachine.Scenes.Ingame {
    public class IngameScreen {
        private readonly float _renderOffset;

        public bool IsCompleted { get; private set; }

        private bool _isHiding;

        private readonly List<EngineActionType> _solution;
        private readonly List<RendererActionBrick> _actionBrickRenders;
        private readonly List<RendererGlyph> _glyphRenderers;

        private readonly List<ParticleSmoke> _smokeParticles;
        private readonly List<ParticleStone> _stoneParticles;
        private readonly List<IngameScreenMessage> _messages;

        private readonly IngameScene _scene;

        public IngameScreen(IngameScene scene, float renderOffset, EngineScreen engineScreen, IngameScreenMessage[] messages) {
            _scene = scene;
            _renderOffset = renderOffset;
            _actionBrickRenders = new List<RendererActionBrick>();
            _glyphRenderers = new List<RendererGlyph>();
            _smokeParticles = new List<ParticleSmoke>();
            _stoneParticles = new List<ParticleStone>();
            _messages = new List<IngameScreenMessage>(messages);
            IsCompleted = false;

            var parser = new EngineParser(engineScreen.Tiles);
            _solution = parser.Parse();

            if (_solution.Count > S.MaxActionsInLevel) {
                throw new Exception("Screen has more than 9 actions in its solution");
            }

            for (var x = 0; x < engineScreen.Width; x++) {
                for (var y = 0; y < engineScreen.Height; y++) {
                    if (engineScreen.Tiles[x, y].Type == EngineGlyphType.Nothing) {
                        continue;
                    }

                    _glyphRenderers.Add(new RendererGlyph(engineScreen.Tiles[x, y].Type, x, y, engineScreen.Width,
                        engineScreen.Height));
                }
            }
        }

        public void Update(float sceneOffset, bool controlsEnabled) {
            _smokeParticles.ForEach(x => x.Update());
            _smokeParticles.RemoveAll(x => x.IsFinished);
            _stoneParticles.ForEach(x => x.Update());
            _stoneParticles.RemoveAll(x => x.IsFinished);
            
            if (Math.Abs(sceneOffset - _renderOffset) >= 0.5 || !controlsEnabled) {
                return;
            }

            if (_messages.Count > 0) {
                _scene.SubtitleWidget.AddDelay();
                _messages.ForEach(message => _scene.SubtitleWidget.AddSubtitle(message.Text, message.VoiceOver));
                _messages.Clear();
            }

            for (var i = 0; i < _actionBrickRenders.Count; i++) {
                _actionBrickRenders[i].Update(_actionBrickRenders.Count, i);
            }

            if (IsCompleted) {
                return;
            }

            _glyphRenderers.ForEach(glyph => glyph.Update());

            var actionInput = GetActionInput();
            if (actionInput.HasValue && _actionBrickRenders.Count < S.MaxActionsInLevel && !_isHiding) {
                _actionBrickRenders.Add(new RendererActionBrick(actionInput.Value, _actionBrickRenders.Count));
                Sfx.PlayGlassSound(actionInput.Value);
            }

            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.Enter) && !_isHiding) {
                if (CompareLists(_actionBrickRenders, _solution)) {
                    if (_renderOffset == S.LastLevelIndex) {
                        Save.LastLevel = S.LastLevelIndex + 1;
                        Save.WriteSave();
                        Sfx.MusicInstance.Stop();
                        GameCore.Instance.SceneManager.SetScene(new OutroSmackScene());
                        return;
                    }
                    
                    IsCompleted = true;
                    GameCore.Instance.ShakePower += 2;
                    _smokeParticles.Add(new ParticleSmoke());
                    _smokeParticles.Add(new ParticleSmoke());
                    _smokeParticles.Add(new ParticleSmoke());
                    _smokeParticles.Add(new ParticleSmoke());
                    _smokeParticles.Add(new ParticleSmoke());
                    _smokeParticles.Add(new ParticleSmoke());
                    _smokeParticles.Add(new ParticleSmoke());
                    _smokeParticles.Add(new ParticleSmoke());
                    _smokeParticles.Add(new ParticleSmoke());
                    _stoneParticles.Add(new ParticleStone());
                    _stoneParticles.Add(new ParticleStone());
                    _stoneParticles.Add(new ParticleStone());
                    _stoneParticles.Add(new ParticleStone());
                    _stoneParticles.Add(new ParticleStone());
                    _stoneParticles.Add(new ParticleStone());
                    _stoneParticles.Add(new ParticleStone());
                    _stoneParticles.Add(new ParticleStone());
                    _stoneParticles.Add(new ParticleStone());
                    _stoneParticles.Add(new ParticleStone());
                    _stoneParticles.Add(new ParticleStone());
                    _stoneParticles.Add(new ParticleStone());
                    _stoneParticles.Add(new ParticleStone());
                    Sfx.PlayLevelCompleted();
                    Save.LastLevel = (int)_renderOffset + 2;
                    Save.WriteSave();
                }
                else {    
                    for (var i = 0; i < _actionBrickRenders.Count; i++) {
                        _actionBrickRenders[i].IsHiding = true;
                        _actionBrickRenders[i].HidingDelay = i * 3;
                        
                        GameCore.Instance.RegisterDelayedAction(i * 3, GetSfxReverseSoundAction(_actionBrickRenders[i].Action));
                    }

                    _isHiding = true;
                }
            }

            if (_isHiding) {
                if (_actionBrickRenders.All(brick => brick.IsFinished)) {
                    _actionBrickRenders.Clear();
                    _isHiding = false;
                }
            }
            else {
                _actionBrickRenders.RemoveAll(x => x.IsFinished);
            }

            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.Back) && !_isHiding && _actionBrickRenders.Count > 0) {
                var brick = _actionBrickRenders.FindLast(x => !x.IsHiding);
                if (brick != null) {
                    brick.IsHiding = true;
                    Sfx.PlayGlassReverseSound(brick.Action);
                }
            }
        }

        public void Draw(float sceneOffset, SpriteBatch batch) {
            if (Math.Abs(sceneOffset - _renderOffset) < 1.5) {
                var offsetX = (_renderOffset - sceneOffset) * S.ViewportWidth;
    
                _glyphRenderers.ForEach(glyph => glyph.Draw(batch, offsetX));
                _actionBrickRenders.ForEach(x => x.Draw(batch, offsetX));
                _smokeParticles.ForEach(x => x.Draw(batch, offsetX));
                _stoneParticles.ForEach(x => x.Draw(batch, offsetX));
                batch.Draw(Gfx.bg_InputFrame, new Vector2(S.InputFrameX + offsetX, S.InputFrameY), Color.White);
            }
        }

        private bool CompareLists(List<RendererActionBrick> left, List<EngineActionType> right) {
            if (left.Count != right.Count) {
                return false;
            }

            return !left.Where((t, i) => t.Action != right[i]).Any();
        }

        private EngineActionType? GetActionInput() {
            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.Q) ||
                KeyboardManager.Instance.IsKeyJustPressed(Keys.NumPad7)) {
                return EngineActionType.MoveNW;
            }

            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.W) ||
                KeyboardManager.Instance.IsKeyJustPressed(Keys.NumPad8)) {
                return EngineActionType.MoveN;
            }

            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.E) ||
                KeyboardManager.Instance.IsKeyJustPressed(Keys.NumPad9)) {
                return EngineActionType.MoveNE;
            }

            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.A) ||
                KeyboardManager.Instance.IsKeyJustPressed(Keys.NumPad4)) {
                return EngineActionType.MoveW;
            }

            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.S) ||
                KeyboardManager.Instance.IsKeyJustPressed(Keys.NumPad5)) {
                return EngineActionType.Special;
            }

            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.D) ||
                KeyboardManager.Instance.IsKeyJustPressed(Keys.NumPad6)) {
                return EngineActionType.MoveE;
            }

            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.Z) ||
                KeyboardManager.Instance.IsKeyJustPressed(Keys.NumPad1)) {
                return EngineActionType.MoveSW;
            }

            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.X) ||
                KeyboardManager.Instance.IsKeyJustPressed(Keys.NumPad2)) {
                return EngineActionType.MoveS;
            }

            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.C) ||
                KeyboardManager.Instance.IsKeyJustPressed(Keys.NumPad3)) {
                return EngineActionType.MoveSE;
            }

            return null;
        }

        private Action GetSfxReverseSoundAction(EngineActionType actionType) {
            return () => Sfx.PlayGlassReverseSound(actionType);
        }

        public void MarkDone() {
            IsCompleted = true;
            var i = 0;
            _solution.ForEach(x => _actionBrickRenders.Add(new RendererActionBrick(x, i++, _solution.Count)));
        }
    }
}