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
    public class NPC
    {
        int direction = 1;
        Random rng = new Random();
        List<Node> gridNodes;
        Heap<Node> openlist;
        List<Node> path;
        Node startNode;
        Node targetNode;
        Point location, oldLocation;
        Point targetLocation;


        public Rectangle drawRectangle = new Rectangle(0, 0, GameValues.tileSize, GameValues.tileSize);
        bool walking = false, calculating = false, start = false;
        int steps = 0;
        float timer = 0;

        public void Update(KeyboardState keys, KeyboardState prevKeys, GameTime gameTime)
        {
            drawRectangle.Size = new Point(GameValues.tileSize, GameValues.tileSize);
            try
            {
                if(GameValues.state != GameState.build)
                {
                    decideNextLocation();
                    start = true;
                }
            }
            catch
            {

            }
            if (walking)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (steps + 1 >= path.Count)
                {
                    walking = false;
                    return;
                }
                else if (timer > 1)
                {
                    steps++;
                    oldLocation = location;
                    timer = 0;
                }
                location = new Point(path[steps].x, path[steps].y);
                if(location.X - oldLocation.X > 0)
                {
                    direction = 1;
                }
                else if(location.X - oldLocation.X < 0)
                {
                    direction = 2;
                }
                else if(location.Y - oldLocation.Y > 0)
                {
                    direction = 3;
                }
                else
                {
                    direction = 4;
                }
                drawRectangle = GameValues.grid[location.X, location.Y, 0].rectangle;
            }
            else if (calculating)
            {
                if (GameValues.Paths.Count == (GameValues.enteranceTiles() * (GameValues.enteranceTiles() - 1)))
                {
                    Debug.WriteLine("test");
                    path = GameValues.Paths[rng.Next(0, GameValues.Paths.Count)];
                    calculating = false;
                    walking = true;
                }
                else
                {
                    PathFinding();
                }
            }
            else if(start)
            {
                location = decideNextLocation();
                targetLocation = decideNextLocation();
                walkToWards(targetLocation.X, targetLocation.Y);
            }
        }

        public Point decideNextLocation()
        {
            List<Tile> entrances = new List<Tile>();
            foreach (Tile t in GameValues.grid)
            {
                if (t.enterance)
                {
                    entrances.Add(t);
                }
            }
            return entrances[rng.Next(0, entrances.Count)].gridPos;
           
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if(direction == 1)
            {
                spritebatch.Draw(GameValues.student_right, drawRectangle, Color.White);
            }
            else if(direction == 2)
            {
                spritebatch.Draw(GameValues.student_left, drawRectangle, Color.White);
            }
            else if (direction == 3)
            {
                spritebatch.Draw(GameValues.student_down, drawRectangle, Color.White);
            }
            else
            {
                spritebatch.Draw(GameValues.student_up, drawRectangle, Color.White);
            }
        }

        private void walkToWards(int xDest, int yDest)
        {
            if (!walking)
            {
                gridNodes = GameValues.TileNodes();
                openlist = new Heap<Node>(GameValues.gridSize);
                
                startNode = (from T in gridNodes where T.x == location.X && T.y == location.Y select T).First();
                targetNode = (from T in gridNodes where T.x == xDest && T.y == yDest select T).First();
                startNode.gCost = 0;
                startNode.fCost = 0;
                openlist.Add(startNode);

                steps = 0;
                timer = 0;
                calculating = true;
            }
        }
        private void PathFinding()
        {

            if (openlist.Count > 0)
            {
                Node node = openlist.RemoveFirst();

                if (node == targetNode)
                {
                    path = RetracePath(startNode, node);
                    if (!GameValues.Paths.Contains(path))
                    {
                        GameValues.Paths.Add(path);
                    }
                    calculating = false;
                    walking = true;
                }
                Debug.WriteLine("x:" + node.x + " y:" + node.y + " gCost:" + node.gCost);
                Tile tile = GameValues.grid[node.x, node.y, 0];
                tile.standardColor = Color.Red;

                foreach (Node neighbour in GameValues.GetNeighbours(gridNodes, node))
                {
                    int newgCost = node.gCost + GetDistance(node, neighbour);
                    Debug.WriteLine("newgCost:" + newgCost + " neighbour:" + neighbour.gCost);

                    if (newgCost < neighbour.gCost)
                    {
                        neighbour.parent = node;
                        neighbour.gCost = newgCost;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.fCost = neighbour.gCost + neighbour.hCost;


                        if (!openlist.Contains(neighbour))
                        {
                            openlist.Add(neighbour);
                        }
                    }
                }
            }

        }



        int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Math.Abs(nodeA.x - nodeB.x);
            int dstY = Math.Abs(nodeA.y - nodeB.y);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);

        }

        List<Node> RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                GameValues.grid[currentNode.x, currentNode.y, 0].standardColor = Color.Blue;
                currentNode = currentNode.parent;
            }
            path.Reverse();
            return path;
        }
    }
}
