using ShootTheDead.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootTheDead.Control;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;

namespace ShootTheDead.States
{
    public class MenuState : State
    {
        private List<Control.Component> components;

        private Texture2D menuBackGroundTexture;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Button");
            var buttonFont = _content.Load<SpriteFont>("Font");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "New Game",
            };

            newGameButton.Click += Button_NewGame_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 250),
                Text = "Quit Game",
            };

            quitGameButton.Click += Button_Quit_Clicked;

            components = new List<Control.Component>()
            {
                newGameButton,
                quitGameButton,
            };
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
            foreach (var component in components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(menuBackGroundTexture, new Vector2(0, 0), Color.White);

            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

        }
    }
}
