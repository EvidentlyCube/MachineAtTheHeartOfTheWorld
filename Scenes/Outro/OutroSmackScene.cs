using IrregularMachine.Core;
using IrregularMachine.Core.Tweens;
using Microsoft.Xna.Framework.Graphics;

namespace IrregularMachine.Scenes.Outro {
    public class OutroSmackScene : IScene {
        private TweenSequence _tween;
        
        public void OnSet() {
            _tween = new TweenSequence(() => GameCore.Instance.SceneManager.SetScene(new OutroScene()));
            _tween.AddTween(new TweenSleep(30));
            _tween.AddTween(new TweenCallback(() => Sfx.Punch.Play(Save.SoundVolume, 0, 0)));
            _tween.AddTween(new TweenSleep(30));
            _tween.AddTween(new TweenCallback(() => Sfx.BodyFall.Play(Save.SoundVolume, 0, 0)));
            _tween.AddTween(new TweenSleep(150));
        }

        public void OnUnset() {
        }
        
        public void Update() {
            _tween.Update();
        }

        public void Draw(SpriteBatch batch) {
        }

    }
}