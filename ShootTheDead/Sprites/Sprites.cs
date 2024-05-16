using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootTheDead
{
    public abstract class Sprite
    {
        public Vector2 sPosition;
        public Texture2D sTexture;
        public Rectangle sRectangles;
        private double TimeElapsed;
        private double timeToUpdate;
        public int frameIndex;
        protected Vector2 sDirection = Vector2.Zero;

        public Sprite(Vector2 position)
        {
            sPosition = position;
        }

        public void AddAnimation(int frames)
        {
            int width = sTexture.Width;
            sRectangles = new Rectangle();
            for (int i = 0; i < frames; i++)
 
                sRectangles = new Rectangle(0, 0, width, sTexture.Height);

        }

        public virtual void Update(GameTime gameTime)
        {
            TimeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sTexture,
            sPosition,
            sRectangles,
            Color.White);
        }

    }
}
