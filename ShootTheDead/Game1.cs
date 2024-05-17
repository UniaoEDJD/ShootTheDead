using Microsoft.Xna.Framework.Graphics;
using ShootTheDead.GameEntities;
using ShootTheDead.Main;
using ShootTheDead.Managers;
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
        private int _oldMouseZoom;
        Texture2D[] runningTextures;
        int counter;
        int activeframe;
        private int _virtualWidth = 1920;
        private int _virtualHeight = 1080;
        private State _currentState;
        private State _nextState;
        public Matrix _screenScaleMatrix;
        private bool _isResizing;
        private Viewport _viewport;
        MapManager mapManager;
        Player player;
        Enemy enemy;
        
        private Texture2D _enemyTexture;

        public void ChangeState(State state)
        {
            _nextState = state;
        }



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Globals.Content = Content;

            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnClientSizeChanged;
            
        }

        private void OnClientSizeChanged(object sender, EventArgs e)
        {
            if (!_isResizing && Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0)
            {
                _isResizing = true;
                updateScreenScaleMatrix();
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

        protected override void Initialize()
        {
            mapManager = new MapManager();
            Texture2D text = (Content.Load<Texture2D>("background"));
            graphics.PreferredBackBufferWidth = Globals.GAME_WIDTH;
            graphics.PreferredBackBufferHeight = Globals.GAME_HEIGHT;
            graphics.ApplyChanges();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mapManager.Initialize();
            // Adiciona o serviço SpriteBatch ao serviço de gráficos do jogo
            Services.AddService(spriteBatch);
            updateScreenScaleMatrix();
            
            
            player = new Player(new Vector2(300, 300), text);

            Enemy.Bounds = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            Enemy.Padding= 50;
            Enemy.RandomGenerator = new Random();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mapManager.LoadContent();
            // _currentState = new MenuState(this, graphics.GraphicsDevice, Content);
            _enemyTexture = Content.Load<Texture2D>("skeleton-move_");
            Enemy.SpawnEnemy(_enemyTexture);
            player.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            // if (_nextState != null)
            // {
            //      _currentState = _nextState;
            //      _nextState = null;
            //  }
            
            // _currentState.Update(gameTime);
            //  _currentState.PostUpdate(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var prevPos = player.sPosition;
            // Atualiza o player
            player.Update(gameTime);
            mapManager.Update(player, prevPos);

            Enemy.TotalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Enemy.UpdateEnemies(player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: _screenScaleMatrix); // Use a transformação da câmera, se necessário
            GraphicsDevice.Clear(Color.Black);

            GraphicsDevice.Viewport = _viewport;

            // Desenha o background e o mapa
            mapManager.Draw(spriteBatch);
            

            // Desenha o jogador
            player.Draw(spriteBatch);
            Enemy.DrawEnemies(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        private void updateScreenScaleMatrix()
        {
            float screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;
            float screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;

            if (screenWidth / _virtualWidth > screenHeight / _virtualHeight)
            {
                float aspect = screenHeight / Globals.GAME_HEIGHT;
                _virtualWidth = (int)(aspect * Globals.GAME_WIDTH);
                _virtualHeight = (int)screenHeight;
            }
            else
            {
                float aspect = screenWidth / Globals.GAME_WIDTH;
                _virtualWidth = (int)screenWidth;
                _virtualHeight = (int)(aspect * Globals.GAME_HEIGHT);
            }

            _screenScaleMatrix = Matrix.CreateScale(_virtualWidth / (float)Globals.GAME_WIDTH);

            _viewport = new Viewport
            {
                X = (int)(screenWidth / 2 - _virtualWidth / 2),
                Y = (int)(screenHeight / 2 - _virtualHeight / 2),
                Width = _virtualWidth,
                Height = _virtualHeight,
                MinDepth = 0,
                MaxDepth = 1
            };
        }
    }
}
