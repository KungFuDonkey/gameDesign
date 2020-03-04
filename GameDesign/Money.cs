using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesign
{
    public class Money
    {
        public float Cash { get; private set; }
        public Money(float money)
        {
            Cash = money;
        }

        public void Update(KeyboardState currKeyboardState, KeyboardState prevKeyboardState)
        {
            if (currKeyboardState.IsKeyDown(Keys.K) && !prevKeyboardState.IsKeyDown(Keys.K))
            {
                earnCash(10000);
            }
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
