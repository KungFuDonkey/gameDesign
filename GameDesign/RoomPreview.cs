using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
namespace rollercoaster_tycoon_ripoff
{
    class RoomPreview
    {
        public Room room = new BreakRoom(0);
        public Rectangle drawRectangle = new Rectangle(0,0,GameValues.tileSize,GameValues.tileSize);
        public float alpha = 0.8f;
        public int direction = -1;
        public void Draw(SpriteBatch spriteBatch, Rectangle selectedRectangle)
        {
            foreach(Tile t in room.layout)
            {
                drawRectangle.Location = selectedRectangle.Location + t.rectangle.Location;
                t.Draw(spriteBatch, drawRectangle, alpha);
            }
            float move = direction * 0.01f;
            if(alpha + move < 0.1f || alpha + move > 0.8f)
            {
                direction *= -1;
                move = direction * 0.01f;
            }
            alpha += move;
        }

    }
}
