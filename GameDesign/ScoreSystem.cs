using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesign
{
    public class ScoreSystem
    {
        public int happiness;
        public int studentGrades;
        public int environment;

        public int score;

        Rectangle scoreRectangle;
        Vector2 drawStringVector;

        public ScoreSystem()
        {
            scoreRectangle = new Rectangle(240, 10, 30, 30);
            drawStringVector = new Vector2(scoreRectangle.X + 5, scoreRectangle.Y + 5);
        }

        public void Update()
        {
            if (GameValues.students == 0)
            {
                score = 0;
            }
            else
            {
                happiness = 50;
                studentGrades = 80;
                environment = 90;

                score = (happiness + studentGrades + environment) / 3;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameValues.tileTex, scoreRectangle, Color.White);
            spriteBatch.DrawString(GameValues.font, score.ToString(), drawStringVector, Color.Black);
        }
    }
}
