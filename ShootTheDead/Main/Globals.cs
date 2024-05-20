global using System;
global using Microsoft.VisualBasic;
global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using Microsoft.Xna.Framework.Input;
global using Microsoft.Xna.Framework.Content;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using System.IO;
global using System.Xml;
using System.Reflection.Metadata;

namespace ShootTheDead
{
    public static class Globals
    {
        public static int TileSize = 64;
        public static float deltaTime;
        public static int GAME_WIDTH = 1920;
        public static int GAME_HEIGHT = 1080;
        public static Point Bounds { get; set; }
        private static int _virtualWidth = 1920;
        private static int _virtualHeight = 1080;
        public static Viewport _viewport;
        public static Matrix _screenScaleMatrix;

        public static void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static void updateScreenScaleMatrix(GraphicsDevice graphicsDevice)
        {
            float screenHeight = graphicsDevice.PresentationParameters.BackBufferHeight;
            float screenWidth = graphicsDevice.PresentationParameters.BackBufferWidth;

            if (screenWidth / _virtualWidth > screenHeight / _virtualHeight)
            {
                float aspect = screenHeight / GAME_HEIGHT;
                _virtualWidth = (int)(aspect * GAME_WIDTH);
                _virtualHeight = (int)screenHeight;
            }
            else
            {
                float aspect = screenWidth / GAME_WIDTH;
                _virtualWidth = (int)screenWidth;
                _virtualHeight = (int)(aspect * GAME_HEIGHT);
            }

            _screenScaleMatrix = Matrix.CreateScale(_virtualWidth / (float)GAME_WIDTH);

            _viewport = new Viewport
            {
                X = (int)(screenWidth / 2 - _virtualWidth / 2),
                Y = (int)(screenHeight / 2 - _virtualHeight / 2),
                Width = _virtualWidth,
                Height = _virtualHeight,
                MinDepth = 0,
                MaxDepth = 1
            };
        }

    }
}