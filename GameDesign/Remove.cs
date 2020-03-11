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
                IEnumerable<Tile> query = from t in GameValues.tiles where drawRectangle.Contains(t.rectangle.Location) && t.layer == firstSelection.layer && t.layer == secondSelection.layer && t.type != Type.grass select t;
                int count = query.Count();
                for (int i = 0; i<count; ++i)
                {
                    if (hasCeiling(query.ElementAt(i)))
                    {
                        return;
                    }
                }
                for(int i = 0; i<count; ++i)
                {
                    Tile t = query.ElementAt(0);
                    if (t.layer == 0)
                    {
                        TileChange.setGrass(t);
                    }
                    else
                    {
                        TileChange.setCeiling(t);
                    }
                    try
                    {
                        Tile ceiling = (from x in GameValues.tiles where x.rectangle.X == t.rectangle.X && x.rectangle.Y == t.rectangle.Y && x.layer > t.layer select x).First();
                        GameValues.tiles.Remove(ceiling);
                    }
                    catch
                    {

                    }
                }
                drawRectangle.X = 0;
                drawRectangle.Y = 0;
                drawRectangle.Width = 0;
                drawRectangle.Height = 0;
            }
        }
        public bool hasCeiling(Tile selectedTile)
        {
            IEnumerable<Tile> query = from t in GameValues.tiles where t.rectangle.X == selectedTile.rectangle.X && t.rectangle.Y == selectedTile.rectangle.Y && t.layer > selectedTile.layer && t.type != Type.ceiling select t;
            foreach(Tile t in query)
            {
                return true;
            }
            return false;
        }
    }
}
