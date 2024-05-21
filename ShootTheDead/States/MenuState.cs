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
        SpriteFont fonte;
        public int scale = 120;


        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Button");
            var buttonFont = _content.Load<SpriteFont>("Font");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.GAME_WIDTH / 2 - 100, Globals.GAME_HEIGHT / 2),
                Text = "New Game",
            };

            newGameButton.Click += Button_NewGame_Click;

            var HighScoreButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.GAME_WIDTH / 2 - 100, Globals.GAME_HEIGHT / 2 + 50),
                Text = "HighScore",
            };

            HighScoreButton.Click += Button_HighScore_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.GAME_WIDTH / 2 - 100, Globals.GAME_HEIGHT / 2 + 100),
                Text = "Quit Game",
            };

            var resUpButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.GAME_WIDTH / 2 + 190, Globals.GAME_HEIGHT / 2 + 200),
                Text = "Res +",
            };

            resUpButton.Click += Button_ResUp_Click;

            var resDownButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.GAME_WIDTH / 2 - 384, Globals.GAME_HEIGHT / 2 + 200),
                Text = "Res -",
            };

            resDownButton.Click += Button_ResDown_Click;



            quitGameButton.Click += Button_Quit_Clicked;

            components = new List<Button>()
            {
                newGameButton,
                HighScoreButton,
                quitGameButton,
                resUpButton,
                resDownButton,
            };

        }

        private void Button_ResDown_Click(object sender, EventArgs e)
        {
            if (Globals.GAME_WIDTH > 1920 && Globals.GAME_HEIGHT > 1080)
            {
                Globals.GAME_WIDTH = 16 * (Globals.GAME_WIDTH / 16 - 40);
                Globals.GAME_HEIGHT = 9 * (Globals.GAME_HEIGHT / 9 - 40);
                if (Globals.GAME_WIDTH > 2560 && Globals.GAME_HEIGHT > 1440)
                {
                    Globals.GAME_WIDTH = 16 * (Globals.GAME_WIDTH / 16 - 80);
                    Globals.GAME_HEIGHT = 9 * (Globals.GAME_HEIGHT / 9 - 80);
                }
            }
            else
            {
                Globals.GAME_WIDTH = 16 * (Globals.GAME_WIDTH / 16 - 20);
                Globals.GAME_HEIGHT = 9 * (Globals.GAME_HEIGHT / 9 - 20);
                Debug.WriteLine($"{Globals.GAME_HEIGHT} {Globals.GAME_WIDTH}");
                if (Globals.GAME_WIDTH < 1280 && Globals.GAME_HEIGHT < 720)
                {
                    Globals.GAME_WIDTH = 1280;
                    Globals.GAME_HEIGHT = 720;
                }
            }
            Game1.Instance.OnResize1();
            
        }

        private void Button_ResUp_Click(object sender, EventArgs e)
        {
            if (Globals.GAME_WIDTH >= 1920 && Globals.GAME_HEIGHT >= 1080)
            {
                Globals.GAME_WIDTH = 16 * (Globals.GAME_WIDTH / 16 + 40);
                Globals.GAME_HEIGHT = 9 * (Globals.GAME_HEIGHT / 9 + 40);
                if (Globals.GAME_WIDTH > 2560 && Globals.GAME_HEIGHT > 1440)
                {
                    Globals.GAME_WIDTH = 3840;
                    Globals.GAME_HEIGHT = 2160;
                }

            }
            else
            {Globals.GAME_WIDTH = 16 * (Globals.GAME_WIDTH / 16 + 20);
             Globals.GAME_HEIGHT = 9 * (Globals.GAME_HEIGHT / 9 + 20);
                if (Globals.GAME_WIDTH > 2560 && Globals.GAME_HEIGHT > 1440)
                {
                    Globals.GAME_WIDTH = 3840;
                    Globals.GAME_HEIGHT = 2160;
                }
            }
            Game1.Instance.OnResize1();

            
        }

        public override GameStateType GetStateType()
        {
            return GameStateType.Menu;
        }


        public override void LoadContent()
        {
            var font = _content.Load<SpriteFont>("Font");
            fonte = _content.Load<SpriteFont>("Font");
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
            components[0].Position = new Vector2(Globals.GAME_WIDTH / 2 - 130, Globals.GAME_HEIGHT / 2);
            components[1].Position = new Vector2(Globals.GAME_WIDTH / 2 - 130, Globals.GAME_HEIGHT / 2 + 50);
            components[2].Position = new Vector2(Globals.GAME_WIDTH / 2 - 130, Globals.GAME_HEIGHT / 2 + 100);
            components[3].Position = new Vector2(Globals.GAME_WIDTH / 2 + 125, Globals.GAME_HEIGHT / 2 + 200);
            components[4].Position = new Vector2(Globals.GAME_WIDTH / 2 - 379, Globals.GAME_HEIGHT / 2 + 200);
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
            textBox.Active = true;
            textBox.Update();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float scaleX = (float)Globals.GAME_WIDTH / menuBackGroundTexture.Width;
            float scaleY = (float)Globals.GAME_HEIGHT / menuBackGroundTexture.Height;

            spriteBatch.Begin(transformMatrix: Globals._screenScaleMatrix);
            // Calculate scaling facto

            // Draw background texture with scaling
            spriteBatch.Draw(menuBackGroundTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, new Vector2(scaleX, scaleY), SpriteEffects.None, 0f);
            spriteBatch.DrawString(fonte, $"{Globals.GAME_WIDTH} x {Globals.GAME_HEIGHT}", new Vector2(Globals.GAME_WIDTH / 2 - 139, Globals.GAME_HEIGHT / 2 + 200), Color.White);
            textBox.Draw(spriteBatch);
            foreach (var component in components)
            { 
               component.Draw(gameTime, spriteBatch);  
            }
            spriteBatch.End();

        }
    }
}
