using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSimulator.Models;

namespace WarehouseSimulator.Utils
{
    public static class Extention
    {
        public static Robot FirstAvailable (this IEnumerable<Robot> robots)
        {
            Robot robot = robots.FirstOrDefault(r => r.IsAvailable); 
            if (robot == default)
            {
                Logger.Log($"No available Robots. :(");
                return robot;
            }
            else
            {
                return robot;
            }
        }
    }
}
