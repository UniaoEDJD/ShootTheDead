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
            sTexture = tex;
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            frameIndex = 0;
            TimeElapsed = 0;
        }

        public virtual void Initialize()
        {
            // Initialize other properties if needed
        }

        protected void AddAnimation(int frameCount)
        {
            int frameWidth = sTexture.Width / frameCount;
            int frameHeight = sTexture.Height;
            sRectangles = new Rectangle[frameCount];

            for (int i = 0; i < frameCount; i++)
            {
                sRectangles[i] = new Rectangle(i * frameWidth, 0, frameWidth, frameHeight);
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
                Color.White,
                Rotation,
                origin,
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}
