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
    class Grass : Tile
    {
        public Grass(Rectangle _rectangle, int _place)
        {
            rectangle = _rectangle;
            standardColor = Color.Green;
            color = Color.Green;
            walkable = false;
            place = _place;
            zone = Zone.Grass;
            type = Type.grass;
        }
    }
}
