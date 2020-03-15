using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace GameDesign
{
    class Remove : Selectors
    {
        public Remove()
        {
            color = Color.Red;
        }
        public override void Update(MouseState mouseState, MouseState prevMouseState, Tile selectedTile)
        {
            base.Update(mouseState, prevMouseState, selectedTile);
            if (mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed)
            {
                List<Tile> query = (from t in GameValues.grid.Cast<Tile>() where drawRectangle.Contains(t.rectangle.Location) && t.place == firstSelection.place && t.place == secondSelection.place && t.type != Type.grass select t).ToList();
                foreach(Tile t in query)
                {
                    TileChange.setGrass(t);
                }
                drawRectangle.X = 0;
                drawRectangle.Y = 0;
                drawRectangle.Width = 0;
                drawRectangle.Height = 0;
            }
        }
    }
}
