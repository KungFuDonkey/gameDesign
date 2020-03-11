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
    class TileCreator : Selectors
    {

        public TileCreator()
        {
            color = Color.Gray;
        }
        Rectangle notSelectedRectangle = new Rectangle(0, 0, 0, 0);
        public override void Update(MouseState mouseState, MouseState prevMouseState, Tile selectedTile)
        {
            base.Update(mouseState, prevMouseState, selectedTile);
            color = GameValues.tileColors[(int)GameValues.selectedTile];
            if (mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed)
            {
                List<Tile> query;
                if (GameValues.selectedTile == BuildTiles.wall)
                {
                    notSelectedRectangle.X = drawRectangle.X + GameValues.tileSize;
                    notSelectedRectangle.Y = drawRectangle.Y + GameValues.tileSize;
                    notSelectedRectangle.Width = drawRectangle.Width - 2 * GameValues.tileSize;
                    notSelectedRectangle.Height = drawRectangle.Height - 2 * GameValues.tileSize;
                    query = (from t in GameValues.tiles where drawRectangle.Contains(t.rectangle.Location) && !notSelectedRectangle.Contains(t.rectangle.Location) && t.layer == firstSelection.layer && t.layer == secondSelection.layer && !t.occupied select t).ToList();
                }
                else
                {
                    query = (from t in GameValues.tiles where drawRectangle.Contains(t.rectangle.Location) && t.layer == firstSelection.layer && t.layer == secondSelection.layer && !t.occupied select t).ToList();
                }
                for(int i = 0; i < query.Count; i++)
                {
                    switch (GameValues.selectedTile)
                    {
                        case BuildTiles.floor:
                            TileChange.setFloor(query[i]);
                            GameValues.tiles.Add(new Ceiling(query[i].rectangle, query[i].layer + 1, Zone.Grass));
                            break;
                        case BuildTiles.pavement:
                            TileChange.setPavement(query[i]);
                            break;
                        case BuildTiles.wall:
                            TileChange.setWall(query[i]);
                            GameValues.tiles.Add(new Ceiling(query[i].rectangle, query[i].layer + 1, Zone.Grass));
                            break;
                    }
                }
                drawRectangle.X = 0;
                drawRectangle.Y = 0;
                drawRectangle.Width = 0;
                drawRectangle.Height = 0;
            }
        }
    }
}
