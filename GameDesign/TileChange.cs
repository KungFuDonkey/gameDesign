using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace rollercoaster_tycoon_ripoff
{
    static class TileChange
    {
        public static void setWall(Tile tile)
        {
            tile.moveable = false;
            tile.standardColor = Color.Brown;
        }
    }
}
