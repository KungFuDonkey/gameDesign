using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesign
{
    public class Building
    {
        public BuildingType type;
        public int tileCount;
        public List<Tile> currBuilding;
        public Rectangle rectangle;
        public Tile middleTile;
        bool hoverWarning;
        string warningString;
        Color grayColor = new Color(Color.Black, 0.5f);

        public Building(BuildingType buildingType, List<Tile> tiles)
        {
            tileCount = tiles.Count;
            type = buildingType;
            currBuilding = tiles;
            checkRectangle();
            warningString = "Building must be at least " + type.minSize.X + "x" + type.minSize.Y;
        }

        public void Update(MouseState currMouseState)
        {
            hoverWarning = rectangle.Contains(currMouseState.Position);
            if (Game1.cam.moving)
            {
                checkRectangle();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameValues.tileSize >= 8)
            {
                spriteBatch.DrawString(GameValues.font, type.ToString().Remove(0, 11), middleTile.rectangle.Center.ToVector2() - GameValues.font.MeasureString(type.ToString().Remove(0, 11)) / 2,
                                            Color.White);
            }
            if (!isMinSize())
            {
                spriteBatch.Draw(GameValues.warning, rectangle, Color.White);
                if (hoverWarning && GameValues.tileSize >= 8)
                {
                    spriteBatch.Draw(GameValues.tileTex, new Rectangle((rectangle.Location.ToVector2() - GameValues.font.MeasureString(warningString) / 2).ToPoint(), 
                                            GameValues.font.MeasureString(warningString).ToPoint()), grayColor);
                    spriteBatch.DrawString(GameValues.font, warningString, rectangle.Location.ToVector2() - GameValues.font.MeasureString(warningString) / 2, Color.White);
                }
            }
        }

        public void checkRectangle()
        {
            checkMiddle();
            rectangle = new Rectangle(middleTile.rectangle.Location, new Point(GameValues.tileSize * 2));
        }

        public void checkMiddle()
        {
            Point closest = currBuilding[0].rectangle.Location;
            Point furthest = currBuilding[0].rectangle.Location;
            Point middle;
            foreach (Tile t in currBuilding)
            {
                float distClose = (closest.X * closest.X) + (closest.Y * closest.Y);
                float distFar = (furthest.X * furthest.X) + (furthest.Y * furthest.Y);

                float distT = (t.rectangle.X * t.rectangle.X) + (t.rectangle.Y * t.rectangle.Y);
                if (distT < distClose)
                {
                    closest = t.rectangle.Location;
                }
                else if (distT > distFar)
                {
                    furthest = t.rectangle.Location;
                }
            }
            middle = closest + (furthest - closest) / new Point(2);
            foreach (Tile t in GameValues.grid)
            {
                if (t.rectangle.Contains(middle))
                {
                    middleTile = t;
                }
            }
        }

        public bool isMinSize()
        {
            if (currBuilding.Count < (type.minSize.X * type.minSize.Y))
            {
                return false;
            }
            foreach (Tile t in currBuilding)
            {
                bool isCorrect = true;
                for (int x = 0; x < type.minSize.X; x++)
                {
                    for (int y = 0; y < type.minSize.Y; y++)
                    {
                        if (x == 0 && y == 0)
                        {
                            continue;
                        }
                        int checkX = t.gridPos.X + x;
                        int checkY = t.gridPos.Y + y;

                        if ((checkX < 0 && checkX >= GameValues.gridWidth && checkY < 0 && checkY >= GameValues.gridHeight) || GameValues.grid[checkX, checkY].buildingType != type)
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

