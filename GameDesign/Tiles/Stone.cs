using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace GameDesign
{
    class Stone : Tile
    {
        public Stone(Rectangle _rectangle, int _layer)
        {
            rectangle = _rectangle;
            standardColor = Color.Gray;
            color = Color.Gray;
            moveable = false;
            layer = _layer;
            zone = Zone.Stone;
            type = Type.Stone;
        }
    }
}
