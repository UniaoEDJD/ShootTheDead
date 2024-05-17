using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShootTheDead
{
    public class Bullet : Sprite
    {
        public Vector2 Direction { get; private set; }
        public float Speed { get; set; } = 1000f; // Velocidade da bala

        public Bullet(Vector2 position, Vector2 direction, Texture2D texture)
            : base(position, texture)
        {
            Direction = direction;
            Direction.Normalize(); // Normaliza a direção para garantir um movimento consistente

            // Adiciona animação com 1 frame (ou mais se necessário)
            AddAnimation(1);
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            sPosition += Direction * Speed * deltaTime; // Atualiza a posição da bala
            
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                sTexture,
                sPosition,
                sRectangles[frameIndex],
                Color.White,
                0f,
                new Vector2(sRectangles[frameIndex].Width / 2, sRectangles[frameIndex].Height / 2),
                1.0f,
                SpriteEffects.None,
                0f
            );
        }
    }
}
