using System.Collections.Generic;

namespace WarehouseSimulator.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public List<OrderItem> Items { get; set; }
        public string Status { get; set; }
        public Order(int OrderId, string CustomerEmail, List<OrderItem> Items, string Status = "Pending")
        {
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
            string joinedItems = string.Join(",", ordersString);
            return $"{{ OrderID : {OrderId} }},{{ CustomerEmail : {CustomerEmail} }},{{ Items : [{joinedItems}] }},{{ Status : {Status} }}";
        }
    }




    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public OrderItem(int id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{{ Id : {Id} }} ,{{ Quantity : {Quantity} }}";
        }
    }


}
