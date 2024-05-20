using Microsoft.Xna.Framework.Audio;
using ShootTheDead.GameEntities;
using ShootTheDead.Main;
using System.Diagnostics;

namespace ShootTheDead
{
    public class Player : Sprite
    {
        public float rotation;  // Adiciona a vari�vel para armazenar a rota��o
        protected float cooldown;
        protected float cooldownLeft;
        public static float TotalSeconds { get; set; }
        public bool Reloading { get; protected set; }
        private float Speed = 300;
        public Rectangle playerRect;
        private Map map = new Map();
        private bool isMoving;
        public int Health { get; private set; } = 5;
        public bool isDead { get; private set; }
        public List<Bullet> Bullets { get; private set; } = new List<Bullet>();
        public bool isTakingDamage { get; private set; } = false;
        private Texture2D bulletTexture;
        private bool isShooting = false;
        private float cool = 3;
        public int score;
        SoundEffect shoot;

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
            shoot = content.Load<SoundEffect>("Sfx/laserShoot");
        }

        public void Shoot()
        {
            Vector2 bulletPosition = sPosition;
            Vector2 bulletDirection = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            Bullet newBullet = new Bullet(bulletPosition, bulletDirection, bulletTexture);
            Bullets.Add(newBullet);
            shoot.Play();
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput(Keyboard.GetState(), gameTime);
            // Obt�m a posi��o do mouse
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

            // Calcula a dire��o entre o jogador e o mouse
            Vector2 direction = mousePosition - sPosition;
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            cooldownLeft -= 5 * delta;
            // Calcula a rota��o em radianos
            rotation = (float)Math.Atan2(direction.Y, direction.X);
            if (Mouse.GetState().LeftButton == ButtonState.Pressed) // Pressione a barra de espa�o para atirar
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
            if (isDead)
            {
                //implementar game over/mudanca de cena
            }

            if (isMoving)
            {
                // Atualiza a anima��o se o jogador estiver se movendo
                base.Update(gameTime);
            }
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                isDead = true;
            }

            Debug.WriteLine("Player Health: " + Health);
        }

        public void HandleInput(KeyboardState keyState, GameTime gameTime)
        {
            isMoving = false;

            if (keyState.IsKeyDown(Keys.W))
            {
                sPosition.Y -= Speed * Globals.deltaTime;
                isMoving = true;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                sPosition.X -= Speed * Globals.deltaTime;
                isMoving = true;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                sPosition.Y += Speed * Globals.deltaTime;
                isMoving = true;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                sPosition.X += Speed * Globals.deltaTime;
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
            // Desenha o jogador com rota��o
            spriteBatch.Draw(
                sTexture,
                sPosition,
                sRectangles[frameIndex],
                Color.White,
                rotation,  // Aplica a rota��o
                new Vector2(sRectangles[frameIndex].Width / 2, sRectangles[frameIndex].Height / 2),  // Origem da rota��o
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