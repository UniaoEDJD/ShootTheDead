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
        MapManager mapManager;
        Player player;
        ScoreManager scoreManager;
        SpriteFont font;
        UI ui;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
           : base(game, graphicsDevice, content)
        {
            mapManager = new MapManager();
            
        }

        public override GameStateType GetStateType()
        {
            return GameStateType.Play;
        }

        public override void LoadContent()
        {
            // Create a new GraphicsDeviceManager
            
            ui = new UI(_content);
            mapManager.Initialize();
            mapManager.LoadContent(_content);
            scoreManager = ScoreManager.Load();
            font = _content.Load<SpriteFont>("Font");
            Texture2D text = (_content.Load<Texture2D>("background"));
            player = new Player(new Vector2(300, 300), text);
            player.LoadContent(_content);
        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: Globals._screenScaleMatrix); // Use a transformação da câmera, se necessário


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
            KeyboardState state = Keyboard.GetState();

            Globals.Update(gameTime);
            ui.Update(player.Health, 5);
            var prevPos = player.sPosition;
            player.Update(gameTime);
            EnemyManager.Update(player, gameTime);
            mapManager.Update(player, prevPos);
            if (player.isDead)
            {
                scoreManager.AddScore(new Main.Score()
                {
                    playerName = Globals.player,
                    playerScore = player.score
                });

                ScoreManager.SaveScore(scoreManager);
                player.isDead = false;
                _game.ChangeState(new HighscoresState(_game, _graphicsDevice, _content));
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {
           
        }
    }
}
