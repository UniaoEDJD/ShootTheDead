using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShootTheDead.Main;

namespace ShootTheDead
{
    public class Player : Sprite
    {
        public float rotation;  // Adiciona a variável para armazenar a rotação
        protected float cooldown;
        protected float cooldownLeft;
        public static float TotalSeconds { get; set; }
        public bool Reloading { get; protected set; }
        private float Speed = 300;
        public Rectangle playerRect;
        private Map map = new Map();
        private bool isMoving;
        public List<Bullet> Bullets { get; private set; } = new List<Bullet>();
        private Texture2D bulletTexture;
        bool isShooting = false;


        public Player(Vector2 position, Texture2D tex) : base(position, tex)
        {
            framesPerSecond = 10;

            playerRect = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        public void LoadContent(ContentManager content)
        {
            sTexture = content.Load<Texture2D>("survivor-move_handgun");
            bulletTexture = content.Load<Texture2D>("bullet");
            AddAnimation(20);
            cooldown = 1;
            cooldownLeft = cooldown;
        }



        public void Shoot()
        {
                Vector2 bulletPosition = sPosition;
                Vector2 bulletDirection = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
                Bullet newBullet = new Bullet(bulletPosition, bulletDirection, bulletTexture);
                Bullets.Add(newBullet);
                
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput(Keyboard.GetState(), gameTime);
            // Obtém a posição do mouse
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

            // Calcula a direção entre o jogador e o mouse
            Vector2 direction = mousePosition - sPosition;
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            cooldownLeft -= 5 * delta;
            // Calcula a rotação em radianos
            rotation = (float)Math.Atan2(direction.Y, direction.X);
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) // Pressione a barra de espaço para atirar
            {
                
                if (cooldownLeft <= 0)
                {
                    Shoot();
                    cooldownLeft = cooldown;
                }
                
                
            }

            foreach (var bullet in Bullets)
            {
                bullet.Update(gameTime);
            }

            if (isMoving)
            {
                // Atualiza a animação se o jogador estiver se movendo
                base.Update(gameTime);
            }
            
        }

        public void HandleInput(KeyboardState keyState, GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            isMoving = false;

            if (keyState.IsKeyDown(Keys.W))
            {
                sPosition.Y -= Speed * deltaTime;
                isMoving = true;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                sPosition.X -= Speed * deltaTime;
                isMoving = true;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                sPosition.Y += Speed * deltaTime;
                isMoving = true;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                sPosition.X += Speed * deltaTime;
                isMoving = true;
            }
            playerRect.X = (int)sPosition.X;
            playerRect.Y = (int)sPosition.Y;
        }

        public void SwapWeapon()
        {
            //Gun = (Gun == _weapon1) ? _weapon2 : _weapon1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Desenha o jogador com rotação
            spriteBatch.Draw(
                sTexture,
                sPosition,
                sRectangles[frameIndex],
                Color.White,
                rotation,  // Aplica a rotação
                new Vector2(sRectangles[frameIndex].Width / 2, sRectangles[frameIndex].Height / 2),  // Origem da rotação
                1.0f,
                SpriteEffects.None,
                0f
            );
            foreach (var bullet in Bullets)
            {
                bullet.Draw(spriteBatch);
            }
        }
    }
}