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
    public class Node : IHeapItem<Node>
    {
        public int x, y;
        int heapIndex;
        public int gCost = int.MaxValue;
        public int hCost = int.MaxValue;
        public int fCost = int.MaxValue;

        public Node parent;

        public int CompareTo(Node nodeToCompare)
        {
            int compare = fCost.CompareTo(nodeToCompare.fCost);
            if (compare == 0)
            {
                compare = hCost.CompareTo(nodeToCompare.hCost);
            }
            return -compare;
        }
        public int HeapIndex
        {
            get
            {
                return heapIndex;
            }
            set
            {
                heapIndex = value;
            }
        }

        public Node(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}