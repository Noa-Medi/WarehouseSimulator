namespace WarehouseSimulator.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }

        public int? ShelfId { get; set; }
        public (int x, int y)? ShelfLocation { get; set; }
        public double Price { get; set; }

        public Product(int id, string name, int stock, int? shelfId, (int x, int y)? shelfLocation, double price)
        {
            Id = id;
            Name = name;
            Stock = stock;
            ShelfId = shelfId;
            ShelfLocation = shelfLocation;
            Price = price;
        }

        public override string ToString()
        {
            return $"{{ ID : {Id} }}, {{ Name : {Name} }}, {{ Stock : {Stock} }}, " +
                   $"{{ ShelfId : {(ShelfId.HasValue ? $"[{ShelfId.Value}]" : "null")}}}, " +
                   $"{{ ShelfLocation : {(ShelfLocation.HasValue ? $"[{ShelfLocation.Value.x},{ShelfLocation.Value.y}]" : "null")}}}, " +
                   $"{{ Price : {Price} }}";
        }
    }
}