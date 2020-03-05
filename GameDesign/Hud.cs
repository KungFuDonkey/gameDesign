﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace GameDesign
{
    class Hud
    {
        Rectangle verticalRectangle, horizontalRectangle, cornerRectangle;
        Rectangle buildRectangle, selectRectangle, removeRectangle;
        float timer = 0.01f, TIMER = 0.01f;
        public Hud(int screenwidth, int screenheight)
        {
            horizontalRectangle = new Rectangle(0, 0, screenwidth, 50);
            verticalRectangle = new Rectangle(-30, 0, 50, screenheight);
            cornerRectangle = new Rectangle(-30, 0, 50, 50);
            buildRectangle = new Rectangle(-30, 100, 50, 50);
            selectRectangle = new Rectangle(-30, 200, 50, 50);
            removeRectangle = new Rectangle(-30, 300, 50, 50);
        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameValues.tileTex, horizontalRectangle, Color.Blue);
            spriteBatch.Draw(GameValues.tileTex, verticalRectangle, Color.LightBlue);
            spriteBatch.Draw(GameValues.tileTex, cornerRectangle, Color.Gray);
            spriteBatch.Draw(GameValues.tileTex, buildRectangle, Color.Black);
            spriteBatch.Draw(GameValues.tileTex, selectRectangle, Color.Pink);
            spriteBatch.Draw(GameValues.tileTex, removeRectangle, Color.Red);
        }
        public bool Update(MouseState mouseState, MouseState prevMouseState, GameTime gameTime)
        {
            checkRectangle(buildRectangle, mouseState, prevMouseState, GameState.build);
            checkRectangle(selectRectangle, mouseState, prevMouseState, GameState.select);
            checkRectangle(removeRectangle, mouseState, prevMouseState, GameState.remove);
            if (verticalRectangle.Contains(mouseState.Position))
            {
                if(verticalRectangle.X < 0)
                {
                    slide(true, gameTime);
                }
                return true;
            }
            else if(verticalRectangle.X > -30)
            {
                slide(false, gameTime);
            }
            return false;
        }
        public void checkRectangle(Rectangle rectangle, MouseState mouseState, MouseState prevMouseState, GameState state)
        { 
            if (rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                GameValues.state = state;
                Debug.WriteLine(state);
            }
        }
        public void slide(bool outward, GameTime gameTime)
        {
            if (outward && timer < 0){
                move(1);
                timer = TIMER;
            }
            else if(timer < 0)
            {
                move(-1);
                timer = TIMER;
            }
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void move(int direction)
        {
            verticalRectangle.X += direction;
            cornerRectangle.X += direction;
            buildRectangle.X += direction;
            selectRectangle.X += direction;
            removeRectangle.X += direction;
        }
    }
}
