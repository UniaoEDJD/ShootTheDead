using ShootTheDead.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootTheDead.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace ShootTheDead.States
{
    public class HighscoresState : State

    {
        private List<Button> components;
        private ScoreManager scoreManager;
        private SpriteFont font;
        private Texture2D border;

        public HighscoresState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            scoreManager = ScoreManager.Load();
            var buttonTexture = _content.Load<Texture2D>("Button");
            var buttonFont = _content.Load<SpriteFont>("Font");

            var mainMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(80, Globals.GAME_HEIGHT - 150),
                Text = "Back To Main Menu",
            };

            mainMenuButton.Click += Button_MainMenu_Click;
            components = new List<Button>()
            {
                mainMenuButton,
            };
        }

        public override void LoadContent()
        {
            font = _content.Load<SpriteFont>("Font");
            border = _content.Load<Texture2D>("solid");
        }

        private void Button_MainMenu_Click(object sender, EventArgs args)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Button_MainMenu_Click(this, new EventArgs());

            foreach (var component in components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        void DrawBorder(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(border, new Rectangle(0, 0, (int)(1920 * Globals.scaleX), (int)(50 * Globals.scaleY)), Color.Purple);
            spriteBatch.Draw(border, new Vector2(0, 1030 * Globals.scaleY), new Rectangle(0, 0, (int)(1920 * Globals.scaleX), (int)(50 * Globals.scaleY)), Color.Purple);
            spriteBatch.Draw(border, new Rectangle(0, 0, (int)(50 * Globals.scaleX), (int)(1080 * Globals.scaleY)), Color.Purple);
            spriteBatch.Draw(border, new Vector2(1870 * Globals.scaleX, 0), new Rectangle(0, 0, (int)(50 * Globals.scaleX), (int)(1080 * Globals.scaleY)), Color.Purple);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: Globals._screenScaleMatrix);
            

            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);
            DrawBorder(spriteBatch);


            var highScores = scoreManager.HighScores;
            Vector2 position = new Vector2(400, 100); // Posição inicial para desenhar os scores

            for (int i = 0; i < highScores.Count; i++)
            {
                var score = highScores[i];
                string scoreText = $"{i + 1}. {score.playerName}: {score.playerScore}";

                spriteBatch.DrawString(font, "Score: " + scoreText, position, Color.White);

                // Incrementar a posição para o próximo score
                position.Y += 70; // Ajuste 20 ou outro valor para espaçamento entre scores
            }
             spriteBatch.End();
        }

        public override GameStateType GetStateType()
        {
            return GameStateType.HighScore;
        }
    }
}

