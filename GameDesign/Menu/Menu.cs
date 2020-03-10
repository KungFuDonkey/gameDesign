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
        Options
    }
    public class Menu
    {
        public MenuState menuState = MenuState.Loading, prevMenuState, newMenuState;
        public OptionsMenu optionsMenu;
        public PauseMenu pauseMenu;
        public LoadGameMenu loadGameMenu;
        public MainMenu mainMenu;

        Rectangle logoRectangle = new Rectangle(10, 10, 400, 110), yellowBlockRectangle, titleRectangle, popUpRectangle = new Rectangle(Game1.viewport.X / 2 - 200, 200, 400, 250);
        public Texture2D UUlogo, yellowBlock, title, popUp, emptyButton;
        public Button playButton, resumeButton, optionsButton, cancelButton, okButton, exitButton, loadgameButton, newgameButton, savegameButton, applyButton, cancelOptionsButton, cancelPopUpButton;
        List<Button> buttons = new List<Button>();
        Point buttonSize = new Point(252, 101);
        bool popUpActive;
        string popUpText;
        public SpriteFont menuFont = Game1.font;

        public Menu()
        {
            newMenuState = MenuState.Main;
            playButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X / 2, 300), buttonSize), "PLAY");
            resumeButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X / 2, 300), buttonSize), "RESUME");
            optionsButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X / 2, 450), buttonSize), "OPTIONS");
            cancelButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X / 2, 750), buttonSize), "CANCEL");
            okButton = new Button(new Rectangle(new Point(200, 600), buttonSize), "OK");
            cancelPopUpButton = new Button(new Rectangle(new Point(200, 600), buttonSize), "CANCEL");
            exitButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X / 2, 600), buttonSize), "EXIT");
            loadgameButton = new Button(new Rectangle(new Point(150, 550), buttonSize), "LOAD GAME");
            newgameButton = new Button(new Rectangle(new Point(150, 350), buttonSize), "NEW GAME");
            savegameButton = new Button(new Rectangle(new Point(Game1.viewport.X - 150 - buttonSize.X, 350), buttonSize), "SAVE GAME");
            applyButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X - 50, 750), buttonSize), "APPLY");
            cancelOptionsButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 + 50, 750), buttonSize), "CANCEL");
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

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].texture = emptyButton;
            }

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
            if (menuState != newMenuState)
            {
                prevMenuState = menuState;
                menuState = newMenuState;
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].active = false;
                }
                switch (menuState)
                {
                    case MenuState.Main:
                        for (int i = 0; i < 3; i++)
                        {
                            buttons[i].active = true;
                        }
                        break;
                    case MenuState.Pause:
                        for (int i = 1; i < 5; i++)
                        {
                            buttons[i].active = true;
                        }
                        break;
                    case MenuState.Loading:
                        break;
                    case MenuState.LoadGame:
                        for (int i = 5; i < 8; i++)
                        {
                            buttons[i].active = true;
                        }
                        break;
                    case MenuState.Options:
                        for (int i = 10; i < 12; i++)
                        {
                            buttons[i].active = true;
                        }
                        break;
                    default:
                        break;
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
                newMenuState = MenuState.LoadGame;
            }
            else if (exitButton.clicked)
            {
                if (menuState == MenuState.Main)
                {
                    game.Exit();
                }
                else if (menuState == MenuState.Pause)
                {
                    string[] text = new string[]{ "All unsaved progress", "will be lost!" };
                    PopUp(text);
                }
            }
            else if (cancelButton.clicked)
            {
                newMenuState = MenuState.Main;
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
                newMenuState = MenuState.Options;
            }
            else if (cancelOptionsButton.clicked)
            {
                if (prevMenuState == MenuState.Pause)
                {
                    newMenuState = MenuState.Pause;
                }
                else if (prevMenuState == MenuState.Main)
                {
                    newMenuState = MenuState.Main;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(yellowBlock, yellowBlockRectangle, Color.White);
            spriteBatch.Draw(title, titleRectangle, Color.White);
            spriteBatch.Draw(UUlogo, logoRectangle, Color.White);
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
                spriteBatch.DrawString(Game1.font, popUpText, new Vector2(Game1.viewport.X / 2 - Game1.font.MeasureString(popUpText).X, 250), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
            }
        }

        public void PopUp(string[] popUpText)
        {
            popUpActive = true;
            this.popUpText = popUpText[0];
            for (int i = 1; i < popUpText.Length; i++)
            {
                float space = (Game1.font.MeasureString(popUpText[0]).X - Game1.font.MeasureString(popUpText[i]).X) / 10;
                this.popUpText += "\n";
                for (int j = 0; j < space; j++)
                {
                    this.popUpText += " ";
                }
                this.popUpText += popUpText[i];
            }
        }
    }
}
