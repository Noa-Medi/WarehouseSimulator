using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSimulator.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public bool IsAvailable { get; set; } = true;

        public string Job {  get; set; }
        public List<OrderItem> Inventory { get; set; }

        public (int x , int y) Position { get; set; }


        public Employee( string name , List<OrderItem> inventory) {
        
        Name = name;
        Inventory = inventory;
        }

        public override string ToString()
        {
            return $"{{ ID : {Id} }} ,\n{{ Name : {Name} }} ,\n{{ IsAvailable : {IsAvailable} }} ,\n{{ Job : {Job} }} ,\n{{ Inventory : {Inventory} }} ,\n{{ Position : {Position} }}";
        }
    }
}
