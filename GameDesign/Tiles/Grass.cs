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
        public Grass(Rectangle _rectangle, int _layer, Point _gridLocation)
        {
            gridLocation = _gridLocation; 
            rectangle = _rectangle;
            standardColor = Color.Green;
            color = Color.Green;
            walkable = true;
            layer = _layer;
            zone = Zone.Grass;
            type = Type.grass;
        }
    }
}
