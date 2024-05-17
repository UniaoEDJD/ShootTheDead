using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ShootTheDead.GameEntities
{
    public class Enemy : Sprite
    {
        public static float TotalSeconds { get; set; }
        public int HP { get; private set; }
        public float rotation;
        public int Speed { get; set; }
        public static Random RandomGenerator { get; set; } // Renomeado para evitar conflito de nome
        public static int Padding { get; set; }
        public static List<Enemy> Zombies { get; } = new List<Enemy>();
        public static Point Bounds { get; set; }

        public Enemy(Vector2 pos, Texture2D tex) : base(pos, tex)
        {
            Speed = 100;
            HP = 2;
            framesPerSecond = 10;
        }

        public void TakeDamage(int dmg)
        {
            HP -= dmg;
        }

        private static Vector2 RandomPosition()
        {
            float w = Bounds.X;
            float h = Bounds.Y;
            Vector2 pos = new();

            if (RandomGenerator.NextDouble() < w / (w + h))
            {
                pos.X = (int)(RandomGenerator.NextDouble() * w);
                pos.Y = (int)(RandomGenerator.NextDouble() < 0.5 ? -Padding : h + Padding);
            }
            else
            {
                pos.Y = (int)(RandomGenerator.NextDouble() * h);
                pos.X = (int)(RandomGenerator.NextDouble() < 0.5 ? -Padding : w + Padding);
            }

            return pos;
        }

        public void Update(Player player)
        {
            var toPlayer = player.sPosition - sPosition;
            rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);

            if (toPlayer.Length() > 4)
            {
                var dir = Vector2.Normalize(toPlayer);
                sPosition += dir * Speed * TotalSeconds;
            }
        }

        public void LoadContent(ContentManager content)
        {
            sTexture = content.Load<Texture2D>("skeleton-move_");
            AddAnimation(17);
            Initialize();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (sTexture != null && sRectangles != null && frameIndex >= 0 && frameIndex < sRectangles.Length)
            {
                spriteBatch.Draw(
                    sTexture,
                    sPosition,
                    sRectangles[frameIndex],
                    Color.White,
                    rotation,  // Aplica a rotação
                    new Vector2(sRectangles[frameIndex].Width / 2, sRectangles[frameIndex].Height / 2),  // Origem da rotação
                    1.0f,
                    SpriteEffects.None,
                    0f
                );
            }
            else
            {
                // Log de erro ou outra ação de tratamento
            }
        }


        public static void SpawnEnemy(Texture2D texture)
        {
            Vector2 spawnPosition = RandomPosition();
            Enemy newEnemy = new Enemy(spawnPosition, texture);
            Zombies.Add(newEnemy);
        }

        public static void UpdateEnemies(Player player)
        {
            foreach (var enemy in Zombies)
            {
                enemy.Update(player);
            }
        }

        public static void DrawEnemies(SpriteBatch spriteBatch)
        {
            foreach (var enemy in Zombies)
            {
                enemy.Draw(spriteBatch);
            }
        }
    }
}
