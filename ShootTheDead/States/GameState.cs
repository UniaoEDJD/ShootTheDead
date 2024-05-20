using ShootTheDead.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using ShootTheDead.Managers;
using ShootTheDead.GameEntities;
using ShootTheDead.Main;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Graphics;
using ShootTheDead.States;
using System.Diagnostics;
using ShootTheDead.Sprites;
using ShootTheDead;

namespace ShootTheDead.States
{
    public class GameState : State
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Point _oldMousePosition;
        private float _mouseSpeed = 0.05f;
        private Vector2 _cameraTarget = Vector2.Zero;
        Texture2D[] runningTextures;
        int counter;
        int activeframe;
        private bool _isResizing;
        MapManager mapManager;
        Player player;
        private static Rectangle[] sRectangles;
        Enemy enemy;
        ScoreManager scoreManager;
        Score score;
        SpriteFont font;
        UI ui;
        Game1 Game;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
           : base(game, graphicsDevice, content)
        {

        }

        public override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mapManager.LoadContent(_content);
            scoreManager = ScoreManager.Load();
            font = _content.Load<SpriteFont>("Font");
            player.LoadContent(_content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: Globals._screenScaleMatrix); // Use a transformação da câmera, se necessário
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.Viewport = Globals._viewport;
            // Desenha o background e o mapa
            mapManager.Draw(spriteBatch);
            // Desenha o jogador
            player.Draw(spriteBatch);
            EnemyManager.Draw(spriteBatch);
            ui.Draw(spriteBatch, player, font);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                scoreManager.AddScore(new Main.Score()
                {
                    playerName = "Player",
                    playerScore = player.score
                });

                ScoreManager.SaveScore(scoreManager);
                Exit();
            }
            ui.Update(player.Health, 5);
            var prevPos = player.sPosition;
            // Atualiza o player
            player.Update(gameTime);
            EnemyManager.Update(player, gameTime);
            mapManager.Update(player, prevPos);
            if (player.isDead)
            {
                scoreManager.AddScore(new Main.Score()
                {
                    playerName = "Player",
                    playerScore = player.score
                });
                ScoreManager.SaveScore(scoreManager);
                Exit();
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
