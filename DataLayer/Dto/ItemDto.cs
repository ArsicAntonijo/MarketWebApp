using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dto
{
    public class ItemDto
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
