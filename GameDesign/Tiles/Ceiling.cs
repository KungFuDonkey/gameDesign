using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace GameDesign
{
    class Ceiling : Tile
    {
        public Ceiling(Rectangle _rectangle, int _layer, Zone _zone)
        {
            rectangle = _rectangle;
            standardColor = Color.Gray;
            color = Color.Gray;
            occupied = false;
            moveable = true;
            layer = _layer;
            zone = _zone;
            type = Type.ceiling;
        }
    }
}
