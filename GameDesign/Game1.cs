using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace GameDesign
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Tile SelectedTile;
        public static RoomPreview roomPreview = new RoomPreview();
        Remove remove = new Remove();
        TileCreator tileCreator = new TileCreator();
        BuildingSelector buildingSelector = new BuildingSelector();
        public static Camera cam = new Camera();
        public List<NPC> nPCs = new List<NPC>();

        Phase currentPhase;
        public Timer gameTimer;
        public static Money money = new Money(10000);
        public static Menu menu;
        public static ScoreSystem score;
        public static BuildingBuilder buildingBuilder;
        Hud hud;
        bool onhud; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            currentPhase = Phase.morning;
            gameTimer = new Timer(currentPhase, 7);
            GameValues.appDataFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        public static Point viewport = new Point(1280, 900);
        protected override void Initialize()
        {
            NPC npc = new NPC();
            nPCs.Add(npc);
            
            Rectangle rect = new Rectangle(0,0,0,0);
            GameValues.grid = new Tile[GameValues.gridWidth, GameValues.gridHeight, 1];
            int tileSize = GameValues.tileSize;
            for (int x = 0; x < GameValues.gridWidth; x++)
            {
                for (int y = 0; y < GameValues.gridHeight - 3; y++)
                {
                    rect = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                    GameValues.grid[x, y, 0] = new Grass(rect, 0);
                }
                rect = new Rectangle(x * GameValues.tileSize, (GameValues.gridHeight - 3) * GameValues.tileSize, GameValues.tileSize, GameValues.tileSize);
                GameValues.grid[x, GameValues.gridHeight - 3, 0] = new Pavement(rect, 0);
                for(int y = 2; y > 0; y--)
                {
                    rect = new Rectangle(x * GameValues.tileSize, (GameValues.gridHeight - y) * GameValues.tileSize, GameValues.tileSize, GameValues.tileSize);
                    GameValues.grid[x, GameValues.gridHeight - y, 0] = new Road(rect, 0);
                }
            }
            graphics.PreferredBackBufferWidth = viewport.X;
            graphics.PreferredBackBufferHeight = viewport.Y;
            graphics.ApplyChanges();
            roomPreview.initialize();
            hud = new Hud(1280, 900);
            menu = new Menu();
            score = new ScoreSystem();
            IsMouseVisible = true;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameValues.font = Content.Load<SpriteFont>("Fonts/SpelFont");
            GameValues.hammer = Content.Load<Texture2D>("Hamer");
            menu.LoadContent(Content);
            money.LoadContent(Content);
            GameValues.tileTex = Content.Load<Texture2D>("Tile");
            GameValues.door = Content.Load<Texture2D>("Door");
            GameValues.tree = Content.Load<Texture2D>("Tree");
            GameValues.prullenbak = Content.Load<Texture2D>("Prullenbak");
            GameValues.path = Content.Load<Texture2D>("Path");
            GameValues.wall1 = Content.Load<Texture2D>("Wall1");
            GameValues.wall2 = Content.Load<Texture2D>("Wall2");
            GameValues.wall3 = Content.Load<Texture2D>("Wall3");
            GameValues.wall4 = Content.Load<Texture2D>("Wall4");
            GameValues.wall5 = Content.Load<Texture2D>("Wall5");
            GameValues.wall6 = Content.Load<Texture2D>("Wall6");
            GameValues.stoplicht = Content.Load<Texture2D>("Stoplicht");
            GameValues.water = Content.Load<Texture2D>("Water");
            GameValues.pion = Content.Load<Texture2D>("Pion");
            GameValues.parkeerplaats = Content.Load<Texture2D>("Parkeerplaats");
            GameValues.fietsplaats = Content.Load<Texture2D>("Fietsplaats");
            GameValues.colorplate = Content.Load<Texture2D>("ColorPlate");
            GameValues.remover = Content.Load<Texture2D>("Eraser");
            GameValues.colorSpetter = Content.Load<Texture2D>("White");
            GameValues.warning = Content.Load<Texture2D>("Warning");
            GameValues.plus = Content.Load<Texture2D>("Plus");
            GameValues.popUp = Content.Load<Texture2D>("Buttons/PopUp");
            GameValues.plusSign = Content.Load<Texture2D>("Buttons/plusSign");
            GameValues.minSign = Content.Load<Texture2D>("Buttons/minSign");
            GameValues.arrowSign = Content.Load<Texture2D>("Buttons/arrowSign");
            GameValues.makeBuilding = Content.Load<Texture2D>("Buttons/makeBuilding");
            GameValues.student_right = Content.Load<Texture2D>("Student_Right");
            GameValues.student_left = Content.Load<Texture2D>("Student_Left");
            GameValues.student_up = Content.Load<Texture2D>("Student_Up");
            GameValues.student_down = Content.Load<Texture2D>("Student_Down");
            GameValues.cursor = Content.Load<Texture2D>("Computer_Cursor");
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        KeyboardState keys;
        KeyboardState prevKeys;
        MouseState mouseState;
        MouseState prevMouseState;

        protected override void Update(GameTime gameTime)
        {
            if (!Program.game.IsActive)
            {
                return;
            }
            else
            {
                keys = Keyboard.GetState();
                mouseState = Mouse.GetState();
            }
            if (keys.IsKeyDown(Keys.Escape) && prevKeys.IsKeyUp(Keys.Escape))
            {
                if (GameValues.state != GameState.menu)
                {
                    menu.newMenuState = MenuState.Pause;
                    GameValues.state = GameState.menu;
                }
                else if (GameValues.state == GameState.menu && menu.menuState == MenuState.Pause)
                {
                    GameValues.state = GameState.select;
                }
            }

            if (GameValues.state == GameState.menu)
            {
                menu.Update(keys, prevKeys, mouseState, prevMouseState, this);
            }
            else if(GameValues.state == GameState.editor)
            {
                cam.Update(keys, prevKeys, mouseState, prevMouseState, buildingBuilder.grid);
                buildingBuilder.Update(mouseState, prevMouseState, gameTime);
            }
            else
            {
                /*
                while ((GameValues.walkAbleTiles()) > nPCs.Count * 50)
                {
                    nPCs.Add(new NPC());
                }
                foreach(NPC n in nPCs)
                {
                    n.Update(keys, prevKeys, gameTime);
                }
                */
                cam.Update(keys, prevKeys, mouseState, prevMouseState, GameValues.grid);
                if (gameTimer.isPhaseOver())
                {
                    currentPhase = gameTimer.getCurrentPhase();
                    if (currentPhase == Phase.morning)
                    {
                        money.DailyPay();
                    }
                }

                IEnumerable<Tile> query = from t in GameValues.grid.Cast<Tile>() where t.place == cam.place select t;
                foreach (Tile t in query)
                {
                    t.Update(mouseState);
                }
                foreach (Building b in GameValues.buildings)
                {
                    b.Update(mouseState);
                }

                money.Update(keys, prevKeys);
                score.Update();
                onhud = hud.Update(mouseState, prevMouseState, gameTime);
                if (!onhud)
                {
                    switch (GameValues.state)
                    {
                        case GameState.build:
                            switch (GameValues.buildState)
                            {
                                case BuildState.room:
                                    roomPreview.Update(keys, prevKeys, mouseState, prevMouseState, SelectedTile.rectangle);
                                    break;
                                case BuildState.singleTile:
                                    tileCreator.Update(mouseState, prevMouseState, SelectedTile);
                                    break;
                            }
                            break;
                        case GameState.remove:
                            remove.Update(mouseState, prevMouseState, SelectedTile);
                            break;
                        case GameState.select:
                            break;
                        case GameState.zone:
                            buildingSelector.Update(mouseState, prevMouseState, SelectedTile);
                            break;
                    }
                }
            }
            prevKeys = keys;
            prevMouseState = mouseState;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (GameValues.state == GameState.menu)
            {
                GraphicsDevice.Clear(Color.White);
                menu.Draw(spriteBatch);
            }
            else if(GameValues.state == GameState.editor)
            {
                GraphicsDevice.Clear(Color.Green);
                buildingBuilder.Draw(spriteBatch);
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);
                IEnumerable<Tile> query = from t in GameValues.grid.Cast<Tile>() where t.place == cam.place select t;
                foreach (Tile t in query)
                {
                    t.Draw(spriteBatch, currentPhase);
                }
                if (GameValues.state == GameState.zone)
                {
                    foreach(Tile t in GameValues.grid)
                    {
                        t.DrawZone(spriteBatch);
                    }

                }

                foreach(NPC n in nPCs)
                {
                    n.Draw(spriteBatch);
                }
                foreach (Building b in GameValues.buildings)
                {
                    b.Draw(spriteBatch);
                }

                if (!onhud)
                {
                    switch (GameValues.state)
                    {
                    case GameState.build:
                        switch (GameValues.buildState)
                        {
                            case BuildState.room:
                                roomPreview.Draw(spriteBatch, SelectedTile.rectangle);
                                break;
                            case BuildState.singleTile:
                                tileCreator.Draw(spriteBatch, gameTime);
                                break;
                        }
                        break;
                    case GameState.select:
                        break;
                    case GameState.remove:
                        remove.Draw(spriteBatch, gameTime);
                        break;
                    case GameState.zone:
                        buildingSelector.Draw(spriteBatch, gameTime);
                        break;
                    }
                }
                hud.draw(spriteBatch);
                money.Draw(spriteBatch);
                score.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
