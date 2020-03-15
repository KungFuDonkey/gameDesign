using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace GameDesign
{
    public enum Zone
    {
        Lesson,
        Break,
        Path,
        Road,
        Grass,
        Stone
    }
    public enum Type
    {
        grass,
        wall,
        floor,
        ceiling,
        Stone
    }
    public class Tile
    {
        public Type type;
        public BuildingType buildingType = GameValues.none;
        public Building thisBuilding
        {
            get
            {
                if (buildingType == GameValues.none)
                {
                    return null;
                }
                foreach (Building b in GameValues.buildings)
                {
                    if (b.currBuilding.Contains(this))
                    {
                        return b;
                    }
                }
                return null;
            }
        }
        public Rectangle rectangle;
        public Color standardColor;
        public Color drawColor;
        public Color color;
        public int layer;
        public bool walkable;
        public Zone zone;
        public bool occupied;
        public Point gridPos
        {
            get
            {
                Point pos = rectangle.Location - GameValues.grid[0, 0].rectangle.Location;
                return new Point(pos.X / GameValues.tileSize, pos.Y / GameValues.tileSize);
            }
        }

        public void Update(MouseState mouse)
        {
            if (rectangle.Contains(mouse.Position))
            {
                color = Color.Blue;
                Game1.SelectedTile = this;
            }
            else
            {
                color = drawColor;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameValues.tileTex, rectangle, color);
        }

        public void Draw(SpriteBatch spriteBatch, Phase currentPhase)
        {
            switch (currentPhase)
            {
                case Phase.morning:
                    drawColor.R = (byte)((int)standardColor.R * 0.6);
                    drawColor.G = (byte)((int)standardColor.G * 0.6);
                    drawColor.B = (byte)((int)standardColor.B * 0.8);
                    break;
                case Phase.noon:
                    color = standardColor;
                    break;
                case Phase.afternoon:
                    drawColor.R = (byte)((int)standardColor.R * 0.8);
                    drawColor.G = (byte)((int)standardColor.G * 0.6);
                    drawColor.B = (byte)((int)standardColor.B * 0.6);
                    break;
                case Phase.night:
                    drawColor.R = (byte)((int)standardColor.R * 0.4);
                    drawColor.G = (byte)((int)standardColor.G * 0.4);
                    drawColor.B = (byte)((int)standardColor.B * 0.4);
                    break;
            }
            spriteBatch.Draw(GameValues.tileTex, rectangle, color);
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle drawRectangle)
        {
            spriteBatch.Draw(GameValues.tileTex, drawRectangle, color);
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle drawRectangle, float alpha)
        {
            spriteBatch.Draw(GameValues.tileTex, drawRectangle, color * alpha);
        }
        public void DrawZone(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameValues.tileTex, rectangle, GameValues.zoneColors[(int)zone] * 0.5f);
        }

        public List<Tile> GetNeighbours()
        {
            List<Tile> neighbours = new List<Tile>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    int checkX = gridPos.X + x;
                    int checkY = gridPos.Y + y;

                    if (checkX >= 0 && checkX < GameValues.gridWidth && checkY >= 0 && checkY < GameValues.gridHeight)
                    {
                        neighbours.Add(GameValues.grid[checkX, checkY]);
                    }
                }
            }
            return neighbours;
        }
        public List<Tile> connectedNeighbours()
        {
            if (buildingType == GameValues.none)
            {
                return null;
            }
            List<Tile> returnList = new List<Tile>();
            List<Tile> openList = new List<Tile>();
            openList.Add(this);
            List<Tile> closedList = new List<Tile>();
            while (openList.Count > 0)
            {
                returnList.Add(openList[0]);
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (!((x == 0 && y == -1) || (x == -1 && y == 0) || (x == 1 && y == 0) || (x == 0 && y == 1)))
                        {
                            continue;
                        }
                        int checkX = openList[0].gridPos.X + x;
                        int checkY = openList[0].gridPos.Y + y;

                        if (checkX >= 0 && checkX < GameValues.gridWidth && checkY >= 0 && checkY < GameValues.gridHeight)
                        {
                            if (GameValues.grid[checkX, checkY].buildingType == buildingType && !(closedList.Contains(GameValues.grid[checkX, checkY]) || openList.Contains(GameValues.grid[checkX, checkY])))
                            {
                                openList.Add(GameValues.grid[checkX, checkY]);
                            }
                        }
                    }
                }
                closedList.Add(openList[0]);
                openList.Remove(openList[0]);
            }
            return returnList;
        }
    }
}
