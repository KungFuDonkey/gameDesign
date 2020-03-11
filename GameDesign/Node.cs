using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDesign
{
    public class Node
    {
        public int x, y;
        public int gCost = int.MaxValue;
        public int hCost = int.MaxValue;
        public int fCost = int.MaxValue;

        public Node parent;

        public Node(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        
    }
}
