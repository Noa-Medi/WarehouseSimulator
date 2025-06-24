using System.Collections.Generic;

namespace WarehouseSimulator.Models
{
    public class Warehouse
    {
        public List<Product> Products { get; set; }
        public List<Robot> Robots { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Order> Orders { get; set; }

        private List<Shelf> _shelves;
        public List<Shelf> Shelves
        {
            get => _shelves;
            set
            {
                _shelves = value;
                UpdateGridWithShelves();
            }
        }

        public int[,] Grid { get; private set; } // 2D grid for pathfinding (shelf = 1, empty = 0)

        public Warehouse()
        {
            Products = new List<Product>();
            Robots = new List<Robot>();
            Employees = new List<Employee>();
            Orders = new List<Order>();
            _shelves = new List<Shelf>();
            Grid = new int[11, 11];  // 11 X 11 warehouse grid
            Grid[10, 10] = 2; // Employee location
        }

        public void AddShelf(Shelf shelf)
        {
            _shelves.Add(shelf);
            Grid[shelf.Location.x, shelf.Location.y] = 1;
        }

        public bool RemoveShelf(Shelf shelf)
        {
            if (_shelves.Remove(shelf))
            {
                Grid[shelf.Location.x, shelf.Location.y] = 0;
                return true;
            }
            return false;
        }

        private void UpdateGridWithShelves()
        {
            // Clear the grid (except employee location)
            Grid = new int[11, 11];
            Grid[10, 10] = 2;

            // Mark all shelf locations
            foreach (var shelf in _shelves)
            {
                if (IsWithinGrid(shelf.Location))
                {
                    Grid[shelf.Location.x, shelf.Location.y] = 1;
                }
            }
        }

        private bool IsWithinGrid((int x, int y) location)
        {
            return location.x >= 0 && location.x < Grid.GetLength(0) &&
                   location.y >= 0 && location.y < Grid.GetLength(1);
        }
    }
}
