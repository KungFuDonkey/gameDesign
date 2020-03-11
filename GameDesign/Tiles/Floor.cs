using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesign
{
    class Floor : Tile
    {
        public Floor(Rectangle _rectangle, int _layer, Zone _zone)
        {
            rectangle = _rectangle;
            standardColor = Color.Beige;
            color = Color.Beige;
            walkable = true;
            layer = _layer;
            zone = _zone;
            type = Type.floor;
        }
    }
}
