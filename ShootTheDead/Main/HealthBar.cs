

namespace ShootTheDead.Main
{
    public class HealthBar
    {
        public int boarder;

        public _2D bar, barbkg;

        public Color color;

        public HealthBar(Vector2 DIMS, int BOARDER, Color COLOR, ContentManager Content)
        {
            boarder = BOARDER;
            color = COLOR;

            bar = new _2D("solid", new Vector2(0, 0), new Vector2(DIMS.X - boarder * 2, DIMS.Y - boarder * 2), Content);
            barbkg = new _2D("shade", new Vector2(0, 0), new Vector2(DIMS.X, DIMS.Y), Content);
        }

        public virtual void Update(float CURRENT, float MAX)
        {
            bar.dims = new Vector2(CURRENT / MAX * (barbkg.dims.X - boarder * 2), bar.dims.Y);
        }

        public virtual void Draw(SpriteBatch _spriteBatch, Vector2 OFFSET)
        {
            barbkg.Draw(_spriteBatch, OFFSET, new Vector2(0,0), Color.Black);
            bar.Draw(_spriteBatch, OFFSET + new Vector2(boarder, boarder), new Vector2(0, 0), color);
        }


    }
}
