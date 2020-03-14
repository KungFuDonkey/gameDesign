using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesign
{
    /*
    public abstract class Building
    {
        public BuildingType type;
        public int happiness, productivity, studentPerformance, maintenanceCost, income, capacity;
        public int tileCount;
        public bool forStudents = true;
        public Building(BuildingType type, int happiness, int capacity, int maintenanceCost, int productivity = 0, int studentPerformance = 0, int income = 0)
        {
            this.type = type;
            this.happiness = happiness;
            this.productivity = productivity;
            this.studentPerformance = studentPerformance;
            this.maintenanceCost = maintenanceCost;
            this.income = income;
            this.capacity = capacity;
        }

        public void Update()
        {

        }
    }
    */
    public abstract class Building
    {
        public BuildingTypes type;
        public int tileCount;
        public int happiness, productivity, studentPerformance, maintenanceCost, income, capacity;

        public Building(BuildingTypes type)
        {
            
        }
    }
}

