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
    static class GameValues
    {
        public static int gridWidth = 256, gridHeight = 256, tileSize = 10, maxHeight = 20;
        public static int gridSize = gridHeight * gridWidth;
        public static Texture2D tileTex;
        public static List<Tile> tiles = new List<Tile>();

        public static List<Tile> Tiles
        {
            get { return tiles; }
        }

        public static Node GetTileNode(Point nodeLocation, Node parent)
        {
            foreach (Tile tile in tiles)
            {
                if(tile.rectangle.Location == nodeLocation)
                {
                    return new Node(nodeLocation, parent, tile.moveable);
                }
            }
            return null;
        }

        public static List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();
            for(int x = -1; x <= 1; x++)
            {
                for(int y = -1; y <= 1; y++)
                {
                    if(x==0 && y == 0)
                    {
                        continue;
                    }
                    else
                    {
                        Point checkNeighbour = new Point(x,y);
                        if(GetTileNode(node.gridLocation + checkNeighbour, null) == null)
                        {

                        }
                        else
                        {
                            neighbours.Add(GetTileNode(node.gridLocation + checkNeighbour, node));
                        }
                    }
                }
            }
            return neighbours;
        }

        public static void TracePath(Node n)
        {
            foreach (Tile tile in tiles)
            {
                if (tile.rectangle.Location == n.gridLocation)
                {
                    tile.color = Color.Red;
                }
            }
        }
    }
}
