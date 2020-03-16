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
        Zone zone;
        int sizex, sizey, screenheight;
        float timer = 0.01f, TIMER = 0.01f;
        Rectangle[] ColorBlocks = new Rectangle[28];
        Rectangle bottomRectangle;
        Rectangle saveRectangle;
        public BuildingBuilder(int _sizex, int _sizey, Zone _zone, int _screenwidth, int _screenheight)
        {
            sizex = _sizex;
            sizey = _sizey;
            save = new char[_sizex, _sizey];
            grid = new Tile[_sizex, _sizey, 1];
            zone = _zone;
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
            saveRectangle = new Rectangle(_screenwidth - 100, _screenheight - 20, 100, 50);
            for(int i = 0; i < 28; i++)
            {
                ColorBlocks[i] = new Rectangle(i * 40, _screenheight - 17, 40, 50);
            }
            screenheight = _screenheight;
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
            spriteBatch.Draw(GameValues.tileTex, saveRectangle, Color.Black);
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
                SaveBuilding.save(save, sizex, sizey, zone);
                GameValues.state = GameState.build;
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
            for (int i = 0; i < ColorBlocks.Count(); ++i)
            {
                ColorBlocks[i].Y -= direction;
            }
        }
    }
}
