﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Collections.Generic;

namespace GameDesign
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        public static Tile SelectedTile;
        RoomPreview roomPreview = new RoomPreview();
        public static Camera cam = new Camera();
        public Timer gameTimer = new Timer();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
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
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();
            roomPreview.initialize();
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
            font = Content.Load<SpriteFont>("SpelFont");
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
            if (gameTimer.isPhaseOver())
            {
                //TODO: switch to night or next day.
            }

            keys = Keyboard.GetState();
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            IEnumerable<Tile> query = from t in GameValues.tiles where t.layer == cam.layer select t;
            foreach (Tile t in query)
            {
                t.Update(mouseState);
            }
            cam.Update(keys, prevKeys);
            roomPreview.Update(keys, prevKeys, mouseState, prevMouseState, SelectedTile.rectangle);
            prevKeys = keys;
            prevMouseState = mouseState;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            IEnumerable<Tile> query = from t in GameValues.tiles where t.layer == cam.layer select t;
            foreach (Tile t in query)
            {
                t.Draw(spriteBatch);
            }
            roomPreview.Draw(spriteBatch, SelectedTile.rectangle);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
