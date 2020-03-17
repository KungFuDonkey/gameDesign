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
    public class BuildingBuilder
    {
        public Tile[,,] grid;
        char selectedTile = ' ';
        char[,] save;
        string currentError;
        int buildingType;
        int sizex, sizey, screenheight, Tilesize;
        float timer = 0.01f, TIMER = 0.01f;
        Rectangle[] ColorBlocks = new Rectangle[28];
        Rectangle bottomRectangle;
        Rectangle saveRectangle;
        Rectangle quitRectangle;
        Vector2 savePoint;
        Vector2 quitPoint;
        Vector2 errorPoint;
        Vector2 enterancePoint;
        public BuildingBuilder(int _sizex, int _sizey, int _buildingType, int _screenwidth, int _screenheight, int tileSizeSave)
        {
            Tilesize = tileSizeSave;
            sizex = _sizex;
            sizey = _sizey;
            save = new char[_sizex, _sizey];
            grid = new Tile[_sizex, _sizey, 1];
            buildingType = _buildingType;
            GameValues.tileSize = 10;
            int tileSize = GameValues.tileSize;
            for(int y = 0; y < sizey; y++)
            {
                for(int x = 0; x < sizex; x++)
                {
                    save[x, y] = ' ';
                    grid[x, y, 0] = new Grass(new Rectangle(400 + x * tileSize, 400 + y * tileSize, tileSize, tileSize), 0);
                }
            }
            bottomRectangle = new Rectangle(0, _screenheight - 20, _screenwidth, 50);
            saveRectangle = new Rectangle(_screenwidth - 50, _screenheight - 20, 50, 50);
            quitRectangle = new Rectangle(_screenwidth - 100, _screenheight - 20, 50, 50);
            savePoint = new Vector2(_screenwidth - 45, _screenheight - 2);
            quitPoint = new Vector2(_screenwidth - 95, _screenheight - 2);
            errorPoint = new Vector2(_screenwidth / 2 - 45, 20);
            enterancePoint = new Vector2(1045, _screenheight - 2);
            for(int i = 0; i < 28; i++)
            {
                ColorBlocks[i] = new Rectangle(i * 40, _screenheight - 17, 40, 50);
            }
            screenheight = _screenheight;
            currentError = "There is no enterance";
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tile t in grid)
            {
                spriteBatch.Draw(GameValues.tileTex, t.rectangle, t.color);
            }
            spriteBatch.Draw(GameValues.tileTex, bottomRectangle, Color.Aquamarine);
            for(int i = 0; i < 27; i++)
            {
                spriteBatch.Draw(GameValues.tileTex, ColorBlocks[i], GameValues.buildTileColors[i]);
            }
            spriteBatch.Draw(GameValues.remover, ColorBlocks[27], Color.White);
            spriteBatch.Draw(GameValues.tileTex, saveRectangle, Color.White);
            spriteBatch.Draw(GameValues.tileTex, quitRectangle, Color.LightGray);
            spriteBatch.DrawString(GameValues.font, "save", savePoint, Color.Black);
            spriteBatch.DrawString(GameValues.font, "quit", quitPoint, Color.Black);
            spriteBatch.DrawString(GameValues.font, "ent", enterancePoint, Color.Black);
            spriteBatch.DrawString(GameValues.font, currentError, errorPoint, Color.Black);
        }
        public void Update(MouseState mouseState, MouseState prevMouseState, GameTime gameTime)
        {
            for(int y = 0; y < sizey; y++)
            {
                for(int x = 0; x < sizex; x++)
                {
                    Tile t = grid[x, y, 0];
                    if (t.rectangle.Contains(mouseState.Position))
                    {
                        t.color = Color.Blue;
                        Game1.SelectedTile = t;
                    }
                    else
                    {
                        t.color = t.standardColor;
                    }
                    if (t.rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        save[x, y] = selectedTile;
                        t.standardColor = GameValues.buildTileColors[Array.IndexOf(GameValues.buildChars, selectedTile)];
                        currentError = check();
                    }
                }
            }
            for(int i = 0; i < 28; i++)
            {
                if(ColorBlocks[i].Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    selectedTile = GameValues.buildChars[i];
                }
            }
            if(saveRectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                if (check() == "")
                {
                    SaveBuilding.save(save, sizex, sizey, buildingType);
                    GameValues.tileSize = Tilesize;
                    GameValues.state = GameState.build;
                    GameValues.buildState = BuildState.room;
                }
            }
            else if(quitRectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                GameValues.tileSize = Tilesize;
                GameValues.state = GameState.build;
                GameValues.buildState = BuildState.room;
            }
            if (bottomRectangle.Contains(mouseState.Position))
            {
                
                if (bottomRectangle.Y > screenheight - 50)
                {
                    slide(true, gameTime);
                }
            }
            else if (bottomRectangle.Y < screenheight - 20)
            {
                slide(false, gameTime);
            }
        }
        public void slide(bool outward, GameTime gameTime)
        {
            if (outward && timer < 0)
            {
                move(1);
                timer = TIMER;
            }
            else if (timer < 0)
            {
                move(-1);
                timer = TIMER;
            }
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void move(int direction)
        {
            bottomRectangle.Y -= direction;
            saveRectangle.Y -= direction;
            quitRectangle.Y -= direction;
            quitPoint.Y -= direction;
            savePoint.Y -= direction;
            enterancePoint.Y -= direction;
            for (int i = 0; i < ColorBlocks.Count(); ++i)
            {
                ColorBlocks[i].Y -= direction;
            }
        }
        public string check()
        {
            string normalBlock = "There are no normal blocks";
            string enterance = "There is no enterance";
            for(int y = 0; y < sizey; y++)
            {
                for(int x = 0; x < sizex; x++)
                {
                    if(save[x,y] != ' ')
                    {
                        int missingblocks = 0;
                        if (save[x, y] == '#')
                            enterance = "";
                        else
                            normalBlock = "";
                        if (x != 0)
                        {
                            if (save[x - 1, y] == ' ')
                                missingblocks++;
                        }
                        else
                        {
                            missingblocks++;
                        }
                        if (x != sizex - 1)
                        {
                            if (save[x + 1, y] == ' ')
                                missingblocks++;
                        }
                        else
                        {
                            missingblocks++;
                        }
                        if (y != 0)
                        {
                            if (save[x, y - 1] == ' ')
                                missingblocks++;
                        }
                        else
                        {
                            missingblocks++;
                        }
                        if (y != sizey - 1)
                        {
                            if (save[x, y + 1] == ' ')
                                missingblocks++;
                        }
                        else
                        {
                            missingblocks++;
                        }
                        if (missingblocks == 4)
                        {
                            return "There is a standalone block";
                        }
                    }
                }
            }
            if(normalBlock != "")
            {
                return normalBlock;
            }
            else
            {
                return enterance;
            }
        }
    }
}
