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
        List<Node> gridNodes;
        List<Node> openlist;
        List<Node> path;
        Node startNode;
        Node targetNode;
        Point location = new Point(0, 0);
        Point oldlocation = new Point();

        public Rectangle drawRectangle = new Rectangle(20, 20, GameValues.tileSize, GameValues.tileSize);
        bool walking = false;
        int steps = 0;
        float timer = 0;
        public void Update(KeyboardState keys, KeyboardState prevKeys, GameTime gameTime)
        {
            drawRectangle.Size = new Point(GameValues.tileSize, GameValues.tileSize);
            if (keys.IsKeyDown(Keys.Enter) && !prevKeys.IsKeyDown(Keys.Enter))
            {
                walkToWards(50, 50, gameTime);
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
                    timer = 0;
                }
                oldlocation = location;
                location = new Point(path[steps].x, path[steps].y);
                drawRectangle = GameValues.grid[location.X, location.Y, 0].rectangle;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(GameValues.student, drawRectangle, Color.White);
        }

        private void walkToWards(int xDest, int yDest, GameTime gameTime)
        {
            if (!walking)
            {
                drawRectangle.X = location.X * GameValues.tileSize;
                drawRectangle.Y = location.Y * GameValues.tileSize;
                path = PathFinding(location, new Point(xDest, yDest));
                steps = 0;
                timer = 0;
                walking = true;
            }
        }
        private List<Node> PathFinding(Point startLocation, Point targetLocation)
        {
            gridNodes = GameValues.TileNodes();
            startNode = (from T in gridNodes where T.x == startLocation.X && T.y == startLocation.Y select T).First();
            startNode.gCost = 0;
            startNode.fCost = 0;

            targetNode = (from T in gridNodes where T.x == targetLocation.X && T.y == targetLocation.Y select T).First();

            openlist = new List<Node>();
            List<Node> path = new List<Node>();

            openlist.Add(startNode);

            while (openlist.Count > 0)
            {
                Node node = (from n in openlist select n).OrderBy(x => x.fCost).ThenBy(x => x.hCost).First();

                if (node == targetNode)
                {
                    path = RetracePath(startNode, targetNode);
                    walking = false;
                    return path;
                }
                Debug.WriteLine("x:" + node.x + " y:" + node.y + " gCost:" + node.gCost);
                openlist.Remove(node);
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
            return null;

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
            Node currentNode = (from n in openlist where n.x == endNode.x && n.y == endNode.y select n).First();

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
