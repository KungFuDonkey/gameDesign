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
                IEnumerable<Tile> query = from t in GameValues.grid.Cast<Tile>() where drawRectangle.Contains(t.rectangle.Location) && t.type != Type.grass select t;
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
                    t.buildingType = GameValues.none;
                    if (t.layer == 0)
                    {
                        TileChange.setGrass(t);
                    }
                    else
                    {
                        TileChange.setCeiling(t);
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
            IEnumerable<Tile> query = from t in GameValues.grid.Cast<Tile>() where t.rectangle.X == selectedTile.rectangle.X && t.rectangle.Y == selectedTile.rectangle.Y && t.layer > selectedTile.layer && t.type != Type.ceiling select t;
            foreach(Tile t in query)
            {
                return true;
            }
            return false;
        }
    }
}
