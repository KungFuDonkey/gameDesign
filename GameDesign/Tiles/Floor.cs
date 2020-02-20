using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rollercoaster_tycoon_ripoff
{
    class Floor : Tile
    {
        public Floor(Rectangle _rectangle, int _layer, Zone _zone)
        {
            rectangle = _rectangle;
            standardColor = Color.Beige;
            color = Color.Beige;
            moveable = true;
            layer = _layer;
            zone = _zone;
        }
    }
}
