using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace rollercoaster_tycoon_ripoff
{
    class BreakRoom : Room
    {
        public BreakRoom(int _layer)
        {
            layer = _layer;
            layout = new List<Tile>();
            zone = Zone.Break;
            for(int i = 0; i < 5; i++)
            {
                layout.Add(new Wall(new Rectangle(i * GameValues.tileSize, 0, GameValues.tileSize, GameValues.tileSize),_layer,zone));
                layout.Add(new Wall(new Rectangle(i * GameValues.tileSize, 4 * GameValues.tileSize, GameValues.tileSize, GameValues.tileSize), _layer, zone));
            }
            for (int i = 0; i < 3; i++)
            {
                layout.Add(new Wall(new Rectangle(0, (i+1) * GameValues.tileSize, GameValues.tileSize, GameValues.tileSize), _layer, zone));
                layout.Add(new Wall(new Rectangle(4 * GameValues.tileSize, (i+1) * GameValues.tileSize, GameValues.tileSize, GameValues.tileSize), _layer, zone));
            }
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    layout.Add(new Floor(new Rectangle((j+1) * GameValues.tileSize, (i + 1) * GameValues.tileSize, GameValues.tileSize, GameValues.tileSize), _layer, zone));
                }
            }
        }
    }
}
