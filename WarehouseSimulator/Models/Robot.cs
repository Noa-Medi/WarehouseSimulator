using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using WarehouseSimulator.Utils;

namespace WarehouseSimulator.Models
{
    public class Robot
    {
        public int Id { get; set; }
        public bool IsAvailable { get; set; } = true;
        public (int x, int y) CurrentPosition { get; set; }
        public List<OrderItem> Inventory { get; set; } = new List<OrderItem>();

        // Movement speed in milliseconds per cell
        public int MovementDelayMs { get; set; } = 1000;

        public override string ToString()
        {
            return $"{{ ID : {Id} }} ,\n {{ IsAvailable : {IsAvailable} }} ,\n {{ Inventory : {Inventory} }} ,\n {{ CurrentPosition : {CurrentPosition} }} ,\n {{ MovementDelayMs : {MovementDelayMs} }}";
        }

        public async Task MoveAlongPath(List<(int x, int y)> path)
        {
            IsAvailable = false;

            Logger.Log($"🤖 Robot #{Id}: Starting movement along {path.Count} cells path...",
                      ConsoleColor.Yellow);

            foreach ((int x, int y) pos in path)
            {
                CurrentPosition = pos;

                // Simulate time taken to move between cells
                await Task.Delay(MovementDelayMs);

                Logger.Log($"🤖 Robot #{Id}: Moved to position ({pos.x},{pos.y})",
                          ConsoleColor.DarkYellow);
            }

            Logger.Log($"🤖 Robot #{Id}: Completed movement to final position ({CurrentPosition.x},{CurrentPosition.y})",
                      ConsoleColor.Green);

            IsAvailable = true;
        }

        public void TakeItem(OrderItem orderItem, Product product)
        {
            Inventory.Add(orderItem);
            product.Stock -= orderItem.Quantity;
            Logger.Log($"🤖 Robot #{Id}: Taken {orderItem.Quantity}x {product.Name} from shelf",
                      ConsoleColor.Cyan);
        }

        public void PlaceItem(OrderItem orderItem, Employee employee)
        {
            if (Inventory.Contains(orderItem))
            {
                Logger.Log($"🤖 Robot #{Id}: Placing item #{orderItem.Id} with employee {employee.Name}",
                          ConsoleColor.Magenta);
                employee.Inventory.Add(orderItem);
                Inventory.Remove(orderItem);
            }
        }

    }
}