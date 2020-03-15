using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameDesign
{
    class BuildingSelector : Selectors
    {
        public BuildingSelector()
        {
            color = Color.Blue;
        }
        public override void Update(MouseState mouseState, MouseState prevMouseState, Tile selectedTile)
        {
            base.Update(mouseState, prevMouseState, selectedTile);
            color = GameValues.zoneColors[(GameValues.buildingTypes.FindIndex(b => { return b == GameValues.selectedBuildingType; })) - 1];
            if (mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed)
            {
                IEnumerable<Tile> query = from t in GameValues.grid.Cast<Tile>() where drawRectangle.Contains(t.rectangle.Location) && t.type == Type.floor select t;
                foreach (Tile t in query)
                {
                    t.buildingType = GameValues.selectedBuildingType;
                }
                drawRectangle.X = 0;
                drawRectangle.Y = 0;
                drawRectangle.Width = 0;
                drawRectangle.Height = 0;
                GameValues.getAllBuildings();
                GameValues.CountTypes();
            }
        }
    }
}
