using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameDesign
{
    static class TileChange
    {
        public static void setGrass(Tile tile)
        {
            tile.walkable = true;
            tile.standardColor = Color.Green;
            tile.occupied = false;
            tile.type = Type.grass;
            tile.zone = Zone.Grass;
            tile.enterance = false;
        }
        public static void setRoad(Tile tile)
        {
            tile.walkable = false;
            tile.standardColor = Color.DarkGray;
            tile.occupied = true;
            tile.zone = Zone.Grass;
            tile.type = Type.grass;
            tile.enterance = false;
        }
        public static void setPavement(Tile tile)
        {
            tile.walkable = true;
            tile.standardColor = Color.Gray;
            tile.occupied = true;
            tile.zone = Zone.Path;
            tile.type = Type.pavement;
            tile.enterance = false;
        }
        public static void setColorTile(Tile tile, Color color, Zone zone)
        {
            tile.walkable = false;
            tile.standardColor = color;
            tile.color = color;
            tile.zone = zone;
            tile.type = Type.building;
            tile.occupied = true;
            tile.enterance = false;
        }
    }
}
