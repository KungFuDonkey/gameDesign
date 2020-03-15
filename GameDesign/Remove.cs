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
                IEnumerable<Tile> query = from t in GameValues.grid.Cast<Tile>() where drawRectangle.Contains(t.rectangle.Location) && t.place == firstSelection.place && t.place == secondSelection.place && t.type != Type.grass select t;
                int count = query.Count(); //gets downed by 1 everytime query.elementat(0) is called on line 27
                for(int i = 0; i<count; ++i)
                {
                    Tile t = query.ElementAt(0);
                    if (t.place == 0)
                    {
                        TileChange.setGrass(t);
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
