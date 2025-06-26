using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using WarehouseSimulator.Models;
using WarehouseSimulator.Services;
using WarehouseSimulator.Utils;

namespace WarehouseSimulator
{
    internal class Program
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            Converters = { new TupleConverter() }
        };

        static void Main(string[] args)
        {
            var warehouse = InitializeWarehouseFromJson();
            var commander = new Command(warehouse);

            Console.WriteLine("All orders processed!");
            Console.ReadLine(); // Keep console open
        }

        private static Warehouse InitializeWarehouseFromJson()
        {
            try
            {
                return new Warehouse
                {
                    Products = DeserializeFromFile<List<Product>>("Data/Products.json"),
                    Robots = DeserializeFromFile<List<Robot>>("Data/Robots.json"),
                    Employees = DeserializeFromFile<List<Employee>>("Data/Employees.json"),
                    Orders = DeserializeFromFile<List<Order>>("Data/Orders.json"),
                    Shelves = DeserializeFromFile<List<Shelf>>("Data/Shelves.json")
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading warehouse data: {ex.Message}");
                throw;
            }
        }

        private static T DeserializeFromFile<T>(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json, JsonOptions);
        }
    }
}