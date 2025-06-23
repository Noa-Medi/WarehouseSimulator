using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSimulator.Models;

namespace WarehouseSimulator.Services
{
    public static class WarehouseVisualizer
    {
        public static void DrawWarehouse(Warehouse warehouse)
        {
            Console.Clear();
            Console.WriteLine("=== WAREHOUSE LAYOUT ===");

            for (int y = 0; y < warehouse.Grid.GetLength(1); y++)
            {
                for (int x = 0; x < warehouse.Grid.GetLength(0); x++)
                {
                    var cell = warehouse.Grid[x, y];
                    var robot = warehouse.Robots.FirstOrDefault(r =>
                        r.CurrentPosition.x == x && r.CurrentPosition.y == y);
                    var product = warehouse.Products.FirstOrDefault(p =>
                        p.ShelfLocation.x == x && p.ShelfLocation.y == y);

                    if (robot != null)
                    {
                        Console.Write(" R "); // Robot
                    }
                    else if (cell == 1)
                    {
                        Console.Write(product != null ?
                            $"[I]" : "[ ]"); // Shelf with product
                    }
                    else if (cell == 2)
                    {
                        Console.Write("{$}"); // Shelf with product
                    }
                    else
                    {
                        Console.Write(" . "); // Empty space
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
