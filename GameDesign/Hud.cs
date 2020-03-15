﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace GameDesign
{
    class Hud
    {
        Rectangle verticalRectangle, horizontalRectangle, cornerRectangle, layerRectangle, indicatorRectangle, bottomRectangle;
        Rectangle buildRectangle, selectRectangle, removeRectangle, zoneRectangle;
        Rectangle[] zoneRectangles = new Rectangle[GameValues.zoneColors.Count()];
        Rectangle otherBuildState;
        Rectangle[] allTiles = new Rectangle[GameValues.tileColors.Count()];
        float timer = 0.01f, TIMER = 0.01f;
        Vector2 drawpoint, DRAWPOINT;
        public Hud(int screenwidth, int screenheight)
        {
            horizontalRectangle = new Rectangle(0, 0, screenwidth, 50);
            verticalRectangle = new Rectangle(-30, 0, 50, screenheight);
            layerRectangle = new Rectangle(screenwidth - 50, 50, 50, screenheight - 50);
            cornerRectangle = new Rectangle(-30, 0, 50, 50);
            bottomRectangle = new Rectangle(0, screenheight - 20, screenwidth, 50);
            buildRectangle = new Rectangle(-30, 100, 50, 50);
            selectRectangle = new Rectangle(-30, 200, 50, 50);
            removeRectangle = new Rectangle(-30, 300, 50, 50);
            zoneRectangle = new Rectangle(-30, 400, 50, 50);
            otherBuildState = new Rectangle(100, screenheight - 17, 50, 50);
            int x = 80;
            foreach(Zone zone in Enum.GetValues(typeof(Zone))){
                zoneRectangles[(int)zone] = new Rectangle(x, screenheight - 17, 50, 50);
                x += 100;
            }
            for(int i = 0; i < GameValues.tileColors.Count(); ++i)
            {
                allTiles[i] = new Rectangle(100 * i + 200, screenheight - 17, 50, 50);
            }
            indicatorRectangle = new Rectangle(screenwidth - 44, 0, 41, 41);
            drawpoint = new Vector2(screenwidth - 29, screenheight - 60);
            DRAWPOINT = new Vector2(screenwidth - 29, screenheight - 60);
        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameValues.tileTex, bottomRectangle, Color.White);
            spriteBatch.Draw(GameValues.tileTex, horizontalRectangle, Color.Blue);
            spriteBatch.Draw(GameValues.tileTex, verticalRectangle, Color.LightBlue);
            spriteBatch.Draw(GameValues.tileTex, cornerRectangle, Color.Gray);
            spriteBatch.Draw(GameValues.hammer, buildRectangle, Color.White);
            spriteBatch.Draw(GameValues.tileTex, selectRectangle, Color.Pink);
            spriteBatch.Draw(GameValues.remover, removeRectangle, Color.White);
            spriteBatch.Draw(GameValues.colorplate, zoneRectangle, Color.White);
            spriteBatch.Draw(GameValues.tileTex, layerRectangle, Color.AliceBlue);
            switch (GameValues.state)
            {
                case GameState.zone:
                    for (int i = 0; i < zoneRectangles.Count(); ++i)
                    {
                        spriteBatch.Draw(GameValues.colorSpetter, zoneRectangles[i], GameValues.zoneColors[i]);
                    }
                    break;
                case GameState.build:
                    spriteBatch.Draw(GameValues.tileTex, otherBuildState, Color.Black);
                    if(GameValues.buildState == BuildState.singleTile)
                    {
                        for(int i = 0; i < GameValues.tileColors.Count(); ++i)
                        {
                            spriteBatch.Draw(GameValues.tileTex, allTiles[i], GameValues.tileColors[i]);
                        }
                    }
                    break;
            }
            for(int i = 0; i < 20; i++)
            {
                if (i == Game1.cam.place)
                {
                    indicatorRectangle.Y = (int)drawpoint.Y - 10;
                    spriteBatch.Draw(GameValues.tileTex, indicatorRectangle, Color.Blue);
                }
                spriteBatch.DrawString(GameValues.font, i.ToString(), drawpoint, Color.Black);
                if(i == 9)
                {
                    drawpoint.X -= 6;
                }
                drawpoint.Y -= 40;
            }
            drawpoint.X = DRAWPOINT.X;
            drawpoint.Y = DRAWPOINT.Y;
        }
        public bool Update(MouseState mouseState, MouseState prevMouseState, GameTime gameTime)
        {
            checkRectangle(buildRectangle, mouseState, prevMouseState, GameState.build);
            checkRectangle(selectRectangle, mouseState, prevMouseState, GameState.select);
            checkRectangle(removeRectangle, mouseState, prevMouseState, GameState.remove);
            checkRectangle(zoneRectangle, mouseState, prevMouseState, GameState.zone);
            switch (GameValues.state)
            {
                case GameState.zone:
                    for (int i = 0; i < zoneRectangles.Count(); ++i)
                    {
                        checkZone(zoneRectangles[i], mouseState, prevMouseState, (Zone)i);
                    }
                    break;
                case GameState.build:
                    checkBuild(otherBuildState, mouseState, prevMouseState);
                    if(GameValues.buildState == BuildState.singleTile)
                    {
                        for(int i = 0; i < GameValues.tileColors.Count(); i++)
                        {
                            checkTile(allTiles[i], mouseState, prevMouseState, (BuildTiles)i);
                        }
                    }
                    break;

            }
            if (verticalRectangle.Contains(mouseState.Position) || bottomRectangle.Contains(mouseState.Position)) 
            {
                if(verticalRectangle.X < 0)
                {
                    slide(true, gameTime);
                }
                return true;
            }
            else if(verticalRectangle.X > -30)
            {
                slide(false, gameTime);
            }
            return false;
        }
        public void checkRectangle(Rectangle rectangle, MouseState mouseState, MouseState prevMouseState, GameState state)
        { 
            if (rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                GameValues.state = state;
            }
        }
        public void checkZone(Rectangle rectangle, MouseState mouseState, MouseState prevMouseState, Zone zone)
        {
            if (rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                GameValues.selectedZone = zone;
                GameValues.selectedBuildingType = GameValues.buildingTypes[(int)zone + 1];
            }
        }
        public void checkBuild(Rectangle rectangle, MouseState mouseState, MouseState prevMouseState)
        {
            if (rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                GameValues.buildState = GameValues.buildState == BuildState.room ? BuildState.singleTile : BuildState.room;
            }
        }
        public void checkTile(Rectangle rectangle, MouseState mouseState, MouseState prevMouseState, BuildTiles tile)
        {
            if (rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                GameValues.selectedTile = tile;
            }
        }
        public void slide(bool outward, GameTime gameTime)
        {
            if (outward && timer < 0){
                move(1);
                timer = TIMER;
            }
            else if(timer < 0)
            {
                move(-1);
                timer = TIMER;
            }
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void move(int direction)
        {
            verticalRectangle.X += direction;
            cornerRectangle.X += direction;
            buildRectangle.X += direction;
            selectRectangle.X += direction;
            removeRectangle.X += direction;
            zoneRectangle.X += direction;
            bottomRectangle.Y -= direction;
            otherBuildState.Y -= direction;
            for (int i = 0; i < zoneRectangles.Count(); ++i)
            {
                zoneRectangles[i].Y -= direction;
            }
            for (int i = 0; i < allTiles.Count(); ++i)
            {
                allTiles[i].Y -= direction;
            }
        }
    }
}
