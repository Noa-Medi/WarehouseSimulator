using System;
using WarehouseSimulator.Models;

namespace WarehouseSimulator.Services
{
    public class ShelfService
    {
        public Warehouse Warehouse { get; set; }

        public ShelfService(Warehouse warehouse) => Warehouse = warehouse;

        public (int x, int y) GetShelfLocation(int shelfId)
        {
            Shelf shelf = Warehouse.Shelves.Find(s => s.Id == shelfId);
            if (shelf == null)
                throw new ArgumentException($"Shelf with ID {shelfId} not found.");
            return shelf.Location;
        }
    }
}
