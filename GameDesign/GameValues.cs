using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace GameDesign
{
    enum GameState
    {
        menu,
        build,
        select,
        remove,
        zone
    }
    enum BuildState
    {
        room,
        singleTile
    }
    enum BuildTiles //maybe roads aswell
    {
        pavement
    }
    static class GameValues
    {
        public static int gridWidth = 200, gridHeight = 200, tileSize = 10, maxPlaces = 20, currentMaxPlace = 1;
        public static Tile[,,] grid;
        public static int gridSize = gridHeight * gridWidth;
        public static Texture2D tileTex, hammer, colorplate, remover, colorSpetter, warning;
        public static BuildState buildState = BuildState.room;
        public static SpriteFont font;
        public static Color[] zoneColors = new Color[6] { Color.Blue, Color.Gray, Color.Brown, Color.Black, Color.Green, Color.Yellow };
        public static Zone selectedZone = Zone.Lesson;
        public static Color[] tileColors = new Color[1] { Color.Gray };//floor wall pavement
        public static GameState state = GameState.menu;
        public static BuildTiles selectedTile = BuildTiles.pavement;
        public static List<BuildingType> buildingTypes = new List<BuildingType>();
        public static List<Building> buildings = new List<Building>();
        //Money
        public static int wallCost = 5;
        public static int floorCost = 1;

        public static int students = 0;
        public static int workers = 0;

        public static int studentIncome = 15;
        public static int researchIncome = 25;

        public static int governmentSubsidy = 2000;
        public static string appDataFilePath;
        public static string gameName = "UniversiTycoon";

        //Amount of buildingTiles
        public static BuildingType none = new None();
        public static BuildingType adminBuilding = new AdminBuilding();
        public static BuildingType lectureBuilding = new LectureBuilding();
        public static BuildingType seminarBuilding = new SeminarBuilding();
        public static BuildingType researchBuilding = new ResearchBuilding();

        public static BuildingType tramStation = new TramStation();
        public static BuildingType bikeParking = new BikeParking();
        public static BuildingType carParking = new CarParking();

        public static BuildingType cafe = new Cafe();
        public static BuildingType foodcourt = new FoodCourt();
        public static BuildingType pub = new Pub();
        public static BuildingType supplyShop = new SupplyShop();
        public static BuildingType studentAssociationBuilding = new StudentAssociationBuilding();
        public static BuildingType gym = new Gym();
        public static BuildingType park = new Park();
        public static BuildingType superMarket = new SuperMarket();

        public static BuildingType selectedBuildingType = adminBuilding;
        public static void CountTypes()
        {
            foreach (BuildingType b in buildingTypes)
            {
                b.tileCount = 0;
            }
            foreach (Tile t in grid)
            {
                t.buildingType.tileCount++;
            }
        }

        public static void getAllBuildings()
        {
            buildings.Clear();
            List<Tile> done = new List<Tile>();
            foreach (Tile t in grid)
            {
                if (t.type != Type.building || done.Contains(t) ||t.buildingType == none)
                {
                    continue;
                }
                List<Tile> neighbours = t.connectedNeighbours();
                if (neighbours == null)
                {
                    continue;
                }
                buildings.Add(new Building(t.buildingType, neighbours));
                done.AddRange(neighbours);
            }
        }

        public static int maintenanceCosts
        {
            get
            {
                int value = 0;
                foreach (BuildingType b in buildingTypes)
                {
                    value += b.maintenanceCost * b.tileCount;
                }
                return value;
            }
        }
    }
}
