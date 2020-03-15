using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameDesign
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Tile SelectedTile;
        RoomPreview roomPreview = new RoomPreview();
        Remove remove = new Remove();
        NPC npc = new NPC();
        public static Camera cam = new Camera();
        public Timer gameTimer = new Timer();
        Hud hud;
        bool onhud;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public static Point viewport = new Point(1280, 900);
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            for(int i = 0; i < GameValues.gridWidth; i++)
            {
                for(int j = 0; j < GameValues.gridHeight; j++)
                {
                    Rectangle rect = new Rectangle(i * GameValues.tileSize, j * GameValues.tileSize, GameValues.tileSize, GameValues.tileSize);
                    GameValues.tiles.Add(new Grass(rect,0, new Point(i,j)));
                    rect = new Rectangle(i * GameValues.tileSize, j * GameValues.tileSize, GameValues.tileSize, GameValues.tileSize); //dont remove for different rectangle
                    GameValues.tiles.Add(new Stone(rect, -1));
                }
            }
            GameValues.npcs.Add(npc);
            graphics.PreferredBackBufferWidth = viewport.X;
            graphics.PreferredBackBufferHeight = viewport.Y;
            graphics.ApplyChanges();
            roomPreview.initialize();
            hud = new Hud(1280, 900);
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
            GameValues.font = Content.Load<SpriteFont>("SpelFont");
            GameValues.tileTex = Content.Load<Texture2D>("Tile");
            GameValues.Student = Content.Load<Texture2D>("player_side");
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
            cam.Update(keys, prevKeys, mouseState, prevMouseState);
            foreach (NPC n in GameValues.npcs)
            {
                n.Update(keys, prevKeys, gameTime);
            }
            onhud = hud.Update(mouseState, prevMouseState, gameTime);
            if (!onhud)
            {
                switch (GameValues.state)
                {
                    case GameState.build:
                        roomPreview.Update(keys, prevKeys, mouseState, prevMouseState, SelectedTile.rectangle);
                        break;
                    case GameState.remove:
                        remove.Update(mouseState,prevMouseState,SelectedTile);
                        break;
                    case GameState.select:
                        break;
                }
            }
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
                        remove.Draw(spriteBatch, gameTime);
                        break;
                }
            }
            npc.Draw(spriteBatch);
            hud.draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
