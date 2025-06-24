using System.Collections.Generic;

namespace WarehouseSimulator.Models
{
    public class Shelf
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public (int x, int y) Location { get; set; }

        public List<Product> Inventory { get; set; }
        public int MaxCapacity { get; set; }

        public Shelf(int id, string name, (int x, int y) location)
        {
            this.Id = id;
            this.Name = name;
            this.Location = location;
            Inventory = new List<Product>();
        }




    }
}
