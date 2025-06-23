using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSimulator.Models;
using WarehouseSimulator.Utils;

namespace WarehouseSimulator.Services
{
    public class RobotService
    {
        public static async Task<Robot> WaitForAvailableRobotAsync(List<Robot> Robots)
        {
            while (true)
            {
                var availableRobot = Robots.FirstOrDefault(r => r.IsAvailable);
                if (availableRobot != null)
                {
                    return availableRobot;
                }

                Logger.Log("⏳ Waiting for an available robot...", ConsoleColor.DarkYellow);
                await Task.Delay(1000); // Check every second
            }
        }
    }

}
