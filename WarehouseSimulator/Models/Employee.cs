using System.Collections.Generic;

namespace WarehouseSimulator.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; } = true;

        public string Job { get; set; }
        public List<OrderItem> Inventory { get; set; }

        public (int x, int y) Position { get; set; }


        public Employee(int id, string name, string job, (int x, int y) position, bool isAvailable = true)
        {
            Id = id;
            Name = name;
            Inventory = new List<OrderItem>();
            Job = job;
            Position = position;
            IsAvailable = isAvailable;

        }

        public override string ToString()
        {
            List<string> inventoryString = new List<string>();
            foreach (OrderItem orderItem in Inventory)
            {
                inventoryString.Add(orderItem.ToString());
            }
            string joinedItems = string.Join(",\n", inventoryString);
            return $"{{ ID : {Id} }} ,{{ Name : {Name} }} ,{{ IsAvailable : {IsAvailable} }} ,{{ Job : {Job} }} ,{{ Inventory : {(Inventory.Count == 0 ? "Null" : joinedItems)} }} ,{{ Position : {Position} }}";
        }
    }
}
