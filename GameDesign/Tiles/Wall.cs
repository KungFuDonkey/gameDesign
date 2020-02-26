using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesign
{
    class Wall : Tile
    {
        public Wall(Rectangle _rectangle, int _layer, Zone _zone)
        {
            rectangle = _rectangle;
            standardColor = Color.Brown;
            color = Color.Brown;
            moveable = false;
            layer = _layer;
            zone = _zone;
            type = Type.wall;
        }
    }
}
