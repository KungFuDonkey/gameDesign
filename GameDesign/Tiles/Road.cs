using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace GameDesign
{
    class Road : Tile
    {
        public Road(Rectangle _rectangle)
        {
            rectangle = _rectangle;
            standardColor = Color.DarkGray;
            color = Color.DarkGray;
            occupied = true;
            walkable = false;
            zone = Zone.Grass;
            type = Type.grass;
            layer = 0;
        }
    }
}
