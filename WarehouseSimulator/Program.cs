using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using WarehouseSimulator.Models;
using WarehouseSimulator.Services;
using WarehouseSimulator.Utils;

namespace WarehouseSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var options = new JsonSerializerOptions
            {
                Converters = { new TupleConverter() }
            };

            var warehouse = new Warehouse
            {
                Products = JsonSerializer.Deserialize<List<Product>>(File.ReadAllText("Data/Products.json"), options),
                Robots = JsonSerializer.Deserialize<List<Robot>>(File.ReadAllText("Data/Robots.json"), options),
                Employees = JsonSerializer.Deserialize<List<Employee>>(File.ReadAllText("Data/Employees.json"), options),
                Orders = JsonSerializer.Deserialize<List<Order>>(File.ReadAllText("Data/Orders.json"), options),
            };

            var commander = new Command(warehouse: warehouse);




            Console.WriteLine("All orders processed!");
            Console.ReadLine(); // Keep console open
        }
    }
}
