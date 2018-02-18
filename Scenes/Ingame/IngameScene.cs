using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using IrregularMachine.Core;
using IrregularMachine.Scenes.Ingame.Widgets;
using IrregularMachine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IrregularMachine.Scenes.Ingame {
    public class IngameScene : IScene {
        private readonly ReadOnlyCollection<IngameScreen> _screens;
        private float _currentScreenRenderOffset;
        private int _currentlySelectedScreen;
        private readonly PauseView _pauseView;
        private readonly ControlsWidget _controlsWidget;
        private readonly CreditsWidget _creditsWidget;
        public readonly SubtitleWidget SubtitleWidget;

        private readonly LevelProceedBlockEffect _levelProceedBlockEffect;
        private float RenderOffset => _currentScreenRenderOffset + _levelProceedBlockEffect.Offset;

        private bool IsWalking => Math.Abs(_currentScreenRenderOffset - _currentlySelectedScreen) > 0.05 ||
                                  _levelProceedBlockEffect.IsAnimating;

        private int _walkSfxTimeout;


        public IngameScene() {
            var engineScreens = ScreensCollection.GetScreens();
            Logger.Debug($"Loaded '{engineScreens.Count}' screens");
            var ingameScreens = new List<IngameScreen>(engineScreens.Count);
            for (var i = 0; i < engineScreens.Count; i++) {
                ingameScreens.Add(new IngameScreen(this, i, engineScreens[i].EgineScreen, engineScreens[i].Messages));
            }

            for (var i = 0; i < Save.LastLevel; i++) {
                ingameScreens[i].MarkDone();
            }

            _screens = ingameScreens.AsReadOnly();
            _currentlySelectedScreen = Save.LastLevel;
            _currentScreenRenderOffset = Save.LastLevel;
                
            _levelProceedBlockEffect = new LevelProceedBlockEffect();
            _pauseView = new PauseView();

            _controlsWidget = new ControlsWidget {IsHiding = Save.LastLevel > 1};
            _creditsWidget = new CreditsWidget();
            SubtitleWidget = new SubtitleWidget();

            if (Save.LastLevel > 0) {
                _creditsWidget.IsFinished = true;
            }
        }

        public void OnSet() {
            _currentlySelectedScreen = Save.LastLevel;
            _currentScreenRenderOffset = Save.LastLevel;
        }

        public void OnUnset() {
        }

        public void Update() {
            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.Escape)) {
                _pauseView.IsActive = true;
            }
            
            if (_pauseView.IsActive) {
                _pauseView.Update();
                return;
            }
            
            if (_walkSfxTimeout > 0) {
                _walkSfxTimeout--;
                
            } else if (IsWalking) {
                Sfx.PlayStepSound();
                _walkSfxTimeout = 10;
            }
            
            _levelProceedBlockEffect.Update();

            if (_screens[_currentlySelectedScreen].IsCompleted) {
                _creditsWidget.IsRunning = true;
            }
            
            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.Left)) {
                Logger.Input("Switching screen to the left");
                _currentlySelectedScreen = Math.Max(0, _currentlySelectedScreen - 1);
                
            } else if (KeyboardManager.Instance.IsKeyJustPressed(Keys.Right)) {
                Logger.Input("Switching screen to the right");
                if (_screens[_currentlySelectedScreen].IsCompleted) {
                    _currentlySelectedScreen = Math.Min(_screens.Count - 1, _currentlySelectedScreen + 1);
                }
                else {
                    _levelProceedBlockEffect.Start();
                }
            }

            _currentScreenRenderOffset =
                (float) MathUtils.ApproachFactor(_currentScreenRenderOffset, _currentlySelectedScreen, 0.3, 0.001, 0.1);
            
            foreach (var ingameScreen in _screens) {
                ingameScreen.Update(RenderOffset, true);
            }

            _controlsWidget.Update();
            _creditsWidget.Update();
            SubtitleWidget.Update();
            _controlsWidget.IsHiding = _controlsWidget.IsHiding || _currentlySelectedScreen > 1;
        }

        public void Draw(SpriteBatch batch) {
            batch.Begin(blendState: CustomBlendStates.RegularWithAlpha, samplerState:SamplerState.AnisotropicClamp);
            batch.Draw(Gfx.bg_Backgorund, new Vector2(
                -RenderOffset * S.ViewportWidth % S.ViewportWidth, 0
            ), Color.White);
            
            batch.Draw(Gfx.bg_Backgorund, new Vector2(
                -RenderOffset * S.ViewportWidth % S.ViewportWidth + S.ViewportWidth, 0
            ), Color.White);
            
            foreach (var ingameScreen in _screens) {
                ingameScreen.Draw(RenderOffset, batch);
            }

            _controlsWidget.Draw(batch);
            _creditsWidget.Draw(batch);
            SubtitleWidget.Draw(batch);
      
            _pauseView.Draw(batch);
            
            batch.End();
        }
    }
}