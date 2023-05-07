using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Confirmed { get; set; }


        public Customer Customer { get; set; }
        public ICollection<OrderedItem>? OrderedItems { get; set; }

    }
}
