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
        public Order(int OrderId, string CustomerEmail, List<OrderItem> Items , string Status = "Pending") {
            this.OrderId = OrderId;
            this.CustomerEmail = CustomerEmail;
            this.Items = Items;
            this.Status = Status;
        }
        public override string ToString()
        {
            List<string> ordersString = new List<string>();
            foreach (OrderItem orderItem in Items)
            {
                ordersString.Add(orderItem.ToString());
            }
            string joinedItems = string.Join(",\n", ordersString);
            return $"{{ OrderID : {OrderId} }},\n{{ CustomerEmail : {CustomerEmail} }},\n{{ Items : [\n{joinedItems}\n] }},\n{{ Status : {Status} }}";
        }
    }




    public class OrderItem
    {
        public int Id { get; set; } 
        public int Quantity { get; set; }

        public OrderItem (int id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $" {{ Id : {Id} }} ,\n  {{ Quantity : {Quantity} }}";
        }
    }

    
}
