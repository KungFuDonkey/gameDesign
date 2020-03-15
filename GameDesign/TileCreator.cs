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
                List<Tile> query = (from t in GameValues.grid.Cast<Tile>() where drawRectangle.Contains(t.rectangle.Location) && t.place == firstSelection.place && t.place == secondSelection.place && !t.occupied select t).ToList();
                for (int i = 0; i < query.Count; i++)
                {
                    switch (GameValues.selectedTile)
                    {
                        case BuildTiles.pavement:
                            TileChange.setPavement(query[i]);
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
