using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        private State _currentState;
        private State _nextState;
        private Color color;
        private Song backgroundMusic;
       
        public static Game1 Instance { get; private set; }

        public Game1()
        {
            Debug.WriteLine("Game1 constructor");
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.IsFullScreen = false;
            Instance = this;
        }

        public void OnResize1()
        {
            graphics.PreferredBackBufferWidth = Globals.GAME_WIDTH;
            graphics.PreferredBackBufferHeight = Globals.GAME_HEIGHT;
            graphics.ApplyChanges();

            Globals.updateScreenScaleMatrix(GraphicsDevice);

        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        protected override void Initialize()
        {
            
            KeyboardInput.Initialize(this, 500f, 20);
            Debug.WriteLine($"{Globals.GAME_HEIGHT} {Globals.GAME_WIDTH}");
            Globals.Bounds = new(Globals.GAME_WIDTH, Globals.GAME_HEIGHT);
            graphics.PreferredBackBufferWidth = Globals.GAME_WIDTH;
            graphics.PreferredBackBufferHeight = Globals.GAME_HEIGHT;
            graphics.ApplyChanges();

            spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.updateScreenScaleMatrix(GraphicsDevice);

            EnemyManager.Init(Content);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            backgroundMusic = Content.Load<Song>("Sfx/creepymusic");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(backgroundMusic);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new MenuState(this, GraphicsDevice, Content);
            _currentState.LoadContent();
            _nextState = null;
            
        }

        protected override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);
            if (_nextState != null)
            {
                _currentState = _nextState;
                _currentState.LoadContent();
                _nextState = null;
                
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

            switch(_currentState.GetStateType())
            {
                case GameStateType.HighScore:
                    color = Color.Black;
                    break;
                default:
                    color = Color.White;
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(color);
            
            _currentState.Draw(gameTime, spriteBatch);
            GraphicsDevice.Viewport = Globals._viewport;
            base.Draw(gameTime);
        }
    }
}