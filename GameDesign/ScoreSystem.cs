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
        public int happiness, studentPerformance, studentCapacity, studentProductivity, workerCapacity, workerProductivity; //Get values from all buildings, 100 max
        public int studentGrades, capacityScore, researchScore; //Calculate form values

        public bool researchBuildings; //Got to be imported but no class yet

        public int score;

        Rectangle scoreRectangle;
        Vector2 drawStringVector;
        Color scoreColor;

        public ScoreSystem()
        {
            scoreRectangle = new Rectangle(240, 10, 30, 30);
            drawStringVector = new Vector2(scoreRectangle.X + 5, scoreRectangle.Y + 5);
        }

        public void Update()
        {
            if (GameValues.students > 0)
            {
                studentGrades = ((studentPerformance + studentProductivity) / 2 - (100 - workerProductivity)) * happiness / 80;
            }
            else
            {
                studentGrades = 0;
            }

            if (researchBuildings)
            {
                researchScore = (workerCapacity * workerProductivity) / 100 * happiness / 80;
            }
            else
            {
                researchScore = 0;
            }

            capacityScore = (studentCapacity + workerCapacity) / 2;

            if (studentGrades > 100)
                studentGrades = 100;
            if (researchScore > 100)
                researchScore = 100;
            if (capacityScore > 100)
                capacityScore = 100;

            score = (studentGrades * 5 + researchScore * 3 + capacityScore) / 9;

            if (score < 50)
                scoreColor = Color.Red;
            else
                scoreColor = Color.Black;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameValues.tileTex, scoreRectangle, Color.White);
            spriteBatch.DrawString(GameValues.font, score.ToString(), drawStringVector, scoreColor);
        }

        public void addResearchScore(int amount)
        {
            researchScore += amount;
        }
    }
}
