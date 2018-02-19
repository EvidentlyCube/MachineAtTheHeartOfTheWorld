using System;
using System.Collections.Generic;
using System.Linq;
using IrregularMachine.Extensions;
using IrregularMachine.IrregularEngine;
using IrregularMachine.IrregularEngine.Data;
using IrregularMachine.IrregularEngine.Parser;
using IrregularMachine.IrregularEngine.Serializer;
using IrregularMachine.Scenes.Ingame;
using IrregularMachine.Scenes.InMemoriam;
using IrregularMachine.Scenes.Intro;
using IrregularMachine.Scenes.Outro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace IrregularMachine.Core {
    public class GameCore : Game {
        public static GameCore Instance { get; private set; }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public SceneManager SceneManager { get; private set; }

        private RenderTarget2D _renderTarget;

        public float ShakePower;
        private Random _random = new Random();

        private readonly List<DelayedAction> _delayActions;

        public GameCore() {
            Logger.Init("GameCore::constructor");
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Instance = this;
            IsFixedTimeStep = true;
            MaxElapsedTime = TargetElapsedTime;
            
            _delayActions = new List<DelayedAction>();
        }

        protected override void LoadContent() {
            Logger.Init("GameCore::LoadContent()");
            _renderTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Content.RootDirectory = "Content";

            Gfx.Load(Content);
            Sfx.Load(Content);

            base.LoadContent();
        }

        protected override void Initialize() {
            Logger.Init("GameCore::Initialize()");
            base.Initialize();

            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            _graphics.PreferredBackBufferWidth = S.ViewportWidth;
            _graphics.PreferredBackBufferHeight = S.ViewportHeight;

            StartGame();
        }

        private void StartGame() {
            _graphics.IsFullScreen = true;
            _graphics.HardwareModeSwitch = false;
            _graphics.ApplyChanges();
            
            Save.LoadSave();

            if (Save.LastLevel == S.LastLevelIndex + 1) {
                SceneManager = new SceneManager(new OutroScene());                
            }
            else {
                Sfx.MusicInstance.Volume = Save.MusicVolume;
                Sfx.MusicInstance.Play();
                SceneManager = new SceneManager(new InMemoriamScene());                
            }
            
        }

        protected override void Update(GameTime gameTime) {
            if (ShakePower > 0) {
                ShakePower -= 0.1f;
            }
            
            _delayActions.ForEach(x => {
                x.Delay--;
                if (x.Delay <= 0) {
                    x.Action();
                }
            });
            _delayActions.RemoveAll(x => x.Delay <= 0);
            
            base.Update(gameTime);

            KeyboardManager.Instance.Update();
            SceneManager.Update();
        }

        protected override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.SetRenderTarget(_renderTarget);
            SceneManager.Draw(_spriteBatch);
            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(samplerState: SamplerState.AnisotropicClamp);
            _spriteBatch.Draw(_renderTarget, GetTargetRectangle(), Color.White);
            _spriteBatch.End();
//            var scaleMatrix = Matrix.Identity;
        }

        public void RegisterDelayedAction(int delayInFrames, Action callback) {
            _delayActions.Add(new DelayedAction(delayInFrames, callback));
        }

        private Rectangle GetTargetRectangle() {
            var angle = _random.NextDouble() * Math.PI * 2;
            var offset = new Vector2(
                (float)(Math.Cos(angle) * ShakePower * 10),
                (float)(Math.Sin(angle) * ShakePower * 10)
            );
            var viewport = GraphicsDevice.Viewport.Bounds;
            var targetRectangle = new Rectangle(0, 0, S.ViewportWidth, S.ViewportHeight);

            if (viewport.GetRatio() < S.ViewportRatio) {
                targetRectangle.Width = viewport.Width;
                targetRectangle.Height = (int)Math.Floor(viewport.Width / S.ViewportRatio);
            } else {
                targetRectangle.Height = viewport.Height;
                targetRectangle.Width = (int)Math.Floor(viewport.Height * S.ViewportRatio);
            }
            
            targetRectangle.X = (viewport.Width - targetRectangle.Width) / 2 + (int)offset.X;
            targetRectangle.Y = (viewport.Height - targetRectangle.Height) / 2 + (int)offset.Y;

            return targetRectangle;
        }
        
        public class DelayedAction {
            public int Delay;
            public Action Action;

            public DelayedAction(int delay, Action action) {
                Delay = delay;
                Action = action ?? throw new ArgumentNullException(nameof(action));
            }
        }
    }
}