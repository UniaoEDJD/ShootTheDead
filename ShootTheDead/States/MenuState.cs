using ShootTheDead.Control;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace ShootTheDead.States
{
    public class MenuState : State
    {
        private List<Button> components;

        private Texture2D menuBackGroundTexture;
        private TextBox textBox;
        private SpriteFont font;
        private Rectangle viewport;
        ScoreManager scoreManager;


        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Button");
            var buttonFont = _content.Load<SpriteFont>("Font");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals._virtualWidth / 2 - 20, Globals._virtualHeight/2),
                Text = "New Game",
            };

            newGameButton.Click += Button_NewGame_Click;

            var HighScoreButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals._virtualWidth / 2 - 20, Globals._virtualHeight / 2 + 50),
                Text = "HighScore",
            };

            HighScoreButton.Click += Button_HighScore_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals._virtualWidth / 2 - 20, Globals._virtualHeight / 2 + 100),
                Text = "Quit Game",
            };

            quitGameButton.Click += Button_Quit_Clicked;

            components = new List<Button>()
            {
                newGameButton,
                HighScoreButton,
                quitGameButton,
            };

        }
        
        public override GameStateType GetStateType()
        {
            return GameStateType.Menu;
        }


        public override void LoadContent()
        {
            var font = _content.Load<SpriteFont>("Font");
            ScoreManager.Load();
            menuBackGroundTexture = _content.Load<Texture2D>("Background");

            viewport = new Rectangle(870, 100, 400, 200);
            textBox = new TextBox(viewport, 150, "Insert your nickname!",
            _graphicsDevice, font, Color.LightGray, Color.DarkGreen, 30);
        }

        private void Button_NewGame_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void Button_HighScore_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new HighscoresState(_game, _graphicsDevice, _content));
        }

        private void Button_Quit_Clicked(object sender, EventArgs args)
        {
            _game.Exit();
        }

        public override void Update(GameTime gameTime)
        {
            components[0].Position = new Vector2(Globals._virtualWidth / 2 - 100, Globals._virtualHeight / 2);
            components[1].Position = new Vector2(Globals._virtualWidth / 2 - 100, Globals._virtualHeight / 2 + 50);
            components[2].Position = new Vector2(Globals._virtualWidth / 2 - 100, Globals._virtualHeight / 2 + 100);
            foreach (var component in components)
                component.Update(gameTime);
            KeyboardInput.Update();
            float margin = 3;
            textBox.Area = new Rectangle((int)(viewport.X + margin), viewport.Y, (int)(viewport.Width - margin), viewport.Height);
            textBox.Renderer.Color = Color.White;
            textBox.Cursor.Selection = new Color(Color.Purple, .4f);
            float lerpAmount = (float)(gameTime.TotalGameTime.TotalMilliseconds % 500f / 500f);
            textBox.Cursor.Color = Color.Lerp(Color.DarkGray, Color.LightGray, lerpAmount);
            Globals.player = textBox.Text.String;
            Debug.WriteLine($" {Globals.player}");
            textBox.Active = true;
            textBox.Update();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float scaleX = (float)Globals._virtualWidth / menuBackGroundTexture.Width;
            float scaleY = (float)Globals._virtualHeight / menuBackGroundTexture.Height;

            spriteBatch.Begin();
            // Calculate scaling facto

            // Draw background texture with scaling
            spriteBatch.Draw(menuBackGroundTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, new Vector2(scaleX, scaleY), SpriteEffects.None, 0f);
            textBox.Draw(spriteBatch);
            foreach (var component in components)
            { 
               component.Draw(gameTime, spriteBatch);  
            }
                
            spriteBatch.End();

        }
    }
}
