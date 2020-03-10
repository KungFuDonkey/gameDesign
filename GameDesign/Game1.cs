using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace GameDesign
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static SpriteFont font;
        public static Tile SelectedTile;
        RoomPreview roomPreview = new RoomPreview();
        Remove remove = new Remove();
        public static Camera cam = new Camera();
        public Timer gameTimer = new Timer();
        public static Money money = new Money(100000);
        public static Menu menu;
        Hud hud;
        bool onhud;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GameValues.appDataFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        public static Point viewport = new Point(1280, 900);
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            for(int i = 0; i < GameValues.gridWidth; i++)
            {
                for(int j = 0; j < GameValues.gridHeight; j++)
                {
                    GameValues.tiles.Add(new Grass(new Rectangle(i*GameValues.tileSize,j*GameValues.tileSize,GameValues.tileSize,GameValues.tileSize),0));
                }
            }
            graphics.PreferredBackBufferWidth = viewport.X;
            graphics.PreferredBackBufferHeight = viewport.Y;
            graphics.ApplyChanges();
            roomPreview.initialize();
            hud = new Hud(1280, 900);
            menu = new Menu();
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Fonts/SpelFont");
            menu.LoadContent(Content);
            money.LoadContent(Content);
            GameValues.tileTex = Content.Load<Texture2D>("Tile");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        KeyboardState keys;
        KeyboardState prevKeys;
        MouseState mouseState;
        MouseState prevMouseState;
        protected override void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keys.IsKeyDown(Keys.Escape) && GameValues.state != GameState.menu)
            {
                if (menu.menuState == MenuState.Main && GameValues.state == GameState.menu)
                {
                    Exit();
                }
                else
                {
                    menu.newMenuState = MenuState.Pause;
                    GameValues.state = GameState.menu;
                }
            }

            if (GameValues.state == GameState.menu)
            {
                menu.Update(keys, prevKeys, mouseState, prevMouseState, this);
            }
            else
            {
                if (gameTimer.isPhaseOver())
                {
                    //TODO: switch to night or next day.
                    if (gameTimer.getCurrentPhase() == Phase.morning)
                    {
                        money.earnCash(GameValues.students * GameValues.studentIncome);
                        money.payCash(GameValues.teachers * GameValues.teacherSalary);
                        money.payCash(GameValues.staff * GameValues.staffSalary);
                    }
                }

                IEnumerable<Tile> query = from t in GameValues.tiles where t.layer == cam.layer select t;
                foreach (Tile t in query)
                {
                    t.Update(mouseState);
                }
                cam.Update(keys, prevKeys, mouseState, prevMouseState);
                money.Update(keys, prevKeys);
                onhud = hud.Update(mouseState, prevMouseState, gameTime);
                if (!onhud)
                {
                    switch (GameValues.state)
                    {
                        case GameState.build:
                            roomPreview.Update(keys, prevKeys, mouseState, prevMouseState, SelectedTile.rectangle);
                            break;
                        case GameState.select:
                            break;
                        case GameState.remove:
                            remove.Update(mouseState, prevMouseState, SelectedTile);
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
            else
            {
                GraphicsDevice.Clear(Color.Black);
                IEnumerable<Tile> query = from t in GameValues.tiles where t.layer == cam.layer select t;
                foreach (Tile t in query)
                {
                    t.Draw(spriteBatch);
                }
                if (!onhud)
                {
                    switch (GameValues.state)
                    {
                        case GameState.build:
                            roomPreview.Draw(spriteBatch, SelectedTile.rectangle);
                            break;
                        case GameState.select:
                            break;
                        case GameState.remove:
                            break;
                    }
                }
                hud.draw(spriteBatch);
                money.draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
