using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace GameDesign
{
    class Pavement : Tile
    {
        public Pavement(Rectangle _rectangle)
        {
            rectangle = _rectangle;
            standardColor = Color.Gray;
            color = Color.Gray;
            walkable = true;
            occupied = true;
            layer = 0;
            zone = Zone.Path;
            type = Type.grass;
        }
    }
}
