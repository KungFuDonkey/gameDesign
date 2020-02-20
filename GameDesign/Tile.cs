using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace rollercoaster_tycoon_ripoff
{
    public enum Zone
    {
        Lesson,
        Break,
        Path,
        Road,
        Grass
    }
    public class Tile
    {
        public Rectangle rectangle;
        public Color standardColor;
        public Color color;
        public int layer;
        public bool moveable;
        public Zone zone;
        public bool occupied;
        public void Update(MouseState mouse)
        {
            if (rectangle.Contains(mouse.Position))
            {
                color = Color.Blue;
                if(mouse.LeftButton == ButtonState.Pressed)
                {
                    TileChange.setWall(this);
                }
                Game1.SelectedTile = this;
            }
            else
            {
                color = standardColor;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameValues.tileTex, rectangle, color);
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle drawRectangle)
        {
            spriteBatch.Draw(GameValues.tileTex, drawRectangle, color);
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle drawRectangle, float alpha)
        {
            spriteBatch.Draw(GameValues.tileTex, drawRectangle, color * alpha);
        }
    }
}
