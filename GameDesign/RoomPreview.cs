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
    public class RoomPreview
    {
        public List<Room> rooms = new List<Room>();
        public Room room;
        public Rectangle drawRectangle = new Rectangle(new Point(), new Point(GameValues.tileSize));
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
                    newRoom(path);
                }
                catch
                {

                }
            }
           
        }
        //creates a new room in the rooms list
        public void newRoom(string _path)
        {
            rooms.Add(new Room(_path));
            roomIndex = 0;
            room = rooms[roomIndex];
        }
        //draws the current room to the screen over the grid
        public void Draw(SpriteBatch spriteBatch, Rectangle selectedRectangle)
        {
            foreach(Tile t in room.layout)
            {
                drawRectangle.Location = selectedRectangle.Location + t.rectangle.Location - room.middle;
                drawRectangle.Size = new Point(GameValues.tileSize);
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
            room.setValues(room.path);
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
                    Tile oldtile = GameValues.grid[(drawRectangle.X - GameValues.grid[0, 0, Game1.cam.place].rectangle.X) / GameValues.tileSize, (drawRectangle.Y - GameValues.grid[0, 0, Game1.cam.place].rectangle.Y) / GameValues.tileSize, Game1.cam.place];
                    if (oldtile.occupied && !(t.type == Type.building && oldtile.type == Type.building))
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
                Rectangle rectangle = new Rectangle(new Point(drawRectangle.X, drawRectangle.Y), new Point(GameValues.tileSize));
                Point gridPos = new Point((drawRectangle.X - GameValues.grid[0, 0, Game1.cam.place].rectangle.X) / GameValues.tileSize, (drawRectangle.Y - GameValues.grid[0, 0, Game1.cam.place].rectangle.Y) / GameValues.tileSize);
                try
                {
                    Tile oldtile = GameValues.grid[gridPos.X, gridPos.Y, Game1.cam.place];
                    if (t.enterance)
                    {
                        TileChange.setEnterance(oldtile, t.zone);
                    }
                    else
                    {
                        TileChange.setColorTile(oldtile, t.color, t.zone);
                    }
                }
                catch
                {
                    GameValues.grid[gridPos.X, gridPos.Y, Game1.cam.place] = new ColorTile(rectangle, room.place, t.color, room.zone);
                }
            }
            Game1.money.payCash(buildCosts);
        }
    }
}
