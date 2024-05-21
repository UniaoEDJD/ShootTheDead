

namespace ShootTheDead
{
    public class Player : Sprite
    {
        public float rotation;  // Adiciona a vari�vel para armazenar a rota��o
        protected float cooldown;
        protected float cooldownLeft;
        public static float TotalSeconds { get; set; }
        private float Speed = 300;
        public Rectangle playerRect;
        private bool isMoving;
        public int Health { get; private set; } = 5;
        public bool isDead { get; set; }
        public List<Bullet> Bullets { get; private set; } = new List<Bullet>();
        public int score;
        private Texture2D bulletTexture;
        SoundEffect shoot;
        static SoundEffect hurt;

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
            var speedScale = 1.2 * (Globals.scaleX);
            Speed *= (int)speedScale;
            shoot = content.Load<SoundEffect>("Sfx/laserShoot");
            hurt = content.Load<SoundEffect>("Sfx/oof");
        }

        public void Shoot()
        {
            Vector2 bulletPosition = sPosition;
            Vector2 bulletDirection = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            Bullet newBullet = new Bullet(bulletPosition, bulletDirection, bulletTexture);
            Bullets.Add(newBullet);
            shoot.Play();
        }

        public void Reset()
        {
            isDead = false;
            sPosition = new Vector2(300, 300);
        }

        public override void Update(GameTime gameTime)
        {
            
            KeyboardState state = Keyboard.GetState();
            // Obt�m a posi��o do mouse
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
           // Vector2 transformedMousePosition = Globals.TransformMouse(mousePosition);
            HandleInput(state ,gameTime);

            // Calcula a dire��o entre o jogador e o mouse
            Vector2 direction = mousePosition - sPosition;
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            cooldownLeft -= 5 * delta;
            // Calcula a rota��o em radianos
            rotation = (float)Math.Atan2(direction.Y, direction.X);
            if (Mouse.GetState().LeftButton == ButtonState.Pressed) //  leftclick para atirar
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
                base.Update(gameTime);
            }
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            hurt.Play();
            if (Health <= 0)
            {
                isDead = true;
            }
        }

        public void HandleInput(KeyboardState state,GameTime gameTime)
        {
            isMoving = false;
            
            if (state.IsKeyDown(Keys.W))
            {
                sPosition.Y -= Speed * Globals.deltaTime;
                isMoving = true;
            }
            if (state.IsKeyDown(Keys.A))
            {
               
                sPosition.X -= Speed * Globals.deltaTime;
                isMoving = true;
            }
            if (state.IsKeyDown(Keys.S))
            {
               
                sPosition.Y += Speed * Globals.deltaTime;
                isMoving = true;
            }
            if (state.IsKeyDown(Keys.D))
            {
               
                sPosition.X += Speed * Globals.deltaTime;
                isMoving = true;
            }
            playerRect.X = ((int)sPosition.X - 25);
            playerRect.Y = ((int)sPosition.Y - 25);
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