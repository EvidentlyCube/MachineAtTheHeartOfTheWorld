using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IrregularMachine.Core;
using IrregularMachine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IrregularMachine.Scenes.Ingame {
    public class IngameScene : IScene {
        private readonly ReadOnlyCollection<IngameScreen> _screens;
        private float _currentScreenRenderOffset;
        private int _currentlySelectedScreen;
        private bool _isLeftDown;

        public IngameScene() {
            var engineScrens = ScreensCollection.GetScreens();
            Logger.Debug($"Loaded '{engineScrens.Count}' screens");
            var ingameScreens = new List<IngameScreen>(engineScrens.Count);
            for (var i = 0; i < engineScrens.Count; i++) {
                ingameScreens.Add(new IngameScreen(i, engineScrens[i]));
            }

            _screens = ingameScreens.AsReadOnly();
            _currentlySelectedScreen = 0;
        }

        public void OnSet() {
            _currentScreenRenderOffset = 0;
        }

        public void OnUnset() {
        }

        public void Update() {
            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.Left)) {
                Logger.Input("Switching screen to the left");
                _currentlySelectedScreen = Math.Max(0, _currentlySelectedScreen - 1);
            } else if (KeyboardManager.Instance.IsKeyJustPressed(Keys.Right)) {
                Logger.Input("Switching screen to the right");
                _currentlySelectedScreen = Math.Min(_screens.Count - 1, _currentlySelectedScreen + 1);
            }

            _currentScreenRenderOffset =
                (float) MathUtils.ApproachFactor(_currentScreenRenderOffset, _currentlySelectedScreen, 0.3, 0.001, 0.1);
            
            foreach (var ingameScreen in _screens) {
                ingameScreen.Update();
            }
        }

        public void Draw(SpriteBatch batch) {
            batch.Begin(blendState: BlendState.NonPremultiplied);
            batch.Draw(Gfx.bg_Backgorund1, new Vector2(
                -_currentScreenRenderOffset * S.ViewportWidth % S.ViewportWidth, 0
            ));
            
            batch.Draw(Gfx.bg_Backgorund1, new Vector2(
                -_currentScreenRenderOffset * S.ViewportWidth % S.ViewportWidth + S.ViewportWidth, 0
            ));
            
            foreach (var ingameScreen in _screens) {
                ingameScreen.Draw(_currentScreenRenderOffset, batch);
            }
            batch.End();
        }
    }
}