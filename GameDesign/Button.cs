using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesign
{
    public class Button
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public bool active, clicked;
        public Color drawColor = Color.White;
        public string text;

        public Button(Rectangle rectangle, Texture2D texture, string text)
        {
            this.rectangle = rectangle;
            this.texture = texture;
            this.text = text;
        }

        public void Update(MouseState currMouseState, MouseState prevMouseState)
        {
            if (pressed(currMouseState))
            {
                drawColor = Color.DarkGray;
            }
            else if (hover(currMouseState))
            {
                drawColor = Color.LightGray;
            }
            else
            {
                drawColor = Color.White;
            }
            clicked = click(currMouseState, prevMouseState);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, drawColor);
            spriteBatch.DrawString(Game1.menu.menuFont, text, rectangle.Center.ToVector2() - Game1.menu.menuFont.MeasureString(text) / 2, Color.White);
        }

        public bool hover(MouseState mouseState)
        {
            return rectangle.Contains(mouseState.Position);
        }

        public bool pressed(MouseState mouseState)
        {
            return hover(mouseState) && mouseState.LeftButton == ButtonState.Pressed;
        }

        public bool click(MouseState currMouseState, MouseState prevMouseState)
        {
            return hover(currMouseState) && currMouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed && active;
        }
    }
}
