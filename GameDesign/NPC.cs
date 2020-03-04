using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GameDesign
{
    public class NPC
    {

        List<Tile> tileWorld;
        Vector2 currentLocation;

        
        
        private void Initialize()
        {
            tileWorld = GameValues.Tiles;
        }

        public void Update(KeyboardState keys)
        {
            if (keys.IsKeyDown(Keys.Enter))
            {
                CalculateShortest_Path(new Point(5, 20), new Point(60, 90));

            }
        }

        public void CalculateShortest_Path(Point starttile, Point endtile)
        {
            Node startNode = GameValues.GetTileNode(starttile, null);
            Node endNode = GameValues.GetTileNode(starttile, null);


            List<Node> openlist = new List<Node>();
            List<Node> closedlist = new List<Node>();


            openlist.Add(startNode);


            while (openlist.Count > 0)
            {
                Node currentNode = openlist[0];
                for (int i = 1; i < openlist.Count; i++)
                {
                    if (openlist[i].fCost < currentNode.fCost || openlist[i].fCost == currentNode.fCost && openlist[i].hCost < currentNode.hCost)
                    {
                        currentNode = openlist[i];
                    }
                }
                openlist.Remove(currentNode);
                closedlist.Remove(currentNode);
                if (currentNode == endNode)
                {
                    RetracePath(startNode, endNode);  
                    return;
                }

                foreach (Node neighbour in GameValues.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedlist.Contains(neighbour))
                    {
                        continue;
                    }
                    int newCostToNeighbour = currentNode.gCost + Math.Max(Math.Abs(currentNode.gridLocation.X - neighbour.gridLocation.X), Math.Abs(currentNode.gridLocation.Y - neighbour.gridLocation.Y));

                    if (newCostToNeighbour < neighbour.gCost || !openlist.Contains(neighbour))
                    {
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = Math.Max(Math.Abs(neighbour.gridLocation.X - endtile.X), Math.Abs(neighbour.gridLocation.Y - endtile.Y));
                        neighbour.parent = currentNode;
                        if (!openlist.Contains(neighbour))
                        {
                            openlist.Add(neighbour);
                        }

                    }
                }
            }
        }
        void RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;
            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            path.Reverse();
            for(int i = 0; i < path.Count; i++)
            {
                GameValues.TracePath(path[i]);
            }
        }
    }



    class Node
    {
        public Node(Point _gridLocation, Node _parent, bool _walkable)
        {
            gridLocation = _gridLocation;
            parent = _parent;
            walkable = _walkable;
        }
        public int hCost;
        public int fCost
        {
            get { return hCost + fCost; }
        }
        public bool walkable;
        public int gCost;
        public Node parent;
        public Point gridLocation;
    }


}
