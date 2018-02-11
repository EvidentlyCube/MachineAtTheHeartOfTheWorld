using System;
using System.Diagnostics;
using IrregularMachine.Extensions;
using IrregularMachine.Scenes.Ingame;
using IrregularMachine.Scenes.InMemoriam;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IrregularMachine.Core {
    public class GameCore : Game {
//        public new static GameCore Instance { get; private set; }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SceneManager _sceneManager;

        private RenderTarget2D _renderTarget;

        public GameCore() {
            Logger.Init("GameCore::constructor");
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent() {
            Logger.Init("GameCore::LoadContent()");
            _renderTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Content.RootDirectory = "Content";

            Gfx.Load(GraphicsDevice, Content);

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
//            _sceneManager = new SceneManager(new InMemoriamScene());
            _sceneManager = new SceneManager(new IngameScene());
        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);

            KeyboardManager.Instance.Update();
            _sceneManager.Update();
        }

        protected override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.SetRenderTarget(_renderTarget);
            _sceneManager.Draw(_spriteBatch);
            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(samplerState: SamplerState.AnisotropicClamp);
            _spriteBatch.Draw(_renderTarget, GetTargetRectangle(), Color.White);
            _spriteBatch.End();
//            var scaleMatrix = Matrix.Identity;
        }

        private Rectangle GetTargetRectangle() {
            var viewport = GraphicsDevice.Viewport.Bounds;
            var targetRectangle = new Rectangle(0, 0, S.ViewportWidth, S.ViewportHeight);

            if (viewport.GetRatio() < S.ViewportRatio) {
                targetRectangle.Width = viewport.Width;
                targetRectangle.Height = (int)Math.Floor(viewport.Width / S.ViewportRatio);
            } else {
                targetRectangle.Height = viewport.Height;
                targetRectangle.Width = (int)Math.Floor(viewport.Height * S.ViewportRatio);
            }
            
            targetRectangle.X = (viewport.Width - targetRectangle.Width) / 2;
            targetRectangle.Y = (viewport.Height - targetRectangle.Height) / 2;

            return targetRectangle;
        }
    }
}