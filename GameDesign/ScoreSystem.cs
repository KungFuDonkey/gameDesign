using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesign
{
    public class ScoreSystem
    {
        public int happiness, studentPerformance, studentProductivity, workerProductivity; //Get values from all buildings, 100 max
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
            UpdateValues();
            CalculateScore();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameValues.tileTex, scoreRectangle, Color.White);
            spriteBatch.DrawString(GameValues.font, score.ToString(), drawStringVector, scoreColor);
        }

        void CalculateScore()
        {
            if (GameValues.students > 0)
            {
                studentGrades = ((studentPerformance + studentProductivity) / 2 - (100 - workerProductivity)) * (happiness / 1000) / 80;
            }
            else
            {
                studentGrades = 0;
            }

            if (GameValues.researchBuilding.tileCount > 0)
            {
                researchScore = (GameValues.workers * workerProductivity) / 100 * (happiness / 1000) / 80;
            }
            else
            {
                researchScore = 0;
            }

            capacityScore = (GameValues.students + GameValues.workers) / 2;

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

        public void UpdateValues()
        {
            happiness = studentPerformance = studentProductivity = workerProductivity = 0;
            GameValues.CountTypes();
            GameValues.students = 0;
            GameValues.workers = 0;
            foreach (Tile t in GameValues.grid)
            {
                if (t.buildingType.capIncrease)
                {
                    if (t.buildingType.forStudents)
                    {
                        GameValues.students += t.buildingType.capacity;
                    }
                    else
                    {
                        GameValues.workers += t.buildingType.capacity;
                    }
                }
                if (t.buildingType.forStudents)
                {
                    studentProductivity += t.buildingType.productivity;
                }
                else
                {
                    workerProductivity += t.buildingType.productivity;
                }
                happiness += t.buildingType.happiness;
                studentPerformance += t.buildingType.studentPerformance;
            }
        }
    }
}
