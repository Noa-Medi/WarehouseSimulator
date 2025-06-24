using System;
using WarehouseSimulator.Models;

namespace WarehouseSimulator.Services
{
    public class ProductService
    {
        public Warehouse Warehouse { get; } // Made readonly

        public ProductService(Warehouse warehouse) => Warehouse = warehouse;

        public void AddProduct(string productName, double price, int? shelfId, int stock = 20)
        {
            var random = new Random();
            int randomId = random.Next(100000, 999999);
            var product = new Product(
                id: randomId,
                name: productName,
                stock: stock, // Use the parameter instead of hardcoded 20
                price: price,
                shelfId: shelfId
                );

            Warehouse.Products.Add(product);
        }

        public bool RemoveProduct(int productId)
        {
            var product = Warehouse.Products.Find(p => p.Id == productId);
            if (product != null)
            {
                Warehouse.Products.Remove(product);
                Shelf shelf = Warehouse.Shelves.Find(s => s.Id == product.ShelfId);
                shelf?.Inventory.Remove(product); //                   TODO: must to put in ShelfService to removing product
                return true;
            }
            return false;
        }
    }
}