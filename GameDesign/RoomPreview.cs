using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace GameDesign
{
    class RoomPreview
    {
        public List<Room> rooms = new List<Room>();
        public Room room;
        public Rectangle drawRectangle = new Rectangle(0,0,GameValues.tileSize,GameValues.tileSize);
        public float alpha = 0.8f;
        public int direction = -1;
        public int roomIndex = 0;
        public void initialize()
        {
            string directory = Directory.GetCurrentDirectory();
            directory = directory.Remove(directory.Length - 22);
            string path = Path.Combine(directory, "Rooms//amount.txt");
            int amount = int.Parse(File.ReadAllLines(path)[0]);
            path = Path.Combine(directory, "Rooms//noRoom.txt");
            rooms.Add(new Room(path));
            for (int i = 0; i < amount; i++)
            {
                try
                {
                    path = Path.Combine(directory, "Rooms//room" + i.ToString() + ".txt");
                    rooms.Add(new Room(path));
                }
                catch
                {

                }
            }
            room = rooms[roomIndex];
        }
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
        public void Update(KeyboardState keyBoardState, KeyboardState prevKeyboardState, MouseState mouseState, MouseState prevMouseState, Rectangle selectedRectangle)
        {
            if(mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && !collision(selectedRectangle))
            {
                build(selectedRectangle);
            }
            if(keyBoardState.IsKeyDown(Keys.E) && !prevKeyboardState.IsKeyDown(Keys.E))
            {
                roomIndex = roomIndex == rooms.Count - 1 ? roomIndex : roomIndex + 1;
                room = rooms[roomIndex];
            }
            if(keyBoardState.IsKeyDown(Keys.Q) && !prevKeyboardState.IsKeyDown(Keys.Q))
            {
                roomIndex = roomIndex == 0 ? 0 : roomIndex - 1;
                room = rooms[roomIndex];
                Debug.WriteLine(roomIndex);
            }
        }
        public bool collision(Rectangle selectedRectangle)
        {
            foreach (Tile t in room.layout)
            {
                drawRectangle.Location = selectedRectangle.Location + t.rectangle.Location - room.middle;
                try
                {
                    Tile oldtile = (from tile in GameValues.tiles where tile.rectangle.X == drawRectangle.X && tile.rectangle.Y == drawRectangle.Y && tile.layer == Game1.cam.layer select tile).First();
                    if (oldtile.occupied && !(t.type == Type.wall && oldtile.type == Type.wall))
                    {
                        Debug.WriteLine("already occupied");
                        Debug.WriteLine(oldtile.type == Type.wall);
                        Debug.WriteLine(t.type == Type.wall);
                        Debug.WriteLine(oldtile.occupied);
                        return true;
                    }
                }
                catch
                {
                    Debug.WriteLine("no terrain");
                    return true;
                }
            }
            return false;
        }
        public void build(Rectangle selectedRectangle)
        {
            if (!room.part)
            {
                room.layer = Game1.cam.layer;
            }
            foreach (Tile t in room.layout)
            {
                drawRectangle.Location = selectedRectangle.Location + t.rectangle.Location - room.middle;
                Rectangle rectangle = new Rectangle(drawRectangle.X, drawRectangle.Y, GameValues.tileSize, GameValues.tileSize);
                try
                {
                    Tile oldtile = (from tile in GameValues.tiles where tile.rectangle.X == drawRectangle.X && tile.rectangle.Y == drawRectangle.Y && tile.layer == room.layer select tile).First();
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
                if(t.type == Type.wall)
                {
                    try
                    {
                        Tile oldtile = (from tile in GameValues.tiles where tile.rectangle.X == drawRectangle.X && tile.rectangle.Y == drawRectangle.Y && tile.layer == room.layer + 1 select tile).First();
                        continue;
                    }
                    catch
                    {

                    }
                }
                GameValues.tiles.Add(new Ceiling(rectangle, room.layer + 1, room.zone));
            }
        }
    }
}
