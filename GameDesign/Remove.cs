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
    class Remove
    {
        Tile firstSelection, secondSelection;
        Rectangle drawRectangle = new Rectangle(0,0,0,0);
        Color color = new Color(255, 0, 0);
        float alpha = 1;
        int direction = -1;
        int minx, maxx, miny, maxy;
        public void Update(MouseState mouseState, MouseState prevMouseState, Tile selectedTile)
        {
            if(mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                firstSelection = selectedTile;
            }
            else if(mouseState.LeftButton == ButtonState.Pressed)
            {
                secondSelection = selectedTile;
                if (firstSelection.rectangle.X < secondSelection.rectangle.X)
                {
                    minx = firstSelection.rectangle.X;
                    maxx = secondSelection.rectangle.X + GameValues.tileSize;
                }
                else
                {
                    minx = secondSelection.rectangle.X;
                    maxx = firstSelection.rectangle.X + GameValues.tileSize;
                }
                if (firstSelection.rectangle.Y < secondSelection.rectangle.Y)
                {
                    miny = firstSelection.rectangle.Y;
                    maxy = secondSelection.rectangle.Y + GameValues.tileSize;
                }
                else
                {
                    miny = secondSelection.rectangle.Y;
                    maxy = firstSelection.rectangle.Y + GameValues.tileSize;
                }
                drawRectangle.X = minx;
                drawRectangle.Y = miny;
                drawRectangle.Width = maxx - minx;
                drawRectangle.Height = maxy - miny;
            }
            else if (mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed)
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
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(GameValues.tileTex, drawRectangle, color * alpha);
            alpha += (float)gameTime.ElapsedGameTime.TotalSeconds * direction;
            if(alpha < 0.2)
            {
                direction *= -1;
                alpha = 0.2f;
            }
            else if(alpha > 1)
            {
                direction *= -1;
                alpha = 1;
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
