namespace ShootTheDead.Sprites
{
    public class AnimatedSprite(Vector2 pos, Texture2D tex) : Sprite(pos, tex)
    {
        public int Speed { get; set; } = 300;
    }
}
