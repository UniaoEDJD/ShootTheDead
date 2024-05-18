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
        private State _currentState;
        private State _nextState;
        private bool _isResizing;
        MapManager mapManager;
        Player player;
        private static Rectangle[] sRectangles;
        Enemy enemy;
        

        public void ChangeState(State state)
        {
            _nextState = state;
        }



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
            Globals.updateScreenScaleMatrix(GraphicsDevice);
       
            
            player = new Player(new Vector2(300, 300), text);
            EnemyManager.Init(Content);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mapManager.LoadContent(Content);
            // _currentState = new MenuState(this, graphics.GraphicsDevice, Content);
            
            
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
            Globals.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var prevPos = player.sPosition;
            // Atualiza o player
            player.Update(gameTime);
            EnemyManager.Update(player);
            mapManager.Update(player, prevPos);



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: Globals._screenScaleMatrix); // Use a transformação da câmera, se necessário
            GraphicsDevice.Clear(Color.Black);

            GraphicsDevice.Viewport = Globals._viewport;

            // Desenha o background e o mapa
            mapManager.Draw(spriteBatch);
            

            // Desenha o jogador
            player.Draw(spriteBatch);
            EnemyManager.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }



    }
}
