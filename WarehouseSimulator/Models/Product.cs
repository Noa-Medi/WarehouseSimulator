namespace WarehouseSimulator.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int? ShelfId { get; set; }
        public double Price { get; set; }

        public Product(int id, string name, int stock, int? shelfId, double price)
        {
            Id = id;
            Name = name;
            Stock = stock;
            ShelfId = shelfId;
            Price = price;
        }

        public override string ToString()
        {
            return $"{{ ID : {Id} }}, {{ Name : {Name} }}, {{ Stock : {Stock} }}, " +
                   $"{{ ShelfId : {(ShelfId.HasValue ? $"[{ShelfId.Value}]" : "null")}}}, " +
                   $"{{ Price : {Price} }}";
        }
    }
}