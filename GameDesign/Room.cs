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
        public int place, rotation = 0, walls, floors;
        int size, width, height;
        public Point middle;
        public List<Tile> layout;
        public string path;
        public bool part;

        public Room(string path)
        {
            this.path = path;
            place = -100;
            setValues(path);
            part = false;
        }
        public Room(string path, int _place)
        {
            this.path = path;
            place = _place;
            setValues(path);
            part = true;
        }
        //reads the values from the path and creates the layout from that
        public void setValues(string path)
        {
            walls = 0;
            floors = 0;
            layout = new List<Tile>();
            zone = Zone.Break;
            List<string> lines = File.ReadAllLines(path).ToList();
            int x = 0, maxX = 0;
            int y = 0;
            int tileSize = GameValues.tileSize;
            foreach(string l in lines)
            {
                foreach(char c in l)
                {
                    switch (c)
                    {
                        case 'A':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place,Color.Aqua, zone));
                            break;
                        case 'B':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.Black, zone));
                            break;
                        case 'C':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.CadetBlue, zone));
                            break;
                        case 'D':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.DimGray, zone));
                            break;
                        case 'E':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.Chocolate, zone));
                            break;
                        case 'F':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.Fuchsia, zone));
                            break;
                        case 'G':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.DarkGreen, zone));
                            break;
                        case 'H':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.HotPink, zone));
                            break;
                        case 'I':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.Indigo, zone));
                            break;
                        case 'J':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.IndianRed, zone));
                            break;
                        case 'K':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.Khaki, zone));
                            break;
                        case 'L':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.Lavender, zone));
                            break;
                        case 'M':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.Magenta, zone));
                            break;
                        case 'N':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.Navy, zone));
                            break;
                        case 'O':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.Orange, zone));
                            break;
                        case 'P':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.PaleTurquoise, zone));
                            break;
                        case 'Q':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.PaleGoldenrod, zone));
                            break;
                        case 'R':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.Red, zone));
                            break;
                        case 'S':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.Silver, zone));
                            break;
                        case 'T':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.Teal, zone));
                            break;
                        case 'U':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.Sienna, zone));
                            break;
                        case 'V':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.Violet, zone));
                            break;
                        case 'W':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.White, zone));
                            break;
                        case 'X':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.Wheat, zone));
                            break;
                        case 'Y':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.Yellow, zone));
                            break;
                        case 'Z':
                            layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, Color.YellowGreen, zone));
                            break;
                        case ' ':
                            break;
                    }
                    x += 1;
                    maxX = maxX < x ? x : maxX;
                    walls++;
                }
                x = 0;
                y += 1;
            }
            size = maxX > y ? maxX : y;
            width = maxX;
            height = y;
            middle = new Point((width / 2) * tileSize, (height / 2) * tileSize);
        }
        //rotates the layout counterclockwise on the grid
        public void rotate()
        {
            if(layout.Count == 0)
            {
                return;
            }
            int tileSize = GameValues.tileSize;
            //rotation
            for(int x = 0; x < size/2; x++)
            {
                for(int y = x; y < size - x - 1; y++)
                {
                    List<Tile> query1 = (from t in layout where t.rectangle.X == x * tileSize && t.rectangle.Y == y * tileSize select t).ToList();
                    List<Tile> query2 = (from t in layout where t.rectangle.X == y * tileSize && t.rectangle.Y == (size - 1 - x) * tileSize select t).ToList();
                    List<Tile> query3 = (from t in layout where t.rectangle.X == (size - 1 - x) * tileSize && t.rectangle.Y == (size - 1 - y) * tileSize select t).ToList();
                    List<Tile> query4 = (from t in layout where t.rectangle.X == (size - 1 - y) * tileSize && t.rectangle.Y == x * tileSize select t).ToList();
                    foreach(Tile t in query1)
                    {
                        t.rectangle.X = (size - 1 - y) * tileSize;
                        t.rectangle.Y = x * tileSize;
                    }
                    foreach(Tile t in query2)
                    {
                        t.rectangle.X = x * tileSize;
                        t.rectangle.Y = y * tileSize;
                    }
                    foreach(Tile t in query3)
                    {
                        t.rectangle.X = y * tileSize;
                        t.rectangle.Y = (size - 1 - x) * tileSize;
                    }
                    foreach(Tile t in query4)
                    {
                        t.rectangle.X = (size - 1 - x) * tileSize;
                        t.rectangle.Y = (size - 1 - y) * tileSize;
                    }
                }
            }
            //moving the layout back to the original spot otherwise the middle won't be in the right spot
            while(layout.Min(x => x.rectangle.X) != 0)
            {
                foreach(Tile t in layout)
                {
                    t.rectangle.X -= tileSize;
                }
            }
            while(layout.Min(x => x.rectangle.Y) != 0)
            {
                foreach(Tile t in layout)
                {
                    t.rectangle.Y -= tileSize;
                }
            }
            int temp = height;
            height = width;
            width = temp;
            middle = new Point((width / 2) * tileSize, (height / 2) * tileSize);
        }
    }
}
