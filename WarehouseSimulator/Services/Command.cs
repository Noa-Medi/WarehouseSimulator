using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WarehouseSimulator.Models;

namespace WarehouseSimulator.Services
{
    public class  Command
    {
        public Warehouse Warehouse { get; set; }
        public  Command(Warehouse warehouse)
        {
            this.Warehouse = warehouse;
            _ = RunMenuLoopAsync();
        }

        private async Task RunMenuLoopAsync()
        {


            while (true)
            {
                Console.WriteLine("1. Orders\n2. Products\n3. Employees\n4. Robots");
                Console.Write("Choose number : ");
                int.TryParse(Console.ReadLine(), out int nInput);

                switch (nInput)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("1. View Orders\n2. Add Order\n3. Remove Order\n4.Proccess Orders\n5.Back to Menu");
                        Console.Write("Choose number : ");
                        int.TryParse(Console.ReadLine(), out nInput);
                        switch (nInput)
                        {
                            case 1:
                                Console.Clear();
                                foreach (Order order in Warehouse.Orders)
                                {
                                    Console.WriteLine(order.ToString());
                                }
                                break;
                            case 2:
                                Console.Clear();
                                break;
                            case 3:
                                Console.Clear();
                                break;
                            case 4:
                                Console.Clear();
                                foreach (Order order in Warehouse.Orders)
                                {
                                    WarehouseVisualizer.DrawWarehouse(Warehouse);
                                    await OrderService.ProsseccOrder(order);
                                }
                                continue;
                            case 5:

                                break;


                        }

                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("1. View Products\n2. Add Product\n3. Remove Product\n4.Back to Menu");
                        Console.Write("Choose number : ");
                        int.TryParse(Console.ReadLine(), out nInput);
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("1. View Employees\n2. Add Employee\n3. Remove Employee\n4.Back to Menu");
                        Console.Write("Choose number : ");
                        int.TryParse(Console.ReadLine(), out nInput);
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("1. View Robots\n2. Add Robot\n3. Remove Robot\n4.Back to Menu");
                        Console.Write("Choose number : ");
                        int.TryParse(Console.ReadLine(), out nInput);
                        break;
                }
                

            }

        }
    }
}
