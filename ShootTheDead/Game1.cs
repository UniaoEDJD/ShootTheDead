namespace ShootTheDead;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public static int  ScreenHeight;
    public static int ScreenWidth;
    private Camera _camera;
    private List<Component> _components;
    private Player _player;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
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

        _camera = new Camera();
        _player = new Player(Content.Load<Texture2D>("survivor-reload_handgun_0"));
        _components = new List<Component>()
        {
            new Sprite(Content.Load<Texture2D>("background")),
            _player
        };

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var component in _components)
            component.Update(gameTime);
        
        _camera.Follow(_player);
        

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(transformMatrix: _camera.Transform);

        foreach (var component in _components)
            component.Draw(gameTime, _spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
