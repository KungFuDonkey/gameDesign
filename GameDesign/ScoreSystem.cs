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
        public int happiness, rawHappiness, studentPerformance, productivity; //Get values from all buildings, 100 max
        public int studentGrades, capacityScore, research; //Calculate form values

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
            int x = rawHappiness;
            happiness = (int) (50 + rawHappiness - ((0.5 * Math.Sqrt(x) + 0.3 * Math.Sin(0.4 * x) + 0.14 * Math.Cos(0.5 * x) + 0.05 * x + 10 + Math.Sin(0.1 * x)) * 2 + 0.00003 * x * x));

            if (GameValues.students > 0)
            {
                studentGrades = (productivity * GameValues.students) * (happiness / 10 + 1);
            }
            else
            {
                studentGrades = 0;
            }

            if (GameValues.researchBuilding.tileCount > 0)
            {
                research = (productivity * GameValues.workers) * (happiness / 10 + 1);
            }
            else
            {
                research = 0;
            }

            if (studentGrades > 100)
                studentGrades = 100;

            score = (int) (0.65 * research + 0.35 * studentPerformance);

            if (score < 30)
                scoreColor = Color.Red;
            else if (score < 70)
                scoreColor = Color.Black;
            else
                scoreColor = Color.Green;
        }

        public void UpdateValues()
        {
            happiness = rawHappiness = studentPerformance = productivity = 0;
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
                    productivity += t.buildingType.productivity;
                }
                else
                {
                    productivity += t.buildingType.productivity;
                }
                rawHappiness += t.buildingType.happiness;
                studentPerformance += t.buildingType.studentPerformance;
            }
        }
    }
}
