using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesign
{
    public class OptionsMenu
    {
        Button applyButton, CancelButton;

        public void Update()
        {
            if (applyButton.clicked)
            {
                Game1.menu.newMenuState = Game1.menu.prevMenuState;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
