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
global using ShootTheDead.States;
using System.Reflection.Metadata;

namespace ShootTheDead
{
    public static class Globals
    {
        public static int TileSize = 64;
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static int GAME_WIDTH = 1920;
        public static int GAME_HEIGHT = 1080;



    }
}