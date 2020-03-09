using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace GameDesign
{
    public enum Zone
    {
        Lesson,
        Break,
        Path,
        Road,
        Grass
    }
    public enum Type
    {
        grass,
        wall,
        floor,
        ceiling
    }
    public class Tile
    {
        public Type type;
        public Rectangle rectangle;
        public Color standardColor;
        public Color color;
        public int layer;
        public bool moveable;
        public Zone zone;
        public bool occupied;
        public Point gridLocation;

        public void Update(MouseState mouse)
        {
            if (rectangle.Contains(mouse.Position))
            {
                color = Color.Blue;
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
