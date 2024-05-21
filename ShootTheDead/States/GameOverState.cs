using ShootTheDead.Control;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShootTheDead.States;
using System.Diagnostics;

namespace ShootTheDead.States
{
    public class GameOverState : State
    {
        private List<Button> components;
        private SpriteFont font;
        private Texture2D buttonTexture;
        private Texture2D backgroundTexture;

        public GameOverState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            LoadContent();
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Calcular a posição centralizada horizontalmente
            float centerX = Globals._virtualWidth / 2;

            // Inicializar os botões com posição centralizada horizontalmente
            var restartButton = new Button(buttonTexture, font)
            {
                Position = new Vector2(centerX - buttonTexture.Width / 2, Globals._virtualHeight / 2),
                Text = "Restart",
            };
            restartButton.Click += RestartButton_Click;

            var exitButton = new Button(buttonTexture, font)
            {
                Position = new Vector2(centerX - buttonTexture.Width / 2, Globals._virtualHeight / 2 + 50),
                Text = "Exit",
            };
            exitButton.Click += ExitButton_Click;

            components = new List<Button>()
            {
                restartButton,
                exitButton,
            };
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float scaleX = (float)Globals._virtualWidth / backgroundTexture.Width;
            float scaleY = (float)Globals._virtualHeight / backgroundTexture.Height;

            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            spriteBatch.Draw(backgroundTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, new Vector2(scaleX, scaleY), SpriteEffects.None, 0f);
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(font, "Game Over", new Vector2(800, 100), Color.Red);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            components[0].Position = new Vector2(Globals._virtualWidth / 2 - 100, Globals._virtualHeight / 2);
            components[1].Position = new Vector2(Globals._virtualWidth / 2 - 100, Globals._virtualHeight / 2 + 50);
            foreach (var component in components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // Remove sprites if they're not needed
        }

        public override void LoadContent()
        {
            backgroundTexture = _content.Load<Texture2D>("Fundo");
            font = _content.Load<SpriteFont>("Font");
            buttonTexture = _content.Load<Texture2D>("Button");
        }

        public override GameStateType GetStateType()
        {
            return GameStateType.GameOver;
        }
    }
}
