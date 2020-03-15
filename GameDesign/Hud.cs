using System;
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
        Rectangle buildRectangle, selectRectangle, removeRectangle, zoneRectangle, plusRectangle, plusXRectangle, plusYRectangle, minXRectangle, minYRectangle, plusTypeRectangle, minTypeRectangle, makeGridRectangle;
        Rectangle[] zoneRectangles = new Rectangle[GameValues.zoneColors.Count()];
        Rectangle otherBuildState, addBuild;
        Rectangle[] allTiles = new Rectangle[GameValues.tileColors.Count()];
        float timer = 0.01f, TIMER = 0.01f;
        Vector2 drawpoint, DRAWPOINT;
        int newGridX, newGridY, newBuildingType;

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
            plusRectangle = new Rectangle(-30, 500, 50, 50);
            plusXRectangle = new Rectangle(300, 500, 50, 50);
            plusYRectangle = new Rectangle(300, 600, 50, 50);
            minXRectangle = new Rectangle(150, 500, 50, 50);
            minYRectangle = new Rectangle(150, 600, 50, 50);
            plusTypeRectangle = new Rectangle(300, 700, 50, 50);
            minTypeRectangle = new Rectangle(150, 700, 50, 50);
            makeGridRectangle = new Rectangle(190, 800, 120, 50);
            otherBuildState = new Rectangle(100, screenheight - 17, 50, 50);
            addBuild = new Rectangle(200, screenheight - 17, 50, 50);
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
            spriteBatch.Draw(GameValues.plus, plusRectangle, Color.Black);
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
                    else
                    {
                        spriteBatch.Draw(GameValues.tileTex, addBuild, Color.White);
                    }
                    break;
                case GameState.plus:
                    spriteBatch.Draw(GameValues.popUp, new Rectangle(50, Game1.viewport.Y - 450, 400, 400), Color.White);
                    spriteBatch.Draw(GameValues.plusSign, plusXRectangle, Color.White);
                    spriteBatch.Draw(GameValues.minSign, minXRectangle, Color.White);
                    spriteBatch.Draw(GameValues.plusSign, plusYRectangle, Color.White);
                    spriteBatch.Draw(GameValues.minSign, minYRectangle, Color.White);
                    spriteBatch.Draw(GameValues.arrowSign, plusTypeRectangle, Color.White);
                    spriteBatch.Draw(GameValues.arrowSign, minTypeRectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                    spriteBatch.Draw(GameValues.makeBuilding, makeGridRectangle, Color.White);
                    spriteBatch.Draw(GameValues.tileTex, new Rectangle(200, 510, 100, 30), Color.White);
                    spriteBatch.DrawString(GameValues.font, "X = " + newGridX, new Vector2(250, 525) - GameValues.font.MeasureString("X = " + newGridX), Color.Black);
                    spriteBatch.Draw(GameValues.tileTex, new Rectangle(200, 610, 100, 30), Color.White);
                    spriteBatch.DrawString(GameValues.font, "Y = " + newGridY, new Vector2(250, 625) - GameValues.font.MeasureString("Y = " + newGridY), Color.Black);
                    spriteBatch.Draw(GameValues.tileTex, new Rectangle(200, 710, 100, 30), Color.White);
                    spriteBatch.DrawString(GameValues.font, "Type = " + GameValues.buildingTypes[newBuildingType], new Vector2(250, 725) - GameValues.font.MeasureString("Type = " + GameValues.buildingTypes[newBuildingType]), Color.Black);
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
            if (click(buildRectangle, mouseState, prevMouseState))
            {
                GameValues.state = GameState.build;
            }
            else if(click(selectRectangle, mouseState, prevMouseState))
            {
                GameValues.state = GameState.select;
            }
            else if (click(removeRectangle, mouseState, prevMouseState))
            {
                GameValues.state = GameState.remove;
            }
            else if (click(zoneRectangle, mouseState, prevMouseState))
            {
                GameValues.state = GameState.zone;
            }
            else if (click(plusRectangle, mouseState, prevMouseState))
            {
                GameValues.state = GameState.plus;
                newGridX = newGridY = 10;
                newBuildingType = 1;
            }

            switch (GameValues.state)
            {
                case GameState.zone:
                    for (int i = 0; i < zoneRectangles.Count(); ++i)
                    {
                        if (click(zoneRectangles[i], mouseState, prevMouseState))
                        {
                            GameValues.selectedZone = (Zone)i;
                        }
                    }
                    break;
                case GameState.build:
                    if (click(otherBuildState, mouseState, prevMouseState))
                    {
                        GameValues.buildState = GameValues.buildState == BuildState.room ? BuildState.singleTile : BuildState.room;
                    }
                    if(GameValues.buildState == BuildState.singleTile)
                    {
                        for(int i = 0; i < GameValues.tileColors.Count(); i++)
                        {
                            if (click(allTiles[i], mouseState, prevMouseState))
                            {
                                GameValues.selectedTile = (BuildTiles)i;
                            }
                        }
                    }
                    else
                    {

                    }
                    break;
                case GameState.plus:
                    if (click(plusXRectangle, mouseState, prevMouseState))
                    {
                        newGridX++;
                    }
                    else if (click(minXRectangle, mouseState, prevMouseState))
                    {
                        newGridX--;
                    }
                    else if (click(plusYRectangle, mouseState, prevMouseState))
                    {
                        newGridY++;
                    }
                    else if (click(minYRectangle, mouseState, prevMouseState))
                    {
                        newGridY--;
                    }
                    else if (click(plusTypeRectangle, mouseState, prevMouseState))
                    {
                        newBuildingType++;
                        if (newBuildingType >= GameValues.buildingTypes.Count)
                        {
                            newBuildingType = 1;
                        }
                    }
                    else if (click(minTypeRectangle, mouseState, prevMouseState))
                    {
                        newBuildingType--;
                        if (newBuildingType < 1)
                        {
                            newBuildingType = GameValues.buildingTypes.Count - 1;
                        }
                    }
                    else if (click(makeGridRectangle, mouseState, prevMouseState))
                    {
                        //Call gridMaker
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
        private bool click(Rectangle rectangle, MouseState mouseState, MouseState prevMouseState)
        {
            return rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released;
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
            plusRectangle.X += direction;
            bottomRectangle.Y -= direction;
            otherBuildState.Y -= direction;
            addBuild.Y -= direction;
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
