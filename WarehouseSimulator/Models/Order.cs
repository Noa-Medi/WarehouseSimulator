using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSimulator.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public List<OrderItem> Items { get; set; }
        public string Status { get; set; }
        public Order(int OrderId, string CustomerEmail, List<OrderItem> Items) {
            this.OrderId = OrderId;
            this.CustomerEmail = CustomerEmail;
            this.Items = Items;
        }
        public override string ToString()
        {
            return $"{{ OrderID : {OrderId } }} , {{ CustomerEmail : {CustomerEmail} }} , {{ Items : {Items} }} , {{ Status : {Status} }}";
        }
    }




    public class OrderItem
    {
        public int Id { get; set; } 
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{{ Id : {Id} }} , {{ Quantity : {Quantity} }}";
        }
    }

    
}
