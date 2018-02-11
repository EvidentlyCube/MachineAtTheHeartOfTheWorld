using System;
using Microsoft.Xna.Framework.Graphics;

namespace IrregularMachine.Core {
    public class SceneManager {
        private IScene _currentScene;

        public SceneManager(IScene currentScene) {
            Logger.Debug($"SceneManager::constructor({currentScene?.GetType()})");

            _currentScene = currentScene ?? throw new ArgumentNullException(nameof(currentScene));
            _currentScene.OnSet();
        }

        public void Update() {
            _currentScene.Update();
        }

        public void Draw(SpriteBatch batch) {
            _currentScene.Draw(batch);
        }

        public void SetScene(IScene scene) {
            Logger.Debug($"SceneManager::SetScene({scene.GetType()})");
            
            _currentScene?.OnUnset();
            _currentScene = scene;
            _currentScene.OnSet();
        }
    }
}