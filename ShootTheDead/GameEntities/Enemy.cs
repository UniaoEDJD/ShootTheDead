namespace ShootTheDead.GameEntities
{
    public class Enemy : AnimatedSprite
    {
        public int HP { get; private set; }  // Vida do inimigo
        private Rectangle rect { get; set; }  // Retângulo de desenho
        public Rectangle collider;  // Colisor do inimigo
        public bool isKilling { get; set; } = false;  // Flag para indicar se o inimigo está matando
        private int frameIndex;  // Índice do quadro de animação atual
        private float timeElapsed;  // Tempo decorrido desde a última atualização do quadro
        private float timeToUpdate;  // Tempo a ser esperado para atualizar o quadro
        private List<Rectangle> sRectangles;  // Lista de retângulos de animação

        // Construtor do inimigo
        public Enemy(Vector2 pos, Texture2D tex) : base(pos, tex)
        {
            rect = new Rectangle(0, 0, 64, tex.Height);  // Inicializa o retângulo de desenho
            Speed = 250;  // Define a velocidade do inimigo
            HP = 2;  // Define a vida inicial do inimigo
            collider = new Rectangle(0, 0, 144, tex.Height);  // Inicializa o colisor
            sRectangles = new List<Rectangle>();  // Inicializa a lista de retângulos de animação
            frameIndex = 0;  // Inicializa o índice do quadro de animação
            timeElapsed = 0;  // Inicializa o tempo decorrido
            framesPerSecond = 10;  // Define os quadros por segundo da animação
            AddAnimation(tex.Width / 17);  // Adiciona a animação dividindo a largura da textura pelo número de quadros
        }

        // Propriedade para definir os frames por segundo
        public int framesPerSecond
        {
            set { timeToUpdate = (1f / value); }  // Calcula o tempo a ser esperado para atualizar o frame
        }

        // Método para adicionar animação
        public void AddAnimation(int frameWidth)
        {
            // Adiciona os retângulos de cada frame de animação na lista
            for (int i = 0; i < sTexture.Width; i += frameWidth)
            {
                sRectangles.Add(new Rectangle(i, 0, frameWidth, sTexture.Height));
            }
        }

        // Método para o inimigo receber dano
        public void TakeDamage(int dmg)
        {
            HP -= dmg;  // Subtrai o dano da vida do inimigo
        }

        // Método de atualização do inimigo
        public void Update(Player player, GameTime gameTime)
        {
            // Atualiza a posição e rotação
            var toPlayer = player.sPosition - sPosition;  // Calcula a direção até o jogador
            Rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);  // Calcula a rotação em direção ao jogador
            collider.X = (int)sPosition.X - 50;  // Atualiza a posição X do colider
            collider.Y = (int)sPosition.Y - 50;  // Atualiza a posição Y do colider
            if (toPlayer.Length() > 4)  // Verifica se a distância até ao jogador é maior que 4
            {
                var dir = Vector2.Normalize(toPlayer);  // Normaliza a direção
                sPosition += dir * Speed * Globals.deltaTime;  // Move o inimigo para o jogador
            }

            // Atualiza a animação
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;  // Incrementa o tempo decorrido
            if (timeElapsed > timeToUpdate)  // Verifica se é para atualizar o frame
            {
                frameIndex++;  // Avança para o próximo frame
                if (frameIndex >= sRectangles.Count)  // Volta para o primeiro quadro se ultrapassar o último
                {
                    frameIndex = 0;
                }
                timeElapsed -= timeToUpdate;  // Reseta o tempo decorrido
            }

        }

        // Método de desenho do inimigo
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                sTexture,
                sPosition,
                sRectangles[frameIndex],  // Desenha o frame atual da animação
                Color.White,
                Rotation,  // Aplica a rotação
                new Vector2(sRectangles[frameIndex].Width / 2, sRectangles[frameIndex].Height / 2),  // Origem da rotação
                1.0f,
                SpriteEffects.None,
                0f
            );
        }
    }
}
