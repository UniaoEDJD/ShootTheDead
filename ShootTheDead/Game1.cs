namespace ShootTheDead;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public static int  ScreenHeight;
    public static int ScreenWidth;
    private List<Component> _components;
    private Player _player;
    Texture2D background;
    private State _nextState;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    public void ChangeState(State state)
    {
        _nextState = state;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        ScreenHeight = _graphics.PreferredBackBufferHeight;

        ScreenWidth = _graphics.PreferredBackBufferWidth;



        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _components = new List<Component>();
         background = Content.Load<Texture2D>("background");
        _player = new Player(new Vector2(100, 100));
        _player.LoadContent(Content);


        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        // if (_nextState != null)
        // {
        //      _currentState = _nextState;
        //      _nextState = null;
        //  }

        _player.Update(gameTime);



        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();

        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Draw(background, Vector2.Zero, Color.White);

        _player.Draw(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
