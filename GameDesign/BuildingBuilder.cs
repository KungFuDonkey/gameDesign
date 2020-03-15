using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace GameDesign
{
    public class BuildingBuilder
    {
        public Tile[,,] grid;
        char selectedTile;
        char[,] save;
        Zone zone;
        int size, screenheight;
        float timer = 0.01f, TIMER = 0.01f;
        Rectangle[] ColorBlocks = new Rectangle[28];
        Rectangle bottomRectangle;
        public BuildingBuilder(int _size, Zone _zone, int _screenheight, int screenwidth)
        {
            size = _size;
            save = new char[size, size];
            grid = new Tile[size, size, 1];
            zone = _zone;
            GameValues.tileSize = 10;
            int tileSize = GameValues.tileSize;
            for(int y = 0; y < size; y++)
            {
                for(int x = 0; x < size; x++)
                {
                    save[x, y] = ' ';
                    grid[x, y, 0] = new Grass(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), 0);
                }
            }
            bottomRectangle = new Rectangle(0, _screenheight - 20, screenwidth, 50);
            for(int i = 0; i < 28; i++)
            {
                ColorBlocks[i] = new Rectangle(i * 50, _screenheight - 17, 50, 50);
            }
            screenheight = _screenheight;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tile t in grid)
            {
                spriteBatch.Draw(GameValues.tileTex, t.rectangle, t.color);
            }
            for(int i = 0; i < 28; i++)
            {
                spriteBatch.Draw(GameValues.tileTex, ColorBlocks[i], GameValues.buildTileColors[i]);
            }
        }
        public void Update(MouseState mouseState, MouseState prevMouseState, GameTime gameTime)
        {
            for(int y = 0; y < size; y++)
            {
                for(int x = 0; x < size; x++)
                {
                    if (grid[x, y, 0].rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        save[x, y] = selectedTile;
                        grid[x, y, 0].color = GameValues.buildTileColors[Array.IndexOf(GameValues.buildChars, selectedTile)];
                    }
                }
            }
            if (bottomRectangle.Contains(mouseState.Position))
            {
                if (bottomRectangle.Y > screenheight - 50)
                {
                    slide(true, gameTime);
                }
                else if(bottomRectangle.Y > screenheight - 20)
                {
                    slide(false, gameTime);
                }
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
            for (int i = 0; i < ColorBlocks.Count(); ++i)
            {
                ColorBlocks[i].Y -= direction;
            }
        }
    }
}
