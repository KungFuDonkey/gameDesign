﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace GameDesign
{
    class Pavement : Tile
    {
        public Pavement(Rectangle _rectangle, int _place)
        {
            rectangle = _rectangle;
            standardColor = Color.Gray;
            color = Color.Gray;
            walkable = true;
            occupied = true;
            place = _place;
            zone = Zone.Path;
            type = Type.pavement;
            enterance = false;
        }
    }
}
