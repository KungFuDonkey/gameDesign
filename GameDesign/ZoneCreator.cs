﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace GameDesign
{
    class ZoneCreator : Selectors
    {
        public ZoneCreator()
        {
            color = Color.Blue;
        }
        public override void Update(MouseState mouseState, MouseState prevMouseState, Tile selectedTile)
        {
            base.Update(mouseState, prevMouseState, selectedTile);
            color = GameValues.zoneColors[(int)GameValues.selectedZone];
            if (mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed)
            {
                IEnumerable<Tile> query = from t in GameValues.grid.Cast<Tile>() where drawRectangle.Contains(t.rectangle.Location) && t.place == firstSelection.place && t.place == secondSelection.place && t.type == Type.building select t;
                foreach(Tile t in query)
                {
                    t.zone = GameValues.selectedZone;
                }
                drawRectangle.X = 0;
                drawRectangle.Y = 0;
                drawRectangle.Width = 0;
                drawRectangle.Height = 0;
            }
        }
    }
}
