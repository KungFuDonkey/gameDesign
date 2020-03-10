using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace GameDesign
{
    public class Money
    {
        public float Cash { get; private set; } //only whole numbers allowed
        Rectangle moneyRectangle, euroRectangle;
        Color moneyColor;
        int offSet;
        Texture2D euroSign;

        public Money(float money)
        {
            Cash = money;
            moneyRectangle = new Rectangle(60, 10, 150, 30);
            euroRectangle = new Rectangle(moneyRectangle.Location, new Point(30,30));
        }

        public void LoadContent(ContentManager Content)
        {
            euroSign = Content.Load<Texture2D>("euro-symbol");
        }

        public void Update(KeyboardState currKeyboardState, KeyboardState prevKeyboardState)
        {
            if (currKeyboardState.IsKeyDown(Keys.K) && !prevKeyboardState.IsKeyDown(Keys.K))
            {
                earnCash(10000);
            }
            if (currKeyboardState.IsKeyDown(Keys.J) && !prevKeyboardState.IsKeyDown(Keys.J))
            {
                payCash(10000);
            }

            offSet = Cash.ToString().Length * 11;
            if (Cash < 0)
            {
                moneyColor = Color.Red;
                offSet -= 5;
            }
            else
            {
                moneyColor = Color.Black;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameValues.tileTex, moneyRectangle, Color.White);
            spriteBatch.Draw(euroSign, euroRectangle, Color.White);
            spriteBatch.DrawString(Game1.font, Cash.ToString(), new Vector2(moneyRectangle.Right - Game1.font.MeasureString(Cash.ToString()).X, moneyRectangle.Center.Y - Game1.font.MeasureString(Cash.ToString()).Y / 2) , moneyColor);
        }

        public void earnCash(float amount)
        {
            Cash += amount;
        }

        public void buyObject(float amount)
        {
            if (canBuy(amount))
            {
                Cash -= amount;
            }
        }

        public bool canBuy(float cost)
        {
            if (Cash - cost >= 0f)
            {
                return true;
            }
            return false;
        }

        public void payCash(float amount)
        {
            Cash -= amount;
        }
    }
}
