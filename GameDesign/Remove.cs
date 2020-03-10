using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace GameDesign
{
    class Remove
    {
        Tile firstSelection, secondSelection;
        public void Update(MouseState mouseState, MouseState prevMouseState, Tile selectedTile)
        {
            if(mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                firstSelection = selectedTile;
                Debug.WriteLine("first");
            }
            else if (mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed)
            {
                secondSelection = selectedTile;
                Debug.WriteLine("second");
                IEnumerable<Tile> query = from t in GameValues.tiles where t.rectangle.X >= firstSelection.rectangle.X && t.rectangle.X <= secondSelection.rectangle.X && t.rectangle.Y >= firstSelection.rectangle.Y && t.rectangle.Y <= secondSelection.rectangle.Y && t.layer == firstSelection.layer && t.layer == secondSelection.layer select t;
                foreach(Tile t in query)
                {
                    if (hasCeiling(t))
                    {
                        return;
                    }
                }
                Debug.WriteLine("ok");
                for(int i = 0; i<query.Count(); i++)
                {
                    Tile t = query.ElementAt(i);
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
                    catch (Exception)
                    {
                    }
                }
            }
        }
        public bool hasCeiling(Tile selectedTile)
        {
            IEnumerable<Tile> query = from t in GameValues.tiles where t.rectangle.X == selectedTile.rectangle.X && t.rectangle.Y == selectedTile.rectangle.Y && t.layer > selectedTile.layer && t.type != Type.ceiling select t;
            foreach(Tile t in query)
            {
                Debug.WriteLine("something else then a ceiling");
                return true;
            }
            return false;
        }
    }
}
