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
        library,
        tramStation,
        bikeParking,
        carParking,
        cafe,
        foodcourt,
        pub,
        supplyShop,
        studentAssociationBuilding,
        gym,
        park,
        superMarket
    }
    public abstract class BuildingType
    {
        public BuildingTypes type;
        public int tileCount;
        public int happiness, productivity, studentPerformance, maintenanceCost, income, capacity;
        public bool active;
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
        public AdminBuilding()
        {
            type = BuildingTypes.adminBuilding;
        }
    }
    class LectureBuilding : BuildingType
    {
        public LectureBuilding()
        {
            type = BuildingTypes.lectureBuilding;
        }
    }
    class SeminarBuilding : BuildingType
    {
        public SeminarBuilding()
        {
            type = BuildingTypes.seminarBuilding;
        }
    }
    class ResearchBuilding : BuildingType
    {
        public ResearchBuilding()
        {
            type = BuildingTypes.researchBuilding;
        }
    }
    class Library : BuildingType
    {
        public Library()
        {
            type = BuildingTypes.library;
        }
    }
    class TramStation : BuildingType
    {
        public TramStation()
        {
            type = BuildingTypes.tramStation;
        }
    }
    class BikeParking : BuildingType
    {
        public BikeParking()
        {
            type = BuildingTypes.bikeParking;
        }

    }
    class CarParking : BuildingType
    {
        public CarParking()
        {
            type = BuildingTypes.carParking;
        }

    }
    class Cafe : BuildingType
    {
        public Cafe()
        {
            type = BuildingTypes.cafe;
        }

    }
    class FoodCourt : BuildingType
    {
        public FoodCourt()
        {
            type = BuildingTypes.foodcourt;
        }
    }
    class Pub : BuildingType
    {
        public Pub()
        {
            type = BuildingTypes.pub;
        }
    }
    class SupplyShop : BuildingType
    {
        public SupplyShop()
        {
            type = BuildingTypes.supplyShop;
        }
    }
    class StudentAssociationBuilding : BuildingType
    {
        public StudentAssociationBuilding()
        {
            type = BuildingTypes.studentAssociationBuilding;
        }

    }
    class Gym : BuildingType
    {
        public Gym()
        {
            type = BuildingTypes.gym;
        }

    }
    class Park : BuildingType
    {
        public Park()
        {
            type = BuildingTypes.park;
        }

    }
    class SuperMarket : BuildingType
    {
        public SuperMarket()
        {
            type = BuildingTypes.superMarket;
        }
    }
}
