

namespace WarehouseSimulator.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public (int x, int y) ShelfLocation { get; set; }
        public double Price { get; set; }
        public override string ToString()
        {
            return $"ID : {Id} , Name : {Name} , Stock : {Stock} , ShelfLocation : [{ShelfLocation}] , Price : {Price}";
        }
    }

}