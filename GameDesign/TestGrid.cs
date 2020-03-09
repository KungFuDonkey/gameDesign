using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace GameDesign
{
    public class TestGrid
    {
        static int gridwidth = 100;
        static int gridheight = 100;
        public bool[,] grid = new bool[gridwidth, gridheight];

        public void Initialize()
        {
            for (int x = 0; x < gridwidth; x++)
            {
                for (int y = 0; y < gridheight; y++)
                {
                    if (x % 6 == 0 || y % 8 == 0)
                    {
                        grid[x, y] = false;
                    }
                    else
                    {
                        grid[x, y] = true;
                    }
                }
            }
        }
            
        public Node GetTileNode(Point nodeLocation, Node parent)
        {
            for (int x = 0; x < gridwidth; x++)
            {
                for (int y = 0; y < gridheight; y++)
                {
                    Point location = new Point(x, y);
                    if (location == nodeLocation)
                    {
                        return new Node(location, grid[x, y]);
                    }
                }
            }
            return null;
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }
                    else
                    {
                        Point checkNeighbour = new Point(x, y);
                        if (GetTileNode(node.gridLocation + checkNeighbour, null) == null)
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


        public void TracePath(Node n)
        {
            
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Point location = new Point(x, y);
                    if (location == n.gridLocation)
                    {
                        Debug.WriteLine(n.gridLocation);
                    }
                }
            }
        }
    }


}
