namespace PresentationLayer.Models
{
    public class CustomersOrder
    {
        public string CustomerId { get; set; }
        public List<OrderedItem> Items { get; set; }

        public CustomersOrder() { }
        public CustomersOrder(string id) { 
            CustomerId = id;
            Items = new List<OrderedItem>();
        }
    }
}
