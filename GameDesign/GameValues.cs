using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace GameDesign
{
    static class GameValues
    {
        public static int gridWidth = 256, gridHeight = 256, tileSize = 10, maxHeight = 20;
        public static int gridSize = gridHeight * gridWidth;
        public static Texture2D tileTex;
        public static List<Tile> tiles = new List<Tile>();
    }
}
