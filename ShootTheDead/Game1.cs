using Microsoft.Xna.Framework.Input;
using ShootTheDead.Managers;
using ShootTheDead.States;
using System.Diagnostics;
using static ShootTheDead.States.State;

namespace ShootTheDead
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private bool _isResizing;
        private State _currentState;
        private State _nextState;
        public static Game1 Instance { get; private set; }

        public Game1()
        {
            Debug.WriteLine("Game1 constructor");
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //Window.AllowUserResizing = true;
            graphics.IsFullScreen = false;
            Instance = this;
        }

        public void OnClientSizeChanged()
        {
            graphics.PreferredBackBufferWidth = Globals._virtualWidth;
            graphics.PreferredBackBufferHeight = Globals._virtualHeight;
            Globals.GAME_WIDTH = Globals._virtualWidth;
            Globals.GAME_HEIGHT = Globals._virtualHeight;
            graphics.ApplyChanges();
            Globals.updateScreenScaleMatrix(GraphicsDevice);

            Debug.WriteLine($"{Globals._virtualHeight} {Globals._virtualWidth}");
        }

        public void OnResize(Object sender, EventArgs e)
        {
            if ((graphics.PreferredBackBufferWidth != graphics.GraphicsDevice.Viewport.Width) ||
                (graphics.PreferredBackBufferHeight != graphics.GraphicsDevice.Viewport.Height))
            {
                graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.Viewport.Width;
                graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.Viewport.Height;
                graphics.ApplyChanges();
            }
        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        protected override void Initialize()
        {
            KeyboardInput.Initialize(this, 500f, 20);
            Debug.WriteLine($"{Globals._virtualHeight} {Globals._virtualWidth}");
            Globals.Bounds = new(Globals._virtualWidth, Globals._virtualHeight);
            graphics.PreferredBackBufferWidth = Globals._virtualWidth;
            graphics.PreferredBackBufferHeight = Globals._virtualHeight;
            graphics.ApplyChanges();

            spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.updateScreenScaleMatrix(GraphicsDevice);

            EnemyManager.Init(Content);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new MenuState(this, GraphicsDevice, Content);
            _currentState.LoadContent();
            _nextState = null;
            UpdateWindowProperties();
        }

        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;
                _currentState.LoadContent();
                _nextState = null;
                UpdateWindowProperties();
            }
            _currentState.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                graphics.ToggleFullScreen();
            }

            base.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            GraphicsDevice.Viewport = Globals._viewport;
            _currentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }

        private void UpdateWindowProperties()
        {
            switch (_currentState.GetStateType())
            {
                case GameStateType.Menu:
                    Window.AllowUserResizing = true;
                    break;

                case GameStateType.Play:
                    Window.AllowUserResizing = true;
                    break;

                case GameStateType.HighScore:
                    Window.AllowUserResizing = true;
                    break;

                case GameStateType.GameOver:
                    Window.AllowUserResizing = true;
                    break;
                    // Add other cases as needed
            }
        }
    }
}