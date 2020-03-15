using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameDesign
{
    class RoomPreview
    {
        public List<Room> rooms = new List<Room>();
        public Room room;
        public Rectangle drawRectangle = new Rectangle(0, 0, GameValues.tileSize, GameValues.tileSize);
        public float alpha = 0.8f, buildCosts;
        public int direction = -1;
        public int roomIndex = 0;
        string path;

        //initialize runs on the initialization and creates the different rooms which are read from the text files in rooms/text
        public void initialize()
        {
            string directory = Directory.GetCurrentDirectory();
            directory = directory.Remove(directory.Length - 22);
            path = Path.Combine(directory, "Rooms//amount.txt");
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

        //draws the current room to the screen over the grid
        public void Draw(SpriteBatch spriteBatch, Rectangle selectedRectangle)
        {
            foreach(Tile t in room.layout)
            {
                drawRectangle.Location = selectedRectangle.Location + t.rectangle.Location - room.middle;
                drawRectangle.Size = new Point(GameValues.tileSize, GameValues.tileSize);
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

        //updates the input of the player leftbutton = build, E and Q = switching between rooms, rightbutton = rotation
        public void Update(KeyboardState keyBoardState, KeyboardState prevKeyboardState, MouseState mouseState, MouseState prevMouseState, Rectangle selectedRectangle)
        {
            room.setValues(rooms[roomIndex].path);
            for (int i = 0; i < room.rotation; i++)
            {
                room.rotate();
            }
            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && !collision(selectedRectangle) && Game1.money.canBuy(buildCosts))
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
            }
            if((mouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton != ButtonState.Pressed) || 
                (mouseState.MiddleButton == ButtonState.Pressed && prevMouseState.MiddleButton != ButtonState.Pressed) ||
                (keyBoardState.IsKeyDown(Keys.R) && !prevKeyboardState.IsKeyDown(Keys.R)))
            {
                room.rotation = (room.rotation + 1) % 4;
            }
            buildCosts = room.walls * GameValues.wallCost + room.floors * GameValues.floorCost;
        }

        //checks for a collision with the grid to decide if the room can be placed
        public bool collision(Rectangle selectedRectangle)
        {
            foreach (Tile t in room.layout)
            {
                drawRectangle.Location = selectedRectangle.Location + t.rectangle.Location - room.middle;
                try
                {
                    Tile oldtile = (from tile in GameValues.tiles where tile.rectangle.X == drawRectangle.X && tile.rectangle.Y == drawRectangle.Y && tile.place == Game1.cam.place select tile).First();
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

        //builds the room onto the grid
        public void build(Rectangle selectedRectangle)
        {
            if (!room.part)
            {
                room.place = Game1.cam.place;
            }
            foreach (Tile t in room.layout)
            {
                drawRectangle.Location = selectedRectangle.Location + t.rectangle.Location - room.middle;
                Rectangle rectangle = new Rectangle(drawRectangle.X, drawRectangle.Y, GameValues.tileSize, GameValues.tileSize);
                try
                {
                    Tile oldtile = (from tile in GameValues.tiles where tile.rectangle.X == drawRectangle.X && tile.rectangle.Y == drawRectangle.Y && tile.place == room.place select tile).First();
                    TileChange.setColorTile(oldtile,t.color,t.zone);
                }
                catch
                {
                    GameValues.tiles.Add(new ColorTile(rectangle, room.place, t.color, room.zone));
                }
            }
            Game1.money.payCash(buildCosts);
        }
    }
}
