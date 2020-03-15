using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace GameDesign
{
    class ColorTile:Tile
    {
        public ColorTile(Rectangle _rectangle, int _place, Color _color, Zone _zone)
        {
            rectangle = _rectangle;
            standardColor = color;
            color = _color;
            walkable = false;
            occupied = true;
            place = _place;
            zone = _zone;
            type = Type.building;
            enterance = false;
        }
    }
}
