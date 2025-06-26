using System.Collections.Generic;
using System.Linq;

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
            Job = job;
            Position = position;
            IsAvailable = isAvailable;
            Inventory = new List<OrderItem>();
        }

        public override string ToString() =>
            $"{{ ID: {Id} }}, {{ Name: {Name} }}, {{ IsAvailable: {IsAvailable} }}, " +
            $"{{ Job: {Job} }}, {{ Inventory: {(Inventory.Any() ? string.Join(",\n", Inventory) : "Empty")} }}, " +
            $"{{ Position: {Position} }}";
    }
}