﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public int place = 0;
        public int movespeed = 5;
        int zoomSpeed = 1;
        public bool moving, zooming;

        public void Update(KeyboardState keyboardState, KeyboardState prevKeyBoardState, MouseState currMouseState, MouseState prevMouseState, Tile[,,] grid)
        {
            zooming = false;
            moving = false;
            movespeed = 5 + GameValues.tileSize / 5;
            int move = 1;
            for(int i = 0; i < 2; i++)
            {
                if (keyboardState.IsKeyDown(keys[i]))
                {
                    moving = true;
                    foreach (Tile t in grid)
                    {
                        t.rectangle.Y += move * movespeed;
                    }
                }
                if (keyboardState.IsKeyDown(keys[i + 2]))
                {
                    moving = true;
                    foreach (Tile t in grid)
                    {
                        t.rectangle.X += move * movespeed;
                    }
                }
                if (keyboardState.IsKeyDown(keys[i + 4]) && !prevKeyBoardState.IsKeyDown(keys[i+4]))
                {
                    moving = true;
                    place = place + move > GameValues.currentMaxPlace - 1 || place + move < 0 ? place : place + move;
                }
                move *= -1;
            }
            if (currMouseState.ScrollWheelValue != prevMouseState.ScrollWheelValue)
            {
                moving = true;
                zooming = true;
                int tileSize = GameValues.tileSize;
                if (tileSize > 15)
                {
                    zoomSpeed = 2;
                    movespeed = 10;
                }
                else
                {
                    zoomSpeed = 1;
                    movespeed = 5;
                }

                Point selectedTile = Game1.SelectedTile.rectangle.Location;
                if (currMouseState.ScrollWheelValue > prevMouseState.ScrollWheelValue && tileSize <= 25)
                {
                    tileSize += zoomSpeed;
                    foreach (Tile t in grid)
                    {
                        Point location = t.rectangle.Location;
                        int distX = (location.X - selectedTile.X) / (tileSize - zoomSpeed) * tileSize + selectedTile.X;
                        int distY = (location.Y - selectedTile.Y) / (tileSize - zoomSpeed) * tileSize + selectedTile.Y;
                        t.rectangle.X = distX;
                        t.rectangle.Y = distY;
                        t.rectangle.Width = tileSize;
                        t.rectangle.Height = tileSize;
                    }
                }
                else if (currMouseState.ScrollWheelValue < prevMouseState.ScrollWheelValue && tileSize > 3)
                {
                    tileSize -= zoomSpeed;
                    foreach (Tile t in grid)
                    {
                        Point location = t.rectangle.Location;
                        int distX = (location.X - selectedTile.X) / (tileSize + zoomSpeed) * tileSize + selectedTile.X;
                        int distY = (location.Y - selectedTile.Y) / (tileSize + zoomSpeed) * tileSize + selectedTile.Y;
                        t.rectangle.X = distX;
                        t.rectangle.Y = distY;
                        t.rectangle.Width = tileSize;
                        t.rectangle.Height = tileSize;
                    }
                }
                GameValues.tileSize = tileSize;
            }
        }
    }
}
