namespace FrontEnd.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal AmauntAvailable { get; set; }
        public string MeasurementUnit { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Tax { get; set; }
        public decimal PriceWithTax { get; set; }
    }
}
