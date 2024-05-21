# Shoot the Dead

### Trabalho realizado por: Tiago Miranda-27937 e Gonçalo Araújo-27928

### Descrição
Shoot The Dead é um jogo do estilo de Topdown Shooter que é um jogo endless onde não há limite de spawn de Zombies até o jogador morrer.
![Background](https://github.com/UniaoEDJD/ShootTheDead/assets/150021756/9bf57dfd-ce06-45aa-b8ad-e76680c0c1c3)

### Controles do Jogo
 - W: Andar para frente
 - A: Andar para a esquerda
 - S: Andar para trás
 - D: Andar para a direita
 - Botão esquerdo do rato: Disparar
 - F: Fullscreen

### Motivação
Foi nos proposto realizar um jogo 2D com o uso da framework Monogame e então decidimos fazer um top down shooter pois é um tipo de jogo que praticamente toda a gente gosta e também as primeiras ideias que surgiram foram sempre relacionadas a um shooter, logo ficou mais fácil decidir o jogo a fazer.

### Sobre este projeto
O nosso jogo é um top down shooter tal como dito em cima, onde fizemos a implementação de animações do player e dos inimigos, tal como o movimento autónomo dos inimigos até ao player, implementação de colisões nas bordas do mapa e colisões nos inimigos para quando recebem tiros do player, implementação de som, implementação de menus e de um sistema de scores. 

### Estruturamento do projeto
* Content:
  - pasta predefinida do monogame onde são carregados todos os recursos para o funcionamento do jogo, desde imagens, sons e ficheiros;
* Control:
  - é onde está a classe do Button.cs e onde foi feito o gerenciamento do botão para as suas funcionalidades e onde está a classe Component.cs que vai ser herdada pelo Button.cs para aceder às suas funções;
* GameEntities:
  - tal como o nome indica, são as entidades necessárias para o jogo, o player.cs e o enemy.cs e é onde fizemos o gerenciamento da arma e da bala;
* Main:
  - esta pasta contém a classe 2D.cs e HealthBar.cs que são usadas para a barra da vida presente no jogo, tem a classe Globals.cs que é usada para declarar variavéis globais que foram usadas no código, tem a classe Map.cs que tem as funções de desenhar e de carregar o mapa e tem a classe Score.cs para as variavéis do nome do player e o seu score, e a classe UI.cs que é para a interface dentro do jogo;
* Managers:
  - nesta pasta como o nome indica estão os gerenciadores do jogo, desde o EnemyManager.cs, MapManager.cs e o ScoreManager.cs;
* Sprites:
  - é a pasta onde contém o Sprites.cs e o AnimatedSprite.cs, são as classes que vão tratar do gerenciamento das animações precisas para o jogo tanto do jogador como do inimigo;
* States:
  - nesta pasta estão contidas todas as classes necessárias para os menus implementados no jogo, GameOverState.cs, GameState.cs, HighScoreState.cs, MenuState.cs e State.cs;
* TextBox:
  - pasta utilizada para o desenvolvimento de uma textbox no menu principal para assim o jogador conseguir introduzir o seu nickname para no fim do jogo ao perder ficar com o seu score guardado no ficheiro;
* E a classe Game1 que já vem com o projeto no monogame ao ser criado.

### Demonstração

https://github.com/UniaoEDJD/ShootTheDead/assets/34071970/ddc576e2-9dc7-467b-a958-8bf484f76b3c


### Conclusão
Com a conclusão deste projeto conseguimos adquirir bastante conhecimento e capacidades para futuros trabalhos que possámos vir a realizar no futuro com o uso da framework do Monogame.



### Bibliografia
Utilizamos alguns sprites diferentes fornecidos gratuitamente pelos seus criadores para o fim de aprendizagem de Desenvolvimento de jogos

Tilemap: https://www.unrealsoftware.de/files_show.php?file=17758

Gameover: https://wallhere.com/en/wallpaper/1004495

Sprites Jogador e Inimigo: https://opengameart.org/art-search?keys=top+down
