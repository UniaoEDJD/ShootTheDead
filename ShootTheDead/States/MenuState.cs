using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ShootTheDead.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootTheDead.States
{
    public class MenuState : State
    {
        // Lista que armazena os componentes (botões) do menu
        private List<Component> _components;
        // Variáveis para armazenar a largura e altura da tela
        private int screenWidth;
        private int screenHeight;
        private Game1 _game1 = new Game1();

        // Construtor da classe MenuState
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            // Obtém a largura e altura da tela
            screenWidth = graphicsDevice.Viewport.Width;
            screenHeight = graphicsDevice.Viewport.Height;

            // Carrega a textura e a fonte dos botões
            var buttonTexture = _content.Load<Texture2D>("Button");
            var buttonFont = _content.Load<SpriteFont>("Font");

            // Obtém as dimensões da textura dos botões
            var buttonWidth = buttonTexture.Width;
            var buttonHeight = buttonTexture.Height;

            // Define o espaçamento entre os botões
            var buttonSpacing = 10; // Espaço entre os botões

            // Inicializa a lista de componentes
            _components = new List<Component>();

            // Calcula a posição vertical central para os botões
            var centerY = (screenHeight - (_components.Count * (buttonHeight + buttonSpacing))) / 2;

            // Cria um botão "New Game"
            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((screenWidth - buttonWidth) / 2, centerY), // Posição central na tela
                Text = "New Game", // Texto do botão
            };
            newGameButton.Click += NewGameButton_Click; // Adiciona um evento de clique ao botão

            // Cria um botão "Credits"
            var creditsButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((screenWidth - buttonWidth) / 2, centerY + buttonHeight + buttonSpacing), // Posição abaixo do botão "New Game"
                Text = "Credits", // Texto do botão
            };
            creditsButton.Click += creditsButton_Click; // Adiciona um evento de clique ao botão

            // Cria um botão "Quit Game"
            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((screenWidth - buttonWidth) / 2, centerY + 2 * (buttonHeight + buttonSpacing)), // Posição abaixo do botão "Credits"
                Text = "Quit Game", // Texto do botão
            };
            quitGameButton.Click += QuitGameButton_Click; // Adiciona um evento de clique ao botão

            // Adiciona os botões à lista de componentes
            _components.Add(newGameButton);
            _components.Add(creditsButton);
            _components.Add(quitGameButton);
        }

        // Método para desenhar os componentes na tela
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {   
            foreach (var component in _components)
                { component.Draw(gameTime, spriteBatch);  }
            
        }

        // Método chamado quando o botão "Credits" é clicado
        private void creditsButton_Click(object sender, EventArgs e)
        {
            // Muda o estado do jogo para a cena de créditos
            _game.ChangeState(new CreditState(_game, _graphicsDevice, _content));
        }

        // Método chamado quando o botão "New Game" é clicado
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            // Muda o estado do jogo para a cena de novo jogo
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        // Método chamado quando o botão "Quit Game" é clicado
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        // Método para atualizar os componentes do menu
        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }

}