using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace GameDesign
{
    class Selectors
    {
        protected Tile firstSelection, secondSelection;
        protected Rectangle drawRectangle = new Rectangle(0, 0, 0, 0);
        protected Color color;
        protected float alpha = 0.8f;
        protected int direction = -1;
        protected int minx, maxx, miny, maxy;
        public virtual void Update(MouseState mouseState, MouseState prevMouseState, Tile selectedTile)
        {
            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                firstSelection = selectedTile;
            }
            else if (mouseState.LeftButton == ButtonState.Pressed)
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
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(GameValues.tileTex, drawRectangle, color * alpha);
            alpha += (float)gameTime.ElapsedGameTime.TotalSeconds * direction;
            if (alpha < 0.2)
            {
                direction *= -1;
                alpha = 0.2f;
            }
            else if (alpha > 0.8f)
            {
                direction *= -1;
                alpha = 0.8f;
            }
        }
    }
}
