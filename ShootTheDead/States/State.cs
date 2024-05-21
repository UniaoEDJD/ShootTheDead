namespace ShootTheDead.States
{
    public abstract class State
    {
        public enum GameStateType
        {
            Menu,
            Play,
            HighScore,
            Pause,
            // Add other states as needed
        }

        #region Fields
        protected ContentManager _content;

        protected GraphicsDevice _graphicsDevice;

        protected Game1 _game;
        #endregion

        #region Methods
        public abstract void LoadContent();

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        
        public abstract void PostUpdate(GameTime gameTime);
        public abstract GameStateType GetStateType();

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _game = game;

            _graphicsDevice = graphicsDevice;

            _content = content;
        }
            
        public abstract void Update(GameTime gameTime);


        #endregion
    }
}
