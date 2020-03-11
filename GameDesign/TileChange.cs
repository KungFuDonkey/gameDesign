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
        public static void setWall(Tile tile)
        {
            tile.walkable = false;
            tile.standardColor = Color.Brown;
            tile.occupied = true;
            tile.type = Type.wall;
        }
        public static void setFloor(Tile tile)
        {
            tile.walkable = true;
            tile.standardColor = Color.Beige;
            tile.occupied = true;
            tile.type = Type.floor;
        }
        public static void setGrass(Tile tile)
        {
            tile.walkable = true;
            tile.standardColor = Color.Green;
            tile.occupied = false;
            tile.type = Type.grass;
            tile.zone = Zone.Grass;
        }
        public static void setCeiling(Tile tile)
        {
            tile.walkable = true;
            tile.standardColor = Color.Gray;
            tile.occupied = false;
            tile.type = Type.ceiling;
            tile.zone = Zone.Grass;
        }
        public static void setRoad(Tile tile)
        {
            tile.walkable = false;
            tile.standardColor = Color.DarkGray;
            tile.occupied = true;
            tile.zone = Zone.Grass;
            tile.type = Type.grass;
        }
        public static void setPavement(Tile tile)
        {
            tile.walkable = true;
            tile.standardColor = Color.Gray;
            tile.occupied = true;
            tile.zone = Zone.Path;
            tile.type = Type.grass;
        }
    }
}
