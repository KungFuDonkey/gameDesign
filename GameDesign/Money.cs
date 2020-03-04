using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesign
{
    class Money
    {
        public float Cash { get; private set; }

        public void Update(KeyboardState currKeyboardState, KeyboardState prevKeyboardState)
        {
            if (currKeyboardState.IsKeyDown(Keys.K) && prevKeyboardState.IsKeyDown(Keys.K))
            {
                earnCash(1000);
            }
        }

        public void earnCash(float amount)
        {
            Cash += amount;
        }

        public void buyObject(float cost)
        {
            if (canBuy(cost))
            {
                Cash -= cost;
            }
        }

        bool canBuy(float cost)
        {
            if (Cash - cost >= 0f)
            {
                return true;
            }
            return false;
        }

        public void payCash(float amount)
        {

        }
    }
}
