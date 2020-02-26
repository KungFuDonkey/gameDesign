using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
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
        public Room(string path)
        {
            layer = -100;
            setValues(path);
            part = false;
        }
        public void setValues(string path)
        {
            layout = new List<Tile>();
            zone = Zone.Break;
            List<string> lines = File.ReadAllLines(path).ToList();
            int x = 0, maxX = 0;
            int y = 0;
            foreach(string l in lines)
            {
                foreach(char c in l)
                {
                    switch (c)
                    {
                        case '#':
                            layout.Add(new Wall(new Rectangle(x * GameValues.tileSize, y * GameValues.tileSize, GameValues.tileSize, GameValues.tileSize), layer, zone));
                            break;
                        case '0':
                            layout.Add(new Floor(new Rectangle(x * GameValues.tileSize, y * GameValues.tileSize, GameValues.tileSize, GameValues.tileSize), layer, zone));
                            break;
                        case ' ':
                            break;
                    }
                    x += 1;
                    maxX = maxX < x ? x : maxX;
                }
                x = 0;
                y += 1;
            }
            Debug.WriteLine(maxX.ToString() + " " + y.ToString());
            middle = new Point((maxX / 2) * GameValues.tileSize, (y / 2) * GameValues.tileSize);
        }
    }
}
