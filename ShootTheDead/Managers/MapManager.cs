using Microsoft.Xna.Framework.Graphics;
using ShootTheDead.Main;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace ShootTheDead.Managers
{
    public class MapManager
    {
        Map map;
        Texture2D text;
        Rectangle[] xxyy;
        public MapManager()
        {
            map = new Map();
        }

        public void Initialize()
        {
            xxyy = new Rectangle[10];
            map.LoadMap("level1.txt");
            foreach (var o in map.tiles)
            {
                map.colliders.Add(new Rectangle((int)o.X, (int)o.Y, Globals.TileSize, Globals.TileSize));
                Debug.WriteLine($"Collider: {o.X}, {o.Y}");
            }
        }
        public Texture2D getTexture(string textureName, ContentManager Content)
        {
             return Content.Load<Texture2D>("Textures/" + textureName);
        }
        public void Update(Player player, Vector2 prevPos)
        {

            foreach (var rect in map.colliders)
            {
                if (player.playerRect.Intersects(rect))
                {
                    player.sPosition = prevPos;
                    Debug.WriteLine("Collision");

                }
            }
        }

        public void LoadMap(string levelFile)
        {
            map.LoadMap(levelFile);
        }

        public void DrawMap(SpriteBatch _spriteBatch, Texture2D text, Rectangle rect1, Rectangle rect2, Rectangle rect3)
        {
            map.drawMap(_spriteBatch, text, rect1, rect2, rect3);
        }

        public void LoadContent()
        {
            text = getTexture("tiles", Globals.Content);
            xxyy[0] = new Rectangle(384, 320, Globals.TileSize, Globals.TileSize);
            xxyy[1] = new Rectangle(0, 768, Globals.TileSize, Globals.TileSize);
            xxyy[2] = new Rectangle(256, 192, Globals.TileSize, Globals.TileSize);
            xxyy[3] = new Rectangle(512, 576, Globals.TileSize, Globals.TileSize);

        }

        public void Draw(SpriteBatch spriteBatch) 
        {

            //draw background
            for (int i = 0; i < Globals.GAME_WIDTH; i += Globals.TileSize)
            {
                for (int j = 0; j < Globals.GAME_HEIGHT; j += Globals.TileSize)
                {
                    spriteBatch.Draw(text, new Vector2(i, j), xxyy[2], Color.White);
                }
            }

            DrawMap(spriteBatch, text, xxyy[0], xxyy[1], xxyy[3]);

        }


    }
}
