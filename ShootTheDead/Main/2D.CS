namespace ShootTheDead.Main
{
    public class _2D
    {
        public Vector2 pos, dims;
        public float rot;
        public Texture2D text;

        public _2D(string _Path, Vector2 _pos, Vector2 _dims, ContentManager Content)
        {
            pos = _pos;
            dims = _dims;

            text = Content.Load<Texture2D>(_Path);
        }

        public virtual void Draw(SpriteBatch _spriteBatch, Vector2 OFFSET)
        {
            if (text != null)
            { _spriteBatch.Draw(text, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, Color.White, rot, new Vector2(text.Bounds.Width / 2, text.Bounds.Height / 2), new SpriteEffects(), 0f); }
        }

        public virtual void Draw(SpriteBatch _spriteBatch, Vector2 OFFSET, Vector2 ORIGIN, Color COLOR)
        {
            if (text != null)
            { _spriteBatch.Draw(text, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, COLOR, rot, ORIGIN, new SpriteEffects(), 0f); }
        }

        public virtual void Update(Vector2 OFFSET)
        {
        }
    }
}