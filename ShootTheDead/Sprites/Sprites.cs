using static System.Net.Mime.MediaTypeNames;

namespace ShootTheDead
{
    public abstract class Sprite (Vector2 pos, Texture2D tex)
    {
        public Vector2 sPosition;
        public Texture2D sTexture;
        public Rectangle[] sRectangles;
        private double TimeElapsed;
        private double timeToUpdate;
        public int frameIndex;
        public float Rotation { get; set; }
        protected readonly Vector2 origin = new(tex.Width /2, tex.Height / 2);

        public int framesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }
        
        public virtual void Initialize()
        {
            sPosition = pos;
        }
        

        public void AddAnimation(int frames)
        {
            int width = sTexture.Width / frames;
            sRectangles = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
            {
                sRectangles[i] = new Rectangle(i * width, 0, width, sTexture.Height);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
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
            spriteBatch.Draw(sTexture,
            sPosition,
            sRectangles[frameIndex],
            Color.White, Rotation, new Vector2 (300, 300), 1f, SpriteEffects.None, 1);
        }

    }

}
