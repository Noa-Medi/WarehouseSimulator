using System;
using System.Linq;
using WarehouseSimulator.Models;

namespace WarehouseSimulator.Services
{
    public class EmployeeService
    {
        public Warehouse Warehouse { get; set; }

        public EmployeeService(Warehouse warehouse) => this.Warehouse = warehouse;

        public void AddEmployee(string name, string job, (int x, int y) position)
        {
            Random random = new Random();
            int randomId = random.Next(100000, 999999);
            Employee newEmployee = new Employee(id: randomId, name: name, job, position);
            Warehouse.Employees.Add(newEmployee);
        }

        public bool RemoveEmployee(int employeeId)
        {
            Employee employee = Warehouse.Employees.First(e => e.Id == employeeId);
            if (employee != null)
            {
                Warehouse.Employees.Remove(employee);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
