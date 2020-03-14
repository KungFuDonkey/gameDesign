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
        public BuildingType buildingType = GameValues.buildingTypes[0];
        public Rectangle rectangle;
        public Color standardColor;
        public Color drawColor;
        public Color color;
        public int layer;
        public bool walkable;
        public Zone zone;
        public bool occupied;
        public Point minSize = new Point(4, 4);

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

        public List<Tile> connectedNeighbours()
        {
            List<Tile> returnList = new List<Tile>();
            List<Tile> openList = new List<Tile>();
            openList.Add(this);
            List<Tile> closedList = new List<Tile>();
            while (openList.Count > 0)
            {
                returnList.Add(openList[0]);
                for (int x = -1; x < 1; x++)
                {
                    for (int y = -1; y < 1; y++)
                    {
                        if (x == 0 && y == 0)
                        {
                            continue;
                        }
                        int checkX = openList[0].rectangle.Center.X + x * GameValues.tileSize;
                        int checkY = openList[0].rectangle.Center.Y + y * GameValues.tileSize;
                        Tile checkTile = (Tile)from t in GameValues.tiles where t.rectangle.Contains(new Point(checkX, checkY)) select t;
                        if (checkTile.buildingType == openList[0].buildingType && !closedList.Contains(checkTile))
                        {
                            openList.Add(checkTile);
                        }
                    }
                }
                closedList.Add(openList[0]);
                openList.Remove(openList[0]);
            }
            return returnList;
        }

        public bool isMinSize()
        {
            List<Tile> currBuilding = connectedNeighbours();
            if (currBuilding.Count < (buildingType.minSize.X * buildingType.minSize.Y))
            {
                return false;
            }
            foreach (Tile t in currBuilding)
            {
                bool isCorrect = true;
                for (int x = 0; x < minSize.X; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        if (x == 0 && y == 0)
                        {
                            continue;
                        }
                        int checkX = t.rectangle.Center.X + x * GameValues.tileSize;
                        int checkY = t.rectangle.Center.Y + y * GameValues.tileSize;
                        Tile checkTile = (Tile)from tile in GameValues.tiles where tile.rectangle.Contains(new Point(checkX, checkY)) select tile;
                        if (checkTile.buildingType != buildingType)
                        {
                            isCorrect = false;
                        }
                    }
                }
                if (isCorrect)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
