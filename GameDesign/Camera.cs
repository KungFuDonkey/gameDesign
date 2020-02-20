using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace rollercoaster_tycoon_ripoff
{
    class Camera
    {
        Keys[] keys = new Keys[6] { Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.W, Keys.S };
        public int layer = 0;
        public int movespeed = 5;
        public void Update(KeyboardState keyboardState, KeyboardState prevKeyBoardState)
        {
            int move = 1;
            for(int i = 0; i < 2; i++)
            {
                if (keyboardState.IsKeyDown(keys[i]))
                {
                    for(int j = 0; j < GameValues.gridSize; j++)
                    {
                        GameValues.tiles[j].rectangle.Y += move * movespeed;
                    }
                }
                if (keyboardState.IsKeyDown(keys[i + 2]))
                {
                    for (int j = 0; j < GameValues.gridSize; j++)
                    {
                        GameValues.tiles[j].rectangle.X += move * movespeed;
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
