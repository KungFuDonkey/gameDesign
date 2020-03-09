using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;


namespace GameDesign
{
    public class NPC
    {
        Random rand = new Random();
        Color randColor = new Color(0, 0, 0);
        Node startNode;
        Node endNode;
        bool readyToCalc = false;

        Heap<Node> openlist;
        HashSet<Node> closedlist;

        public void Update(KeyboardState keys)
        {
            
            if (keys.IsKeyDown(Keys.Enter) && !readyToCalc)
            {

                prepareShortestPath(new Point(5, 20), new Point(120, 180));

            }
            if (readyToCalc)
            { 

                if (openlist.Count > 0)
                {

                    Node currentNode = openlist.RemoveFirst();
                   
                    closedlist.Add(currentNode);
                    randColor.R = (byte)rand.Next(256); 
                    GameValues.TracePath(currentNode, randColor);


                    if (currentNode.gridLocation == endNode.gridLocation)
                    {
                        GameValues.TracePath(currentNode, Color.Blue);
                        //RetracePath(startNode, endNode);
                        readyToCalc = false;
                        Debug.WriteLine("1");
                        return;
                    }

                    foreach (Node neighbour in GameValues.GetNeighbours(currentNode))
                    {
                        if (!neighbour.walkable || closedlist.Contains(neighbour))
                        {
                            continue;
                        }
                        int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                        if (newCostToNeighbour < neighbour.gCost || !openlist.Contains(neighbour))
                        {
                            neighbour.gCost = newCostToNeighbour;
                            neighbour.hCost = GetDistance(neighbour, endNode); 
                            neighbour.parent = currentNode;
                            if (!openlist.Contains(neighbour))
                            {
                                openlist.Add(neighbour);
                            }
                            else
                            {
                                openlist.UpdateItem(neighbour);
                            }

                        }
                    }
                }
            }
        }

        public int GetDistance(Node startNode, Node endNode)
        {
            int distX = Math.Abs(startNode.gridLocation.X - endNode.gridLocation.X);
            int distY = Math.Abs(startNode.gridLocation.Y - endNode.gridLocation.Y);

            if(distX > distY)
            {
                return 14 * distY + 10 * (distX - distY);
            }
            return 14 * distX + 10 * (distY - distX);
        }

        public void prepareShortestPath(Point starttile, Point endtile)
        {
            startNode = GameValues.GetTileNode(starttile);
            endNode = GameValues.GetTileNode(endtile);


            openlist = new Heap<Node>(GameValues.gridSize);
            closedlist = new HashSet<Node>();
            openlist.Add(startNode);
            readyToCalc = true;
        }

        //public void CalculateShortest_Path(Point starttile, Point endtile)
        //{
        //    Node startNode = GameValues.GetTileNode(starttile, null);
        //    Node endNode = GameValues.GetTileNode(starttile, null);


        //    List<Node> openlist = new List<Node>();
        //    List<Node> closedlist = new List<Node>();


        //    openlist.Add(startNode);


        //    while (openlist.Count > 0)
        //    {
        //        Node currentNode = openlist[0];
        //        for (int i = 1; i < openlist.Count; i++)
        //        {
        //            if (openlist[i].fCost < currentNode.fCost || openlist[i].fCost == currentNode.fCost && openlist[i].hCost < currentNode.hCost)
        //            {
        //                currentNode = openlist[i];
        //            }
        //        }
        //        openlist.Remove(currentNode);
        //        closedlist.Add(currentNode);
        //        GameValues.TracePath(currentNode);

        //        if (currentNode == endNode)
        //        {
        //            RetracePath(startNode, endNode);  
        //            return;
        //        }

        //        foreach (Node neighbour in GameValues.GetNeighbours(currentNode))
        //        {
        //            if (!neighbour.walkable || closedlist.Contains(neighbour))
        //            {
        //                continue;
        //            }
        //            int newCostToNeighbour = currentNode.gCost + Math.Max(Math.Abs(currentNode.gridLocation.X - neighbour.gridLocation.X), Math.Abs(currentNode.gridLocation.Y - neighbour.gridLocation.Y));

        //            if (newCostToNeighbour < neighbour.gCost || !openlist.Contains(neighbour))
        //            {
        //                neighbour.gCost = newCostToNeighbour;
        //                neighbour.hCost = Math.Max(Math.Abs(neighbour.gridLocation.X - endtile.X), Math.Abs(neighbour.gridLocation.Y - endtile.Y));
        //                neighbour.parent = currentNode;
        //                if (!openlist.Contains(neighbour))
        //                {
        //                    openlist.Add(neighbour);
        //                }

        //            }
        //        }
        //    }
        //}

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
                GameValues.TracePath(path[i], Color.Green);
            }
        }
    }



    public class Node : IHeapItem<Node>
    {
        public Node(Point _gridLocation, bool _walkable)
        {
            gridLocation = _gridLocation;
            walkable = _walkable;
        }
        public int hCost;
        public int fCost
        {
            get { return hCost + gCost; }
        }
        public bool walkable;
        public int gCost;
        int heapIndex;
        public Node parent;
        public Point gridLocation;
        public int HeapIndex
        {
            get { return heapIndex; }
            set { heapIndex = value; }
        }

        public int CompareTo(Node nodeToCompare)
        {
            int compare = fCost.CompareTo(nodeToCompare.fCost);
            if(compare == 0)
            {
                compare = hCost.CompareTo(nodeToCompare.hCost);
            }
            return -compare;
        }
    }




}
