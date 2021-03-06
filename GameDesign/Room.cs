﻿using System;
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
    public class Room
    {
        public Zone zone;
        public BuildingType buildingType;
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
            List<string> lines = File.ReadAllLines(path).ToList();
            buildingType = GameValues.buildingTypes[int.Parse(lines[0])];
            lines.RemoveAt(0);
            int x = 0, maxX = 0;
            int y = 0;
            int tileSize = GameValues.tileSize;
            foreach(string l in lines)
            {
                foreach(char c in l)
                {
                    if(c == '#')
                    {
                        layout.Add(new Enterance(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, zone));
                    }
                    else if(c != ' ')
                    {
                        layout.Add(new ColorTile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), place, GameValues.buildTileColors[Array.IndexOf(GameValues.buildChars, c)], zone));
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
