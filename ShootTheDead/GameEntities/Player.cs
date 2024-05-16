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
       


        public Player(Vector2 position, Texture2D tex) : base(position, tex)
        {
            framesPerSecond = 10;

            playerRect = new Rectangle((int)position.X, (int)position.Y, 16, 16);
        }

        public void Reset()
        {
            //_weapon1 = new Pistol();
            //_weapon2 = new Rifle();
            //IsDead = false;
        }

        public void LoadContent(ContentManager content)
        {
            sTexture = content.Load<Texture2D>("survivor-move_handgun");
            AddAnimation(20);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            

            HandleInput(Keyboard.GetState(), gameTime);

            // Obtém a posição do mouse
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

            // Calcula a direção entre o jogador e o mouse
            Vector2 direction = mousePosition - sPosition;

            // Calcula a rotação em radianos
            rotation = (float)Math.Atan2(direction.Y, direction.X);



            /*if (cooldownLeft > 0)
            {
                inGame = false;
            }
            if (cooldownLeft > 0)
                cooldownLeft--;
            if (MouseLeftDown)
            {
                
            }*/


            base.Update(gameTime);
        }

        public void HandleInput(KeyboardState keyState, GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyState.IsKeyDown(Keys.W))
            {
                sPosition.Y -= Speed * deltaTime;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                sPosition.X -= Speed * deltaTime;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                sPosition.Y += Speed * deltaTime;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                sPosition.X += Speed * deltaTime;
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
        }
    }
}