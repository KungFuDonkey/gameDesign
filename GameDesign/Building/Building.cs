using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public Point middle;
        public List<Tile> currBuilding;
        public Rectangle rectangle;

        public Building(BuildingType buildingType, List<Tile> tiles)
        {
            tileCount = tiles.Count;
            type = buildingType;
            currBuilding = tiles;
            rectangle = new Rectangle(currBuilding[0].rectangle.Location, new Point(GameValues.tileSize * 2, GameValues.tileSize * 2));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isMinSize())
            {
                spriteBatch.Draw(GameValues.warning, rectangle, Color.White);
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

                        if (checkX >= 0 && checkX < GameValues.gridWidth && checkY >= 0 && checkY < GameValues.gridHeight)
                        {
                            continue;
                        }
                        if (GameValues.grid[checkX, checkY].buildingType != type)
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

