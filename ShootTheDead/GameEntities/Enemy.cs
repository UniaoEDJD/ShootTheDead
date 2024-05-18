using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShootTheDead.Sprites;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace ShootTheDead.GameEntities
{
    public class Enemy : AnimatedSprite
    {
        public int HP { get; private set; }
        private Rectangle rect {get; set; }
        public Rectangle collider;
        public bool isKilling { get; set; } = false;
        public Enemy(Vector2 pos, Texture2D tex) : base(pos, tex)
        {
            rect = new Rectangle(0, 0, 288, tex.Height);
            Speed = 100;
            HP = 2;
            collider =  new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }
        
        public void TakeDamage(int dmg)
        {
            HP -= dmg;
        }

        public void Update(Player player)
        {
            var toPlayer = player.sPosition - sPosition;
            Rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);
            collider.X = (int)sPosition.X;
            collider.Y = (int)sPosition.Y;
            if (toPlayer.Length() > 4)
            {
                var dir = Vector2.Normalize(toPlayer);
                sPosition += dir * Speed * Globals.deltaTime;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            // Desenha o jogador com rota鈬o
            spriteBatch.Draw(
                sTexture,
                sPosition,
                rect,
                Color.White,
                Rotation,  // Aplica a rota鈬o
                new Vector2(rect.Width / 2, rect.Height / 2),  // Origem da rota鈬o
                1.0f,
                SpriteEffects.None,
                0f
            );

        }
    }
}
