using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSimulator.Utils
{
    public  class Node
    {
        public int x, y, gCost, hCost;
        public int FCost => gCost + hCost;

        public Node(int x, int y, int gCost, int hCost)
        {
            this.x = x;
            this.y = y;
            this.gCost = gCost;
            this.hCost = hCost;
        }
    }
}
