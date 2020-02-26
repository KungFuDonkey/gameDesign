﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDesign
{
    class Room
    {
        public Zone zone;
        public int layer;
        public Point middle;
        public List<Tile> layout;
        public bool part;
        public Room()
        {
            zone = Zone.Break;
            layer = -100;
            setValues("");
            part = false;
        }
        public void setValues(string path)
        {
            layout = new List<Tile>();
            middle = new Point(2 * GameValues.tileSize, 2 * GameValues.tileSize);
            zone = Zone.Break;
            for (int i = 0; i < 5; i++)
            {
                layout.Add(new Wall(new Rectangle(i * GameValues.tileSize, 0, GameValues.tileSize, GameValues.tileSize), layer, zone));
                layout.Add(new Wall(new Rectangle(i * GameValues.tileSize, 4 * GameValues.tileSize, GameValues.tileSize, GameValues.tileSize), layer, zone));
            }
            for (int i = 0; i < 3; i++)
            {
                layout.Add(new Wall(new Rectangle(0, (i + 1) * GameValues.tileSize, GameValues.tileSize, GameValues.tileSize), layer, zone));
                layout.Add(new Wall(new Rectangle(4 * GameValues.tileSize, (i + 1) * GameValues.tileSize, GameValues.tileSize, GameValues.tileSize), layer, zone));
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    layout.Add(new Floor(new Rectangle((j + 1) * GameValues.tileSize, (i + 1) * GameValues.tileSize, GameValues.tileSize, GameValues.tileSize), layer, zone));
                }
            }
        }
    }
}
