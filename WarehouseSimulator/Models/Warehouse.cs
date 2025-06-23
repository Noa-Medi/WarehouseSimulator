using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSimulator.Services;
using WarehouseSimulator.Utils;

namespace WarehouseSimulator.Models
{
    public class Warehouse
    {
        public List<Product> Products { get; set; }
        public List<Robot> Robots { get; set; }
        public List<Employee> Employees { get; set; } 
        public List<Order> Orders { get; set; }
        public int[,] Grid { get; set; } // 2D grid for pathfinding (shelf = 1 , empty = 0)
        public Warehouse()
        {
            Products = new List<Product>();
            Robots = new List<Robot>();
            Employees = new List<Employee>();
            Grid = new int[11, 11];  // 11 X 11  warehouse grid 
            InitializeShelves();
        }
        private void InitializeShelves()
        {
            for (int i = 1; i <= 10; i++) {
                for (int j = 1; j <= 10; j++) {
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        Grid[i, j] = 1;
                    }
                }
            }
            Grid[10, 10] = 2; // Emp Location

        }

        
        
        



       
    }
}
