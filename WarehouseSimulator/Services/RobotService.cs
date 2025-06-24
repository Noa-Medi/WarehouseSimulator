using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseSimulator.Models;
using WarehouseSimulator.Utils;

namespace WarehouseSimulator.Services
{
    public class RobotService
    {
        public Warehouse Warehouse { get; set; }

        public RobotService(Warehouse warehouse) => this.Warehouse = warehouse;
        public void AddRobot((int x, int y) currentPosition, int movementDelayMs)
        {
            Random random = new Random();
            int randomId = random.Next(100000, 999999);
            Robot newRobot = new Robot(id: randomId, currentPosition: currentPosition, movementDelayMs: movementDelayMs);
            Warehouse.Robots.Add(newRobot);
        }

        public bool RemoveRobot(int robotId)
        {
            Robot robot = Warehouse.Robots.First(r => r.Id == robotId);
            if (robot != null)
            {
                Warehouse.Robots.Remove(robot);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Robot> WaitForAvailableRobotAsync(List<Robot> Robots)
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

        public Robot FirstAvailable(IEnumerable<Robot> robots)
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
