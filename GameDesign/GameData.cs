using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesign
{
    [System.Serializable]
    class GameData
    {
        public List<int> types = new List<int>();
        public List<int> zones = new List<int>();

        public GameData()
        {
            foreach (Tile t in GameValues.tiles)
            {
                switch (t.type)
                {
                    case Type.grass:
                        types.Add(0);
                        break;
                    case Type.wall:
                        types.Add(1);
                        break;
                    case Type.floor:
                        types.Add(2);
                        break;
                    case Type.ceiling:
                        types.Add(3);
                        break;
                    default:
                        break;
                }
                switch (t.zone)
                {
                    case Zone.Lesson:
                        zones.Add(0);
                        break;
                    case Zone.Break:
                        zones.Add(1);
                        break;
                    case Zone.Path:
                        zones.Add(2);
                        break;
                    case Zone.Road:
                        zones.Add(3);
                        break;
                    case Zone.Grass:
                        zones.Add(4);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
