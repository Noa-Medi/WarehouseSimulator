using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSimulator.Models;
using WarehouseSimulator.Utils;

namespace WarehouseSimulator.Services
{
    public class OrderService
    {
        public static Warehouse warehouse { get; set; }

        public OrderService(Warehouse warehouse) => OrderService.warehouse = warehouse;
        public static void AddOrder(string customerEmail, List<OrderItem> orderItems) {
            Random random = new Random();
            int randomId = random.Next(100000, 999999);
            Order newOrder = new Order(OrderId : randomId , CustomerEmail : customerEmail , Items : orderItems);
            warehouse.Orders.Add(newOrder);
        }
        public static bool RemoveOrder(int orderId)
        {
           
                Order order = warehouse.Orders.First(o => o.OrderId == orderId);
                if (order != null) {
                warehouse.Orders.Remove(order);
                return true;

            }
            else
            {
                return false;
            }
        }
        public static async Task ProsseccOrder(Order order  ) {
       
            if (order != null && order.Status == "Pending")
            {
                foreach (OrderItem item in order.Items)
                {
                    order.Status = "Processing";
                    var product = warehouse.Products.FirstOrDefault(p => p.Id == item.Id);
                    if (product == null || product.Stock < item.Quantity)
                    {
                        Logger.Log($"⚠️ Out of stock: {product?.Name}. Restocking...", ConsoleColor.Red);
                        RestockProduct(product.Id, 10);
                        continue;
                    }

                    Robot robot = await RobotService.WaitForAvailableRobotAsync(warehouse.Robots);
                    var path = Pathfinder.FindPath(robot.CurrentPosition, product.ShelfLocation, warehouse.Grid);

                    Logger.Log($"🤖 Robot #{robot.Id} moving to {product.ShelfLocation}...", ConsoleColor.Yellow);

                    // Visualize each movement step
                    foreach (var step in path)
                    {
                        robot.CurrentPosition = step;
                        WarehouseVisualizer.DrawWarehouse(warehouse);
                        await Task.Delay(300); // Adjust speed as needed
                    }

                    robot.TakeItem(item, product);
                    WarehouseVisualizer.DrawWarehouse(warehouse);

                    // Move to packer (optional)
                    var packer = warehouse.Employees.FirstOrDefault(e => e.Job == "Packer" && e.IsAvailable);
                    if (packer != null)
                    {
                        var packerPath = Pathfinder.FindPath(robot.CurrentPosition, packer.Position, warehouse.Grid);
                        foreach (var step in packerPath)
                        {
                            robot.CurrentPosition = step;
                            WarehouseVisualizer.DrawWarehouse(warehouse);
                            await Task.Delay(300);
                        }
                        robot.PlaceItem(item, packer);
                        WarehouseVisualizer.DrawWarehouse(warehouse);
                    }

                    if (packer.Inventory.Count > 0)
                    {
                        await Task.Delay(300);
                        order.Status = "Packed";
                    }
                }

            }
        

    }

        private static void RestockProduct(int productId, int quantity)
        {
            var product = warehouse.Products.FirstOrDefault(p => productId == p.Id);
            product.Stock += quantity;
            Logger.Log($"🔄 Restocked {product.Name}. New stock: {product.Stock}", ConsoleColor.Blue);
        }

    }
}
