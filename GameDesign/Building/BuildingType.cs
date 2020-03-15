using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesign
{
    public enum BuildingTypes
    {
        none,
        adminBuilding,
        lectureBuilding,
        seminarBuilding,
        researchBuilding,
        tramStation,
        busStation,
        bikeParking,
        carParking,
        cafe,
        foodcourt,
        pub,
        club,
        supplyShop,
        library,
        studentAssociationBuilding,
        gym,
        park,
        grassField,
        superMarket
    }
    public abstract class BuildingType
    {
        public BuildingTypes type;
        public int tileCount = 0;
        public int happiness, productivity, studentPerformance, maintenanceCost, income, capacity;
        public bool capIncrease = false, forStudents = true;
        public Point minSize;

        public BuildingType()
        {
            GameValues.buildingTypes.Add(this);
            minSize = new Point(4, 4);
        }
    }

    class None : BuildingType
    {
        public None()
        {
            type = BuildingTypes.none;
        }
    }
    class AdminBuilding : BuildingType
    {
        public int managementCap = 50;
        public AdminBuilding()
        {
            type = BuildingTypes.adminBuilding;
            maintenanceCost = 25;
            capacity = 3;
            capIncrease = true;
            forStudents = false;
        }
    }
    class LectureBuilding : BuildingType
    {
        public LectureBuilding()
        {
            type = BuildingTypes.lectureBuilding;
            studentPerformance = 15;
            maintenanceCost = 30;
            capacity = 4;
            capIncrease = true;
        }
    }
    class SeminarBuilding : BuildingType
    {
        public SeminarBuilding()
        {
            type = BuildingTypes.seminarBuilding;
            studentPerformance = 3;
            productivity = 8;
            maintenanceCost = 20;
            capacity = 4;
            capIncrease = true;
        }
    }
    class ResearchBuilding : BuildingType
    {
        public int research = 8;
        public ResearchBuilding()
        {
            type = BuildingTypes.researchBuilding;
            productivity = 6;
            maintenanceCost = 35;
            capacity = 4;
            capIncrease = true;
        }
    }
    class TramStation : BuildingType
    {
        public int growthRate = 3;
        public TramStation()
        {
            type = BuildingTypes.tramStation;
            maintenanceCost = 10;
        }
    }
    class BusStation : BuildingType
    {
        public int growthRate = 1;
        public BusStation()
        {
            type = BuildingTypes.busStation;
            maintenanceCost = 3;
        }
    }
    class BikeParking : BuildingType
    {
        public BikeParking()
        {
            type = BuildingTypes.bikeParking;
            maintenanceCost = 10;
            capacity = 30;
        }

    }
    class CarParking : BuildingType
    {
        public CarParking()
        {
            type = BuildingTypes.carParking;
            maintenanceCost = 15;
            capacity = 10;
            forStudents = false;
        }

    }
    class Cafe : BuildingType
    {
        public Cafe()
        {
            type = BuildingTypes.cafe;
            happiness = 5;
            maintenanceCost = 3;
            income = 4;
            capacity = 4;
        }

    }
    class FoodCourt : BuildingType
    {
        public FoodCourt()
        {
            type = BuildingTypes.foodcourt;
            happiness = 2;
            maintenanceCost = 6;
            income = 1;
            capacity = 10;
        }
    }
    class Pub : BuildingType
    {
        public Pub()
        {
            type = BuildingTypes.pub;
            happiness = 12;
            studentPerformance = -7;
            maintenanceCost = 32;
            income = 8;
            capacity = 3;
        }
    }
    class Club : BuildingType
    {
        Club()
        {
            type = BuildingTypes.club;
            happiness = 8;
            studentPerformance = -10;
            maintenanceCost = 55;
            income = 10;
            capacity = 9;
        }
    }
    class SupplyShop : BuildingType
    {
        public SupplyShop()
        {
            type = BuildingTypes.supplyShop;
            happiness = 1;
            productivity = 2;
            maintenanceCost = 10;
            income = 2;
            capacity = 4;
        }
    }
    class Library : BuildingType
    {
        public Library()
        {
            type = BuildingTypes.library;
            happiness = 3;
            productivity = 8;
            studentPerformance = 3;
            maintenanceCost = 20;
            income = 1;
            capacity = 5;
        }
    }
    class StudentAssociationBuilding : BuildingType
    {
        public StudentAssociationBuilding()
        {
            type = BuildingTypes.studentAssociationBuilding;
            happiness = 6;
            productivity = 2;
            studentPerformance = 2;
            maintenanceCost = 5;
            capacity = 5;
        }

    }
    class Gym : BuildingType
    {
        public Gym()
        {
            type = BuildingTypes.gym;
            happiness = 4;
            productivity = 2;
            studentPerformance = 2;
            maintenanceCost = 10;
            income = 2;
            capacity = 4;
        }

    }
    class Park : BuildingType
    {
        public Park()
        {
            type = BuildingTypes.park;
            happiness = 6;
            productivity = 2;
            studentPerformance = 2;
            maintenanceCost = 15;
            capacity = 4;
        }

    }
    class GrassField : BuildingType
    {
        GrassField()
        {
            type = BuildingTypes.grassField;
            happiness = 4;
            productivity = 2;
            studentPerformance = 2;
            maintenanceCost = 5;
            capacity = 6;
        }
    }
    class SuperMarket : BuildingType
    {
        public SuperMarket()
        {
            type = BuildingTypes.superMarket;
            happiness = 5;
            productivity = 2;
            studentPerformance = 2;
            maintenanceCost = 30;
            income = 5;
            capacity = 8;
        }
    }
}
