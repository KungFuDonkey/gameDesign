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
        public static int gridSize = gridHeight * gridWidth;
        public static Texture2D tileTex, hammer, colorplate, remover, colorSpetter;
        public static List<Tile> tiles = new List<Tile>();
        public static BuildState buildState = BuildState.room;
        public static SpriteFont font;
        public static Color[] zoneColors = new Color[6] { Color.Blue, Color.Gray, Color.Brown, Color.Black, Color.Green, Color.Green };
        public static Zone selectedZone = Zone.Lesson;
        public static Color[] tileColors = new Color[1] { Color.Gray };//floor wall pavement
        public static GameState state = GameState.menu;
        public static BuildTiles selectedTile = BuildTiles.pavement;
        //Money
        public static int wallCost = 5;
        public static int floorCost = 1;

        public static int students = 0;
        public static int teachers = 0;
        public static int staff = 0;

        public static int studentIncome = 30;
        public static int teacherSalary = 100;
        public static int staffSalary = 60;


        public static string appDataFilePath;
        public static string gameName = "UniversiTycoon";
    }
}
