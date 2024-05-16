using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ShootTheDead.Main
{
    public class Map
    {
        public int Height, Width;
        public char[,] map;
        public int tileSize = 64;
        public List<Vector2> tiles;
        public List<Rectangle> colliders;


        public void LoadMap(string levelFile)
        {
            tiles = new List<Vector2>();
            colliders = new List<Rectangle>();
            string[] linhas = File.ReadAllLines($"Content/{levelFile}");
            int lineCount = linhas.Length;
            int colCount = linhas[0].Length;
            Vector2 position = new Vector2();

            map = new char[colCount, lineCount];
            for (int x = 0; x < colCount; x++)
            {
                for (int y = 0; y < lineCount; y++)
                {
                    position.X = x * tileSize;
                    position.Y = y * tileSize;

                    if (linhas[y][x] == '1')
                    {
                        map[x, y] = linhas[y][x];
                        tiles.Add(new Vector2(position.X, position.Y));
                        Debug.WriteLine($"Tile: {x}, {y}");
                    }
                    else
                    {
                        map[x, y] = linhas[y][x];
                    }   
                }
            }



        }

        public void drawMap(SpriteBatch _spriteBatch, Texture2D text, Rectangle rect1, Rectangle rect2, Rectangle rect3)
        {
            Height = 1920;
            Width = 1080;
            Vector2 position = new Vector2();
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    position.X = x * tileSize;
                    position.Y = y * tileSize;
                    switch (map[x, y])
                    {
                        case '0':
                            _spriteBatch.Draw(text, position, rect1, Color.White);
                            break;
                        case '1':
                            _spriteBatch.Draw(text, position, rect2, Color.White);
                            break;
                        case '2':
                            _spriteBatch.Draw(text, position, rect3, Color.White);
                            break;
                    }
                }
            }
        }

    }
}
