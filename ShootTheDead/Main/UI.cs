namespace ShootTheDead.Main
{
    public class UI
    {
        public SpriteFont font;

        public HealthBar heatlh;

        public UI(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Font");

            heatlh = new HealthBar(new Vector2(200, 20), 2, Color.Red, Content);
        }


        public void Update(float health, float maxHealth)
        {
            heatlh.Update(health, maxHealth);
        }

        public void Draw(SpriteBatch _spriteBatch, Player player, SpriteFont Font)
        {
            string tmpstring = "zombies killed: " + player.score;
            Vector2 strgDist = Font.MeasureString(tmpstring);
            _spriteBatch.DrawString(Font, tmpstring, new Vector2(Globals._virtualWidth - strgDist.X - 10, 10), Color.White, 0, new Vector2(0,0), 0.75f, SpriteEffects.None, 0);

            heatlh.Draw(_spriteBatch, new Vector2(10, 10));
        }
    }

}
