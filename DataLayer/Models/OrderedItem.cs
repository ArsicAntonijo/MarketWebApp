
namespace DataLayer.Models
{
    public class OrderedItem
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public decimal Amaunt { get; set; }

        public Order? Order { get; set; }
        public Item? Item { get; set; }
    }
}
