using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseSimulator.Models;
using WarehouseSimulator.Utils;

namespace WarehouseSimulator.Services
{
    public class OrderService
    {
        public Warehouse Warehouse { get; set; }

        public OrderService(Warehouse warehouse) => this.Warehouse = warehouse;

        public void AddOrder(string customerEmail, List<OrderItem> orderItems)
        {
            Random random = new Random();
            int randomId = random.Next(100000, 999999);
            Order newOrder = new Order(OrderId: randomId, CustomerEmail: customerEmail, Items: orderItems);
            Warehouse.Orders.Add(newOrder);
        }
        public bool RemoveOrder(int orderId)
        {

            Order order = Warehouse.Orders.First(o => o.OrderId == orderId);
            if (order != null)
            {
                Warehouse.Orders.Remove(order);
                return true;

            }
            else
            {
                return false;
            }
        }
        public async Task ProcessOrder(Order order)
        {
            ShelfService shelfService = new ShelfService(warehouse: Warehouse);
            RobotService robotService = new RobotService(warehouse: Warehouse);

            if (order != null && order.Status == "Pending")
            {
                foreach (OrderItem item in order.Items)
                {
                    order.Status = "Processing";
                    var product = Warehouse.Products.FirstOrDefault(p => p.Id == item.Id);
                    if (product == null || product.Stock < item.Quantity)
                    {
                        Logger.Log($"⚠️ Out of stock: {product?.Name}. Restocking...", ConsoleColor.Red);
                        RestockProduct(product.Id, 10);
                        continue;
                    }

                    Robot robot = await robotService.WaitForAvailableRobotAsync(Warehouse.Robots);
                    if (product.ShelfId == null || !product.ShelfId.HasValue)
                    {
                        Logger.Log($"There is no assigned shelf for this Product {product.Name}", ConsoleColor.Red);
                        continue;
                    }
                    (int x, int y) productShelfLocation = shelfService.GetShelfLocation(shelfId: product.ShelfId.Value);


                    var path = Pathfinder.FindPath(robot.CurrentPosition, productShelfLocation, Warehouse.Grid);

                    Logger.Log($"🤖 Robot #{robot.Id} moving to {productShelfLocation}...", ConsoleColor.Yellow);

                    // Visualize each movement step
                    foreach (var step in path)
                    {
                        robot.CurrentPosition = step;
                        WarehouseVisualizer.DrawWarehouse(Warehouse);
                        await Task.Delay(300); // Adjust speed as needed
                    }

                    robot.TakeItem(item, product);
                    WarehouseVisualizer.DrawWarehouse(Warehouse);

                    // Move to packer (optional)
                    var packer = Warehouse.Employees.FirstOrDefault(e => e.Job == "Packer" && e.IsAvailable);
                    if (packer != null)
                    {
                        var packerPath = Pathfinder.FindPath(robot.CurrentPosition, packer.Position, Warehouse.Grid);
                        foreach (var step in packerPath)
                        {
                            robot.CurrentPosition = step;
                            WarehouseVisualizer.DrawWarehouse(Warehouse);
                            await Task.Delay(300);
                        }
                        robot.PlaceItem(item, packer);
                        WarehouseVisualizer.DrawWarehouse(Warehouse);
                    }

                    if (packer.Inventory.Count > 0)
                    {
                        await Task.Delay(300);
                        order.Status = "Packed";
                    }
                }

            }


        }

        private void RestockProduct(int productId, int quantity)
        {
            var product = Warehouse.Products.FirstOrDefault(p => productId == p.Id);
            product.Stock += quantity;
            Logger.Log($"🔄 Restocked {product.Name}. New stock: {product.Stock}", ConsoleColor.Blue);
        }

    }
}
