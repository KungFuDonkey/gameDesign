using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace GameDesign
{
    public class Camera
    {
        Keys[] keys = new Keys[6] { Keys.W, Keys.S, Keys.A, Keys.D, Keys.Up, Keys.Down };
        public int layer = 0;
        public int movespeed = 5;
        public void Update(KeyboardState keyboardState, KeyboardState prevKeyBoardState)
        {
            int move = 1;
            for(int i = 0; i < 2; i++)
            {
                if (keyboardState.IsKeyDown(keys[i]))
                {
                    foreach (Tile t in GameValues.tiles)
                    {
                        t.rectangle.Y += move * movespeed;
                    }
                }
                if (keyboardState.IsKeyDown(keys[i + 2]))
                {
                    foreach(Tile t in GameValues.tiles)
                    {
                        t.rectangle.X += move * movespeed;
                    }
                }
                if (keyboardState.IsKeyDown(keys[i + 4]) && !prevKeyBoardState.IsKeyDown(keys[i+4]))
                {
                    layer = layer + move > GameValues.maxHeight || layer + move < 0 ? layer : layer + move;
                }
                move *= -1;
            }
        }
    }
}
