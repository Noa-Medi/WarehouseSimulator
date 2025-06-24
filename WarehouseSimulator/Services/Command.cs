using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseSimulator.Models;

namespace WarehouseSimulator.Services
{
    public class Command
    {
        public Warehouse Warehouse { get; set; }
        public Command(Warehouse warehouse)
        {
            this.Warehouse = warehouse;
            _ = RunMenuLoopAsync();
        }

        private async Task RunMenuLoopAsync()
        {
            OrderService orderService = new OrderService(warehouse: Warehouse);
            ProductService productService = new ProductService(warehouse: Warehouse);

            while (true)
            {
                Console.WriteLine("1. Orders\n2. Products\n3. Employees\n4. Robots");
                Console.Write("Choose number : ");
                int.TryParse(Console.ReadLine(), out int nInput);

                switch (nInput)
                {
                    case 1://Orders
                        Console.Clear();
                        Console.WriteLine("1. View Orders\n2. Add Order\n3. Remove Order\n4.Proccess Orders\n5.Back to Menu");
                        Console.Write("Choose number : ");
                        int.TryParse(Console.ReadLine(), out nInput);
                        switch (nInput)
                        {
                            case 1: //View Orders
                                Console.Clear();
                                foreach (Order order in Warehouse.Orders)
                                {
                                    Console.WriteLine(order.ToString());
                                }
                                break;
                            case 2://Add Order
                                Console.Clear();
                                Console.Write("Email : ");
                                string email = Console.ReadLine();
                                foreach (Product product in Warehouse.Products)
                                {
                                    Console.WriteLine(product.ToString());
                                }
                                bool readyToOrder = true;
                                List<OrderItem> orderList = new List<OrderItem>();
                                CatchOrderLoop(ref readyToOrder, ref orderList);
                                if (orderList != null && orderList.Count > 0)
                                {
                                    Console.WriteLine($"Your Order :");
                                    Console.WriteLine($"Email : {email}");
                                    foreach (OrderItem orderItem in orderList)
                                    {
                                        Console.WriteLine($"{orderItem.ToString()}");
                                    }
                                    Console.Write($"Is that correct or you want to add again Y / N: ");
                                    var choose = Console.ReadLine();
                                    if (choose.ToLower() == "yes" || choose.ToLower() == "y")
                                    {
                                        orderList.Clear();

                                        readyToOrder = true;
                                        CatchOrderLoop(ref readyToOrder, ref orderList);
                                        Console.WriteLine("Added Successfully");
                                        Console.Clear();
                                    }
                                    orderService.AddOrder(customerEmail: email, orderList);
                                }
                                break;
                            case 3://Remove Order

                                bool isFinish = false;
                                while (!isFinish)
                                {
                                    if (Warehouse.Orders.Count > 0)
                                    {
                                        Console.Clear();
                                        foreach (Order orderItem in Warehouse.Orders)
                                        {
                                            Console.WriteLine(orderItem.ToString());
                                        }
                                        Console.Write("Which order to remove (Give order ID): ");
                                        int.TryParse(Console.ReadLine(), out int orderId);
                                        Order order = Warehouse.Orders.Find(o => o.OrderId == orderId);
                                        Console.Clear();
                                        if (order == null) continue;
                                        Console.WriteLine($"Are you sure do remove order:\n{order.ToString()}");
                                        Console.WriteLine($"Yes / No : ");
                                        string choose = Console.ReadLine();
                                        choose.ToLower();
                                        if (choose == "yes" || choose == "y")
                                        {
                                            bool result = orderService.RemoveOrder(order.OrderId);
                                            if (result == true)
                                            {
                                                Console.WriteLine("Removed Successfully");
                                                if (Warehouse.Orders.Count > 0)
                                                {
                                                    Console.Write("Do you want to continiue removing ? ");
                                                    string chooseCont = Console.ReadLine();
                                                    chooseCont.ToLower();
                                                    if (chooseCont == "n" || chooseCont == "no")
                                                    {
                                                        isFinish = true;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        continue;
                                                    }
                                                }
                                                else
                                                {
                                                    break;
                                                }

                                            }
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }


                                break;
                            case 4://Proccess Orders
                                //Console.Clear();
                                if (Warehouse.Orders.Count > 0)
                                {
                                    foreach (Order order in Warehouse.Orders)
                                    {
                                        WarehouseVisualizer.DrawWarehouse(Warehouse);
                                        await orderService.ProsseccOrder(order);
                                    }
                                }
                                else
                                {
                                    Console.Write("No Available Order !!");
                                }
                                continue;
                            case 5://Back to Menu

                                break;


                        }

                        break;
                    case 2://Products
                        Console.Clear();
                        Console.WriteLine("1. View Products\n2. Add Product\n3. Remove Product\n4.Back to Menu");
                        Console.Write("Choose number : ");
                        int.TryParse(Console.ReadLine(), out nInput);
                        switch (nInput)
                        {
                            case 1://View Products
                                Console.Clear();
                                foreach (Product product in Warehouse.Products)
                                {
                                    Console.WriteLine(product.ToString());
                                }
                                break;
                            case 2://Add 
                                Console.Write("Product name : ");
                                string productName = Console.ReadLine();

                                bool isFinish1 = true;
                                double productPrice;
                                do
                                {
                                    Console.Write("Product price: ");
                                    string price = Console.ReadLine();
                                    isFinish1 = double.TryParse(price, out productPrice);
                                } while (!isFinish1);

                                bool isFinish2 = true;
                                (int x, int y) productShelfLocation;
                                do
                                {
                                    Console.Write("Product shelf location: ");
                                    string locationString = Console.ReadLine();
                                    //isFinish1 = (int x, int y).TryParse(price, out productPrice);
                                } while (!isFinish1);

                                Console.Write("Product stock: ");
                                string productStock = Console.ReadLine();
                                //productService.AddProduct(productName: productName, price: productPrice, shelfLocation: productShelfLocation, stock: productStock);

                                break;
                            case 3://Remove Product

                                break;
                            case 4://Back to Menu

                                break;


                        }
                        break;
                    case 3://Employees
                        Console.Clear();
                        Console.WriteLine("1. View Employees\n2. Add Employee\n3. Remove Employee\n4.Back to Menu");
                        Console.Write("Choose number : ");
                        int.TryParse(Console.ReadLine(), out nInput);
                        switch (nInput)
                        {
                            case 1://View Products
                                Console.Clear();
                                foreach (Employee employee in Warehouse.Employees)
                                {
                                    Console.WriteLine(employee.ToString());
                                }
                                break;
                            case 2://Add Product

                                break;
                            case 3://Remove Product

                                break;
                            case 4://Back to Menu

                                break;


                        }
                        break;
                    case 4://Robots
                        Console.Clear();
                        Console.WriteLine("1. View Robots\n2. Add Robot\n3. Remove Robot\n4.Back to Menu");
                        Console.Write("Choose number : ");
                        int.TryParse(Console.ReadLine(), out nInput);
                        switch (nInput)
                        {
                            case 1://View Products
                                Console.Clear();
                                foreach (Robot robot in Warehouse.Robots)
                                {
                                    Console.WriteLine(robot.ToString());
                                }
                                break;
                            case 2://Add Product

                                break;
                            case 3://Remove Product

                                break;
                            case 4://Back to Menu

                                break;


                        }
                        break;
                }


            }

        }

        private void CatchOrderLoop(ref bool readyToOrder, ref List<OrderItem> orderList)
        {
            while (readyToOrder)
            {
                if (orderList.Count >= 1)
                {
                    Console.WriteLine("Do you want to order another Product? Y / N");
                    string sInput = Console.ReadLine();
                    sInput.ToLower();
                    if (sInput == "y" || sInput == "yes")
                    {
                        Console.Write("Product Id : ");
                        string productIdInput = Console.ReadLine();
                        if (productIdInput == "Finish") { readyToOrder = false; break; }
                        int.TryParse(productIdInput, out int productId);
                        if (Warehouse.Products.Find(p => p.Id == productId) != null)
                        {
                            Console.Write("How many : ");
                            string productCount = Console.ReadLine();
                            if (productCount == "Finish") { readyToOrder = false; break; }
                            int.TryParse(productCount, out int productCountInt);
                            OrderItem newOrderItem = new OrderItem(id: productId, quantity: productCountInt);
                            orderList.Add(newOrderItem);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("If you want to get back Type: Finish ");
                    Console.Write("Product Id : ");
                    string productIdInput = Console.ReadLine();
                    if (productIdInput == "Finish") { readyToOrder = false; break; }
                    int.TryParse(productIdInput, out int productId);
                    if (Warehouse.Products.Find(p => p.Id == productId) != null)
                    {
                        Console.Write("How many : ");
                        string productCount = Console.ReadLine();
                        if (productCount == "Finish") { readyToOrder = false; break; }
                        int.TryParse(productCount, out int productCountInt);
                        OrderItem newOrderItem = new OrderItem(id: productId, quantity: productCountInt);
                        orderList.Add(newOrderItem);
                    }
                }

            }
        }
    }
}
