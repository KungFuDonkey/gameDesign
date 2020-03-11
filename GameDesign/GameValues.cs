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
    enum GameState
    {
        build,
        select,
        remove
    }
    static class GameValues
    {
        public static int gridWidth = 200, gridHeight = 200, tileSize = 10, maxHeight = 20;
        public static int gridSize = gridHeight * gridWidth;
        public static Texture2D tileTex;
        public static Texture2D Student;
        public static List<Tile> tiles = new List<Tile>();
        public static List<NPC> npcs = new List<NPC>();
        public static GameState state = GameState.build;
        public static SpriteFont font;
        
        public static List<Node> TileNodes()
        {
            List<Node> nodes = new List<Node>();
            IEnumerable<Tile> query = (from T in tiles where T.walkable select T);
            foreach(Tile T in query)
            {
                if(T.layer == 0)
                {
                    nodes.Add(new Node(T.gridLocation.X, T.gridLocation.Y));
                }
            }
            return nodes;
        }
    

        public static List<Node> GetNeighbours(List<Node> grid, Node n)
        {
            List<Node> neighbours = new List<Node>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;
                    IEnumerable<Node> query = (from T in grid where T.x == n.x + x && T.y == n.y + y select T);
                    foreach (Node t in query)
                    {
                        neighbours.Add(t);
                    }
                }
            }
            return neighbours;
        }
        
    }
}
