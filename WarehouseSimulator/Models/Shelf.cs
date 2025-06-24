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


        public override string ToString()
        {
            List<string> shelvesString = new List<string>();
            foreach (Product product in Inventory)
            {
                shelvesString.Add(product.ToString());
            }
            string joinedItems = string.Join(",\n", shelvesString);
            return
                $"{{ ID : {Id} }} ,{{ Name : {Name} }} ,{{ Location : {Location} }} " +
                $"{{ Inventory : {joinedItems} }} ,{{ MaxCapacity : {MaxCapacity} }}"
                ;
        }

    }
}
