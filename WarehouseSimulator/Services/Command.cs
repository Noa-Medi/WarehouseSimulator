using System;
using System.Collections.Generic;
using System.Linq;
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
            EmployeeServic employeeServic = new EmployeeServic(warehouse: Warehouse);
            RobotService robotService = new RobotService(warehouse: Warehouse);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Warehouse Management System ===");
                Console.WriteLine("1. Orders\n2. Products\n3. Employees\n4. Robots\n5. Exit");
                Console.Write("Choose option: ");
                int.TryParse(Console.ReadLine(), out int mainChoice);

                switch (mainChoice)
                {
                    case 1: // Orders
                        await HandleOrdersMenu(orderService);
                        break;
                    case 2: // Products
                        HandleProductsMenu(productService);
                        break;
                    case 3: // Employees
                        HandleEmployeesMenu(employeeServic);
                        break;
                    case 4: // Robots
                        HandleRobotsMenu(robotService);
                        break;
                    case 5: // Exit
                        Console.WriteLine("Exiting system...");
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        WaitForUser();
                        break;
                }
            }
        }

        private async Task HandleOrdersMenu(OrderService orderService)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Orders Menu ===");
                Console.WriteLine("1. View Orders\n2. Add Order\n3. Remove Order\n4. Process Orders\n5. Back to Main Menu");
                Console.Write("Choose option: ");
                int.TryParse(Console.ReadLine(), out int orderChoice);

                switch (orderChoice)
                {
                    case 1: // View Orders
                        Console.Clear();
                        if (Warehouse.Orders.Count == 0)
                        {
                            Console.WriteLine("No orders available!");
                        }
                        else
                        {
                            foreach (Order order in Warehouse.Orders)
                            {
                                Console.WriteLine(order.ToString());
                            }
                        }
                        WaitForUser();
                        break;

                    case 2: // Add Order
                        Console.Clear();
                        Console.Write("Customer Email: ");
                        string email = Console.ReadLine();

                        Console.WriteLine("Available Products:");
                        foreach (Product product in Warehouse.Products)
                        {
                            Console.WriteLine(product.ToString());
                        }

                        List<OrderItem> orderList = new List<OrderItem>();
                        bool readyToOrder = true;
                        CatchOrderLoop(ref readyToOrder, ref orderList);

                        if (orderList.Count > 0)
                        {
                            Console.WriteLine("\nYour Order:");
                            Console.WriteLine($"Email: {email}");
                            foreach (OrderItem item in orderList)
                            {
                                Console.WriteLine(item.ToString());
                            }

                            Console.Write("Confirm order (Y/N)? ");
                            if (Console.ReadLine().ToLower() == "y")
                            {
                                orderService.AddOrder(email, orderList);
                                Console.WriteLine("Order added successfully!");
                            }
                        }
                        WaitForUser();
                        break;

                    case 3: // Remove Order
                        bool removing = true;
                        while (removing && Warehouse.Orders.Count > 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Current Orders:");
                            foreach (Order order in Warehouse.Orders)
                            {
                                Console.WriteLine(order.ToString());
                            }

                            Console.Write("Enter Order ID to remove (or 0 to cancel): ");
                            if (int.TryParse(Console.ReadLine(), out int orderId) && orderId != 0)
                            {
                                Order orderToRemove = Warehouse.Orders.Find(o => o.OrderId == orderId);
                                if (orderToRemove != null)
                                {
                                    Console.WriteLine($"\nOrder to remove:\n{orderToRemove}");
                                    Console.Write("Are you sure (Y/N)? ");
                                    if (Console.ReadLine().ToLower() == "y")
                                    {
                                        if (orderService.RemoveOrder(orderId))
                                        {
                                            Console.WriteLine("Order removed successfully!");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Order not found!");
                                }
                            }
                            else
                            {
                                removing = false;
                            }

                            if (Warehouse.Orders.Count > 0)
                            {
                                Console.Write("Remove another order (Y/N)? ");
                                removing = Console.ReadLine().ToLower() == "y";
                            }
                        }
                        break;

                    case 4: // Process Orders
                        Console.Clear();
                        if (Warehouse.Orders.Count > 0)
                        {
                            foreach (Order order in Warehouse.Orders.ToList()) // ToList to avoid modification issues
                            {
                                WarehouseVisualizer.DrawWarehouse(Warehouse);
                                await orderService.ProcessOrder(order);
                                Console.WriteLine($"Processed order {order.OrderId}");
                                WaitForUser();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No orders available to process!");
                            WaitForUser();
                        }
                        break;

                    case 5: // Back to Main Menu
                        return;

                    default:
                        Console.WriteLine("Invalid option!");
                        WaitForUser();
                        break;
                }
            }
        }

        private void HandleProductsMenu(ProductService productService)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Products Menu ===");
                Console.WriteLine("1. View Products\n2. Add Product\n3. Remove Product\n4. Back to Main Menu");
                Console.Write("Choose option: ");
                int.TryParse(Console.ReadLine(), out int productChoice);

                switch (productChoice)
                {
                    case 1: // View Products
                        Console.Clear();
                        if (Warehouse.Products.Count == 0)
                        {
                            Console.WriteLine("No products available!");
                        }
                        else
                        {
                            foreach (Product product in Warehouse.Products)
                            {
                                Console.WriteLine(product.ToString());
                            }
                        }
                        WaitForUser();
                        break;

                    case 2: // Add Product
                        Console.Clear();
                        Console.Write("Product Name: ");
                        string name = Console.ReadLine();

                        double price = GetValidDouble("Product Price: ");
                        int shelfId = GetValidInt("Shelf ID: ");
                        int stock = GetValidInt("Initial Stock: ");

                        productService.AddProduct(name, price, shelfId, stock);
                        Console.WriteLine("Product added successfully!");
                        WaitForUser();
                        break;

                    case 3: // Remove Product
                        Console.Clear();
                        if (Warehouse.Products.Count == 0)
                        {
                            Console.WriteLine("No products available to remove!");
                            WaitForUser();
                            break;
                        }

                        Console.WriteLine("Current Products:");
                        foreach (Product product in Warehouse.Products)
                        {
                            Console.WriteLine(product.ToString());
                        }

                        Console.Write("Enter Product ID to remove (or 0 to cancel): ");
                        if (int.TryParse(Console.ReadLine(), out int productId) && productId != 0)
                        {
                            Product productToRemove = Warehouse.Products.Find(p => p.Id == productId);
                            if (productToRemove != null)
                            {
                                Console.WriteLine($"\nProduct to remove:\n{productToRemove}");
                                Console.Write("Are you sure (Y/N)? ");
                                if (Console.ReadLine().ToLower() == "y")
                                {
                                    Warehouse.Products.Remove(productToRemove);
                                    Console.WriteLine("Product removed successfully!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Product not found!");
                            }
                        }
                        WaitForUser();
                        break;

                    case 4: // Back to Main Menu
                        return;

                    default:
                        Console.WriteLine("Invalid option!");
                        WaitForUser();
                        break;
                }
            }
        }

        private void HandleEmployeesMenu(EmployeeServic employeeService)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Employees Menu ===");
                Console.WriteLine("1. View Employees\n2. Add Employee\n3. Remove Employee\n4. Back to Main Menu");
                Console.Write("Choose option: ");
                int.TryParse(Console.ReadLine(), out int employeeChoice);

                switch (employeeChoice)
                {
                    case 1: // View Employees
                        Console.Clear();
                        if (Warehouse.Employees.Count == 0)
                        {
                            Console.WriteLine("No employees available!");
                        }
                        else
                        {
                            foreach (Employee employee in Warehouse.Employees)
                            {
                                Console.WriteLine(employee.ToString());
                            }
                        }
                        WaitForUser();
                        break;

                    case 2: // Add Employee
                        Console.Clear();
                        Console.Write("Employee Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Employee Job: ");
                        string job = Console.ReadLine();

                        int x = GetValidInt("Position X: ");
                        int y = GetValidInt("Position Y: ");

                        employeeService.AddEmployee(name, job, (x, y));
                        Console.WriteLine("Employee added successfully!");
                        WaitForUser();
                        break;

                    case 3: // Remove Employee
                        Console.Clear();
                        if (Warehouse.Employees.Count == 0)
                        {
                            Console.WriteLine("No employees available to remove!");
                            WaitForUser();
                            break;
                        }

                        Console.WriteLine("Current Employees:");
                        foreach (Employee employee in Warehouse.Employees)
                        {
                            Console.WriteLine(employee.ToString());
                        }

                        Console.Write("Enter Employee ID to remove (or 0 to cancel): ");
                        if (int.TryParse(Console.ReadLine(), out int employeeId) && employeeId != 0)
                        {
                            Employee employeeToRemove = Warehouse.Employees.Find(e => e.Id == employeeId);
                            if (employeeToRemove != null)
                            {
                                Console.WriteLine($"\nEmployee to remove:\n{employeeToRemove}");
                                Console.Write("Are you sure (Y/N)? ");
                                if (Console.ReadLine().ToLower() == "y")
                                {
                                    Warehouse.Employees.Remove(employeeToRemove);
                                    Console.WriteLine("Employee removed successfully!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Employee not found!");
                            }
                        }
                        WaitForUser();
                        break;

                    case 4: // Back to Main Menu
                        return;

                    default:
                        Console.WriteLine("Invalid option!");
                        WaitForUser();
                        break;
                }
            }
        }

        private void HandleRobotsMenu(RobotService robotService)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Robots Menu ===");
                Console.WriteLine("1. View Robots\n2. Add Robot\n3. Remove Robot\n4. Back to Main Menu");
                Console.Write("Choose option: ");
                int.TryParse(Console.ReadLine(), out int robotChoice);

                switch (robotChoice)
                {
                    case 1: // View Robots
                        Console.Clear();
                        if (Warehouse.Robots.Count == 0)
                        {
                            Console.WriteLine("No robots available!");
                        }
                        else
                        {
                            foreach (Robot robot in Warehouse.Robots)
                            {
                                Console.WriteLine(robot.ToString());
                            }
                        }
                        WaitForUser();
                        break;

                    case 2: // Add Robot
                        Console.Clear();
                        int speed = GetValidInt("Robot Speed: ");
                        int x = GetValidInt("Position X: ");
                        int y = GetValidInt("Position Y: ");

                        robotService.AddRobot((x, y), speed);
                        Console.WriteLine("Robot added successfully!");
                        WaitForUser();
                        break;

                    case 3: // Remove Robot
                        Console.Clear();
                        if (Warehouse.Robots.Count == 0)
                        {
                            Console.WriteLine("No robots available to remove!");
                            WaitForUser();
                            break;
                        }

                        Console.WriteLine("Current Robots:");
                        foreach (Robot robot in Warehouse.Robots)
                        {
                            Console.WriteLine(robot.ToString());
                        }

                        Console.Write("Enter Robot ID to remove (or 0 to cancel): ");
                        if (int.TryParse(Console.ReadLine(), out int robotId) && robotId != 0)
                        {
                            Robot robotToRemove = Warehouse.Robots.Find(r => r.Id == robotId);
                            if (robotToRemove != null)
                            {
                                Console.WriteLine($"\nRobot to remove:\n{robotToRemove}");
                                Console.Write("Are you sure (Y/N)? ");
                                if (Console.ReadLine().ToLower() == "y")
                                {
                                    Warehouse.Robots.Remove(robotToRemove);
                                    Console.WriteLine("Robot removed successfully!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Robot not found!");
                            }
                        }
                        WaitForUser();
                        break;

                    case 4: // Back to Main Menu
                        return;

                    default:
                        Console.WriteLine("Invalid option!");
                        WaitForUser();
                        break;
                }
            }
        }

        private void CatchOrderLoop(ref bool readyToOrder, ref List<OrderItem> orderList)
        {
            while (readyToOrder)
            {
                Console.WriteLine("\nEnter 'finish' to complete order or product ID to add item");
                Console.Write("Product ID: ");
                string input = Console.ReadLine();

                if (input?.ToLower() == "finish")
                {
                    readyToOrder = false;
                    break;
                }

                if (int.TryParse(input, out int productId))
                {
                    Product product = Warehouse.Products.Find(p => p.Id == productId);
                    if (product != null)
                    {
                        int quantity = GetValidInt("Quantity: ");
                        orderList.Add(new OrderItem(productId, quantity));
                        Console.WriteLine($"Added {quantity}x {product.Name} to order");
                    }
                    else
                    {
                        Console.WriteLine("Product not found!");
                    }
                }
            }
        }

        private int GetValidInt(string prompt)
        {
            int value;
            do
            {
                Console.Write(prompt);
            } while (!int.TryParse(Console.ReadLine(), out value));
            return value;
        }

        private double GetValidDouble(string prompt)
        {
            double value;
            do
            {
                Console.Write(prompt);
            } while (!double.TryParse(Console.ReadLine(), out value));
            return value;
        }

        private void WaitForUser()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}