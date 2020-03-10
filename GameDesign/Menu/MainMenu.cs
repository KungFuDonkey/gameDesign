using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesign
{
    public enum MenuState
    {
        Main,
        Pause,
        Loading,
        LoadGame,
        SaveGame,
        Options
    }
    public class MainMenu
    {
        Rectangle logoRectangle = new Rectangle(10, 10, 400, 110), yellowBlockRectangle, titleRectangle, popUpRectangle = new Rectangle(0,0, 100, 100);
        public Texture2D UUlogo, yellowBlock, title, popUp, emptyButton;
        public Button playButton, resumeButton, optionsButton, cancelButton, okButton, exitButton, loadgameButton, newgameButton, savegameButton, applyButton, cancelOptionsButton, cancelPopUpButton;
        List<Button> buttons = new List<Button>();
        Point buttonSize = new Point(252, 101);
        public MenuState menuState = MenuState.Loading, prevMenuState, newState;
        bool popUpActive;
        string popUpText;
        public SpriteFont menuFont = Game1.font;

        public MainMenu()
        {
            newState = MenuState.Main;
            playButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X / 2, 300), buttonSize), emptyButton, "PLAY");
            resumeButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X / 2, 300), buttonSize), emptyButton, "RESUME");
            optionsButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X / 2, 450), buttonSize), emptyButton, "OPTIONS");
            cancelButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X / 2, 750), buttonSize), emptyButton, "CANCEL");
            okButton = new Button(new Rectangle(new Point(200, 600), buttonSize), emptyButton, "OK");
            cancelPopUpButton = new Button(new Rectangle(new Point(200, 600), buttonSize), emptyButton, "CANCEL");
            exitButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X / 2, 600), buttonSize), emptyButton, "EXIT");
            loadgameButton = new Button(new Rectangle(new Point(150, 550), buttonSize), emptyButton, "LOAD GAME");
            newgameButton = new Button(new Rectangle(new Point(150, 350), buttonSize), emptyButton, "NEW GAME");
            savegameButton = new Button(new Rectangle(new Point(Game1.viewport.X - 150 - buttonSize.X, 350), buttonSize), emptyButton, "SAVE GAME");
            applyButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X - 50, 750), buttonSize), emptyButton, "APPLY");
            cancelOptionsButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 + 50, 750), buttonSize), emptyButton, "CANCEL");
            //Main 0-2
            buttons.Add(playButton);
            buttons.Add(optionsButton);
            buttons.Add(exitButton);
            //Pause 1-4
            buttons.Add(resumeButton);
            buttons.Add(savegameButton);
            //LoadGame 5-7
            buttons.Add(loadgameButton);
            buttons.Add(newgameButton);
            buttons.Add(cancelButton);
            //PopUp 8-9
            buttons.Add(okButton);
            buttons.Add(cancelPopUpButton);
            //Options 10-11
            buttons.Add(applyButton);
            buttons.Add(cancelOptionsButton);

            yellowBlockRectangle = new Rectangle(0, Game1.viewport.Y / 3, Game1.viewport.X, Game1.viewport.Y);
            titleRectangle = new Rectangle(Game1.viewport.X / 2 - 400, 100, 800, 200);
        }

        public void LoadContent(ContentManager Content)
        {
            menuFont = Content.Load<SpriteFont>("Fonts/MenuFont");
            UUlogo = Content.Load<Texture2D>("UUlogo");
            yellowBlock = Content.Load<Texture2D>("Yellowblock");
            title = Content.Load<Texture2D>("Title");
            popUp = Content.Load<Texture2D>("Buttons/PopUp");
            emptyButton = Content.Load<Texture2D>("Buttons/EmptyButton");

            playButton.texture = emptyButton;
            resumeButton.texture = emptyButton;
            optionsButton.texture = emptyButton;
            cancelButton.texture = emptyButton;
            okButton.texture = emptyButton;
            exitButton.texture = emptyButton;
            loadgameButton.texture = emptyButton;
            newgameButton.texture = emptyButton;
            savegameButton.texture = emptyButton;
            cancelPopUpButton.texture = emptyButton;
            cancelOptionsButton.texture = emptyButton;
            applyButton.texture = emptyButton;
            /*
            playButton.texture = Content.Load<Texture2D>("Buttons/PlayButton");
            resumeButton.texture = Content.Load<Texture2D>("Buttons/ResumeButton");
            optionsButton.texture = Content.Load<Texture2D>("Buttons/OptionsButton");
            cancelButton.texture = Content.Load<Texture2D>("Buttons/CancelButton");
            okButton.texture = Content.Load<Texture2D>("Buttons/OkButton");
            exitButton.texture = Content.Load<Texture2D>("Buttons/ExitButton");
            loadgameButton.texture = Content.Load<Texture2D>("Buttons/LoadGameButton");
            newgameButton.texture = Content.Load<Texture2D>("Buttons/NewGameButton");
            savegameButton.texture = Content.Load<Texture2D>("Buttons/SaveGameButton");
            */
        }

        public void Update(KeyboardState currKeyboardState, KeyboardState prevKeyboardState, MouseState currMouseState, MouseState prevMouseState, Game1 game)
        {
            if (menuState != newState)
            {
                prevMenuState = menuState;
                menuState = newState;
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].active = false;
                }
                if (menuState == MenuState.Main)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        buttons[i].active = true;
                    }
                }
                else if (menuState == MenuState.LoadGame)
                {
                    for (int i = 5; i < 8; i++)
                    {
                        buttons[i].active = true;
                    }
                }
                else if (menuState == MenuState.Pause)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        buttons[i].active = true;
                    }
                }
                else if (menuState == MenuState.Options)
                {
                    for (int i = 10; i < 12; i++)
                    {
                        buttons[i].active = true;
                    }
                }
            }

            if (popUpActive)
            {

            }

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Update(currMouseState, prevMouseState);
            }

            if (playButton.clicked)
            {
                newState = MenuState.LoadGame;
            }
            else if (exitButton.clicked)
            {
                if (menuState == MenuState.Main)
                {
                    game.Exit();
                }
                else if (menuState == MenuState.Pause)
                {
                    PopUp("All unsaved progress \n        will be lost");
                }
            }
            else if (cancelButton.clicked)
            {
                newState = MenuState.Main;
            }
            else if (newgameButton.clicked)
            {
                GameValues.state = GameState.select;
            }
            else if (resumeButton.clicked)
            {
                GameValues.state = GameState.select;
            }
            else if (optionsButton.clicked)
            {
                newState = MenuState.Options;
            }
            else if (cancelOptionsButton.clicked)
            {
                if (prevMenuState == MenuState.Pause)
                {
                    newState = MenuState.Pause;
                }
                else if (prevMenuState == MenuState.Main)
                {
                    newState = MenuState.Main;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(yellowBlock, yellowBlockRectangle, Color.White);
            spriteBatch.Draw(UUlogo, logoRectangle, Color.White);
            spriteBatch.Draw(title, titleRectangle, Color.White);
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].active)
                {
                    buttons[i].Draw(spriteBatch);
                }
            }
            if (popUpActive)
            {
                spriteBatch.Draw(popUp, popUpRectangle, Color.White);
                spriteBatch.DrawString(Game1.font, popUpText, new Vector2(500, 500), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
            }
        }

        public void PopUp(string popUpText)
        {
            popUpActive = true;
            this.popUpText = popUpText;
        }
    }
}
