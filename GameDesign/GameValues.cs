using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace rollercoaster_tycoon_ripoff
{
    static class GameValues
    {
        public static int gridWidth = 100;
        public static int gridHeight = 10;
        public static int gridSize = gridHeight * gridWidth;
        public static int tileSize = 50;
        public static int maxHeight = 20;
        public static Texture2D tileTex;
        public static List<Tile> tiles = new List<Tile>();
    }
}
