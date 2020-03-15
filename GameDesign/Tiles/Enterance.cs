using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace GameDesign
{
    class Enterance : Tile
    {
        public Enterance(Rectangle _rectangle, int _place, Zone zone)
        {
            rectangle = _rectangle;
            standardColor = Color.Gray;
            color = Color.Gray;
            walkable = true;
            occupied = true;
            place = _place;
            zone = Zone.Path;
            type = Type.building;
            enterance = true;
        }
    }
}
