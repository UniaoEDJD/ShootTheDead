using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShootTheDead.Sprites;
using System;
using System.Collections.Generic;

namespace ShootTheDead.GameEntities
{
    public class Enemy : AnimatedSprite
    {
        public int HP { get; private set; }
        private Rectangle rect { get; set; }
        public Rectangle collider;
        public bool isKilling { get; set; } = false;
        private int frameIndex;
        private float timeElapsed;
        private float timeToUpdate;
        private List<Rectangle> sRectangles;

        public Enemy(Vector2 pos, Texture2D tex) : base(pos, tex)
        {
            rect = new Rectangle(0, 0, 288, tex.Height);
            Speed = 250;
            HP = 2;
            collider = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            sRectangles = new List<Rectangle>();
            frameIndex = 0;
            timeElapsed = 0;
            framesPerSecond = 10;
            AddAnimation(tex.Width / 17); // Assuming 4 frames in a row for the enemy texture
        }

        public int framesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public void AddAnimation(int frameWidth)
        {
            for (int i = 0; i < sTexture.Width; i += frameWidth)
            {
                sRectangles.Add(new Rectangle(i, 0, frameWidth, sTexture.Height));
            }
        }

        public void TakeDamage(int dmg)
        {
            HP -= dmg;
        }

        public void Update(Player player, GameTime gameTime)
        {
            // Update position and rotation
            var toPlayer = player.sPosition - sPosition;
            Rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);
            collider.X = (int)sPosition.X;
            collider.Y = (int)sPosition.Y;
            if (toPlayer.Length() > 4)
            {
                var dir = Vector2.Normalize(toPlayer);
                sPosition += dir * Speed * Globals.deltaTime;
            }

            // Update animation
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate)
            {
                frameIndex++;
                if (frameIndex >= sRectangles.Count)
                {
                    frameIndex = 0;
                }
                timeElapsed -= timeToUpdate;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                sTexture,
                sPosition,
                sRectangles[frameIndex],
                Color.White,
                Rotation,  // Aplica a rotação
                new Vector2(sRectangles[frameIndex].Width / 2, sRectangles[frameIndex].Height / 2),  // Origem da rotação
                1.0f,
                SpriteEffects.None,
                0f
            );
        }
    }
}
