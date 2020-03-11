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

    public enum PopUpState
    {
        ExitPause,
        OverwriteSave,
        DeleteSave
    }

    public class Menu
    {
        public MenuState menuState = MenuState.Loading, prevMenuState, newMenuState;
        Rectangle logoRectangle = new Rectangle(10, 10, 400, 110), yellowBlockRectangle, titleRectangle, popUpRectangle;
        public Texture2D UUlogo, yellowBlock, title, popUp, emptyButton;
        public Button playButton, resumeButton, optionsButton, cancelButton, yesButton, exitButton, loadgameButton, newgameButton, savegameButton, applyButton, cancelOptionsButton, noButton;
        List<Button> buttons = new List<Button>();
        Point buttonSize = new Point(252, 101), none = new Point(0,0);
        bool popUpActive;
        string popUpText;
        public PopUpState popUpState;
        public SpriteFont menuFont = GameValues.font;

        public Menu()
        {
            newMenuState = MenuState.Main;
            playButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X / 2, 300), buttonSize), "PLAY");
            resumeButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X / 2, 300), buttonSize), "RESUME");
            optionsButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X / 2, 450), none), "");
            cancelButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X / 2, 750), buttonSize), "CANCEL");
            yesButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X - 50, 475), new Point(buttonSize.X / 2, buttonSize.Y / 2)), "YES");
            noButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 + 50, 475), new Point(buttonSize.X / 2, buttonSize.Y / 2)), "NO");
            exitButton = new Button(new Rectangle(new Point(Game1.viewport.X / 2 - buttonSize.X / 2, 600), buttonSize), "EXIT");
            loadgameButton = new Button(new Rectangle(new Point(150, 550), none), "");
            newgameButton = new Button(new Rectangle(new Point(150, 350), buttonSize), "NEW GAME");
            savegameButton = new Button(new Rectangle(new Point(Game1.viewport.X - 150 - buttonSize.X, 350), none), "");
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
            buttons.Add(yesButton);
            buttons.Add(noButton);
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
                for (int i = 8; i < 10; i++)
                {
                    buttons[i].active = true;
                    buttons[i].Update(currMouseState, prevMouseState);
                }
            }
            else
            {
                for (int i = 8; i < 10; i++)
                {
                    buttons[i].active = false;
                }
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].Update(currMouseState, prevMouseState);
                }
            }

            if (popUpActive)
            {
                if (noButton.clicked)
                {
                    popUpActive = false;
                }
                else if (yesButton.clicked)
                {
                    switch (popUpState)
                    {
                        case PopUpState.ExitPause:
                            menuState = MenuState.Main;
                            break;
                        case PopUpState.OverwriteSave:
                            break;
                        case PopUpState.DeleteSave:
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (playButton.clicked)
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
                    newMenuState = MenuState.Main;
                    /*
                    popUpState = PopUpState.ExitPause;
                    string[] text = new string[]{ "All unsaved progress", "will be lost!" };
                    PopUp(text);
                    */
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
            else if (savegameButton.clicked)
            {
                SaveSystem.SaveGame();
            }
            else if (loadgameButton.clicked)
            {
                GameValues.state = GameState.select;
                GameData data =  SaveSystem.LoadGame();
                int i = 0;
                foreach (Tile t in GameValues.tiles)
                {
                    t.type = (Type)data.types[i];
                    t.zone = (Zone)data.zones[i];
                    i++;
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
                spriteBatch.DrawString(GameValues.font, popUpText, new Vector2(popUpRectangle.X + 100, popUpRectangle.Y + 40), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                for (int i = 8; i < 10; i++)
                {
                    buttons[i].Draw(spriteBatch);
                }
            }
        }

        public void PopUp(string[] popUpText)
        {
            this.popUpText = popUpText[0];
            for (int i = 1; i < popUpText.Length; i++)
            {
                float space = (GameValues.font.MeasureString(popUpText[0]).X - GameValues.font.MeasureString(popUpText[i]).X) / 10;
                this.popUpText += "\n";
                for (int j = 0; j < space; j++)
                {
                    this.popUpText += " ";
                }
                this.popUpText += popUpText[i];
            }
            Point textSize = GameValues.font.MeasureString(this.popUpText).ToPoint();
            Point size = textSize + new Point(200, 160);
            Point location = new Point(Game1.viewport.X / 2 - size.X / 2, 200);
            popUpRectangle.Location = location;
            popUpRectangle.Size = size;
            yesButton.rectangle.Location = new Point(popUpRectangle.X, popUpRectangle.Bottom - yesButton.rectangle.Height / 2);
            noButton.rectangle.Location = new Point(popUpRectangle.Right - noButton.rectangle.Width, popUpRectangle.Bottom - noButton.rectangle.Height / 2);
            popUpActive = true;
        }
    }
}
