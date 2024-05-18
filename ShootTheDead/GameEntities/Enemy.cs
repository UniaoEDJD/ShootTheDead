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

        public Enemy(Vector2 pos, Texture2D tex) : base(pos, tex)
        {
            Speed = 100;
            HP = 2;
        }
        
        public void TakeDamage(int dmg)
        {
            HP -= dmg;
        }

        public void Update(Player player)
        {
            var toPlayer = player.sPosition - sPosition;
            Rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);

            if (toPlayer.Length() > 4)
            {
                var dir = Vector2.Normalize(toPlayer);
                sPosition += dir * Speed * Globals.deltaTime;
            }
        }
    }
}
