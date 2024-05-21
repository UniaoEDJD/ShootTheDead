using ShootTheDead.Control;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;

namespace ShootTheDead.States
{
    public class MenuState : State
    {
        private List<Button> components;

        private Texture2D menuBackGroundTexture;


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

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals._virtualWidth / 2 - 20, Globals._virtualHeight / 2 + 50),
                Text = "Quit Game",
            };

            quitGameButton.Click += Button_Quit_Clicked;

            components = new List<Button>()
            {
                newGameButton,
                quitGameButton,
            };

        }

        public override GameStateType GetStateType()
        {
            return GameStateType.Menu;
        }


        public override void LoadContent()
        {  
            menuBackGroundTexture = _content.Load<Texture2D>("Background");
        }

        private void Button_NewGame_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void Button_Quit_Clicked(object sender, EventArgs args)
        {
            _game.Exit();
        }

        public override void Update(GameTime gameTime)
        {
            components[0].Position.X = Globals._virtualWidth / 2 - 20;
            components[0].Position.Y = Globals._virtualHeight / 2;
            components[1].Position.X = Globals._virtualWidth / 2 - 20;
            components[1].Position.Y = Globals._virtualHeight / 2 + 50;
            foreach (var component in components)
                component.Update(gameTime);
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

            foreach (var component in components)
                { component.Draw(gameTime, spriteBatch); 
                  
                }
                
            spriteBatch.End();

        }
    }
}
