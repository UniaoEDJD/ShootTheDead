using Microsoft.Xna.Framework.Graphics;
using ShootTheDead.GameEntities;
using ShootTheDead.Main;
using ShootTheDead.Managers;
using ShootTheDead.States;
using System.Diagnostics;

namespace ShootTheDead
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Point _oldMousePosition;
        private float _mouseSpeed = 0.05f;
        private Vector2 _cameraTarget = Vector2.Zero;
        Texture2D[] runningTextures;
        int counter;
        int activeframe;
        private bool _isResizing;
        MapManager mapManager;
        Player player;
        private static Rectangle[] sRectangles;
        Enemy enemy;
        ScoreManager scoreManager;
        Score score;
        SpriteFont font;
        UI ui;
        private State _currentState;
        private State _nextState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnClientSizeChanged;
            
        }

        private void OnClientSizeChanged(object sender, EventArgs e)
        {
            if (!_isResizing && Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0)
            {
                _isResizing = true;
                Globals.updateScreenScaleMatrix(GraphicsDevice);
                _isResizing = false;
            }
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
            Globals.Bounds = new(Globals.GAME_WIDTH, Globals.GAME_HEIGHT);
            mapManager = new MapManager();
            Texture2D text = (Content.Load<Texture2D>("background"));
            graphics.PreferredBackBufferWidth = Globals.GAME_WIDTH;
            graphics.PreferredBackBufferHeight = Globals.GAME_HEIGHT;
            graphics.ApplyChanges();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mapManager.Initialize();
            Services.AddService(spriteBatch);
            Globals.updateScreenScaleMatrix(GraphicsDevice);
            ui = new UI(Content);
            player = new Player(new Vector2(300, 300), text);
            EnemyManager.Init(Content);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new MenuState(this, GraphicsDevice, Content);
            _currentState.LoadContent();
            _nextState = null;
        }

        protected override void Update(GameTime gameTime)
        { 
            if(_nextState != null)
            {
                _currentState = _nextState;
                _currentState.LoadContent();

                _nextState = null;
            }
            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {   
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.White);
            _currentState.Draw(gameTime, spriteBatch);  
            spriteBatch.End();
            base.Draw(gameTime);
        }



    }
}
