using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
                drawRectangle.Location = selectedRectangle.Location + t.rectangle.Location - room.middle;
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
        public void Update(MouseState mouseState, MouseState prevMouseState, Rectangle selectedRectangle)
        {
            if(mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && !collision(selectedRectangle))
            {
                build(selectedRectangle);
            }
        }
        public bool collision(Rectangle selectedRectangle)
        {
            foreach (Tile t in room.layout)
            {
                drawRectangle.Location = selectedRectangle.Location + t.rectangle.Location - room.middle;
                try
                {
                    Tile oldtile = (from tile in GameValues.tiles where tile.rectangle.X == drawRectangle.X && tile.rectangle.Y == drawRectangle.Y select tile).First();
                    if (oldtile.occupied)
                    {
                        return true;
                    }
                }
                catch
                {
                    return true;
                }
            }
            return false;
        }
        public void build(Rectangle selectedRectangle)
        {
            foreach (Tile t in room.layout)
            {
                drawRectangle.Location = selectedRectangle.Location + t.rectangle.Location - room.middle;
                try
                {
                    Tile oldtile = (from tile in GameValues.tiles where tile.rectangle.X == drawRectangle.X && tile.rectangle.Y == drawRectangle.Y select tile).First();
                    switch (t.type)
                    {
                        case Type.wall:
                            TileChange.setWall(oldtile);
                            break;
                        case Type.floor:
                            TileChange.setFloor(oldtile);
                            break;
                        case Type.grass:
                            break;
                    }
                }
                catch
                {
                    Rectangle rectangle = new Rectangle(drawRectangle.X,drawRectangle.Y, GameValues.tileSize, GameValues.tileSize);//needs fix
                    switch (t.type)
                    {
                        case Type.wall:
                            GameValues.tiles.Add(new Wall(rectangle, room.layer, room.zone));
                            break;
                        case Type.floor:
                            GameValues.tiles.Add(new Floor(rectangle, room.layer, room.zone));
                            break;
                        case Type.grass:
                            break;
                    }
                }
            }
        }
    }
}
