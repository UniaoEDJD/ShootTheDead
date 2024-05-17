using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ShootTheDead
{
    public abstract class Sprite
    {
        public Vector2 sPosition;
        public Texture2D sTexture;
        public Rectangle[] sRectangles;
        private double TimeElapsed;
        private double timeToUpdate;
        public int frameIndex;
        public float Rotation { get; set; }
        protected readonly Vector2 origin;

        public int framesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public Sprite(Vector2 pos, Texture2D tex)
        {
            sPosition = pos;
            sTexture = tex ?? throw new ArgumentNullException(nameof(tex), "Texture cannot be null.");
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            frameIndex = 0;
            TimeElapsed = 0;
        }

        public virtual void Initialize()
        {
            if (sTexture == null)
            {
                throw new ArgumentNullException(nameof(sTexture), "Texture cannot be null.");
            }
            // Initialize other properties if needed
        }

        public void AddAnimation(int frames)
        {
            if (sTexture == null)
            {
                throw new InvalidOperationException("Texture must be set before adding animation.");
            }

            int width = sTexture.Width / frames;
            sRectangles = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
            {
                sRectangles[i] = new Rectangle(i * width, 0, width, sTexture.Height);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (sRectangles == null || sRectangles.Length == 0)
            {
                throw new InvalidOperationException("Animation frames are not initialized.");
            }

            TimeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            if (TimeElapsed > timeToUpdate)
            {
                TimeElapsed -= timeToUpdate;

                if (frameIndex < sRectangles.Length - 1)
                {
                    frameIndex++;
                }
                else
                {
                    frameIndex = 0;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (sTexture == null || sRectangles == null || sRectangles.Length == 0)
            {
                throw new InvalidOperationException("Sprite cannot be drawn because it is not properly initialized.");
            }

            spriteBatch.Draw(sTexture,
                sPosition,
                sRectangles[frameIndex],
                Color.White,
                Rotation,
                origin,
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}
