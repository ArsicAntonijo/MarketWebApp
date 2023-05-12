using DataLayer.Data;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class StorageRepository : Repository
    {
        public StorageRepository(MarketApiContext context)
            : base(context) { }

        public async Task<IEnumerable<Receipt>> GetAll()
        {
            return await _context.Receipt.ToListAsync();
        }

        public string NewItemsRecieved(List<OrderedItem> orderedItems)
        {
            foreach (var oi in orderedItems)
            {
                Item item = _context.Item.Where(it => it.ItemId == oi.ItemId).FirstOrDefault();
                if (item != null)
                {
                    decimal newAmaunt = item.AmauntAvailable + oi.Amaunt;
                    item.AmauntAvailable = newAmaunt;
                    _context.Entry(item).State = EntityState.Modified;
                }
                else
                {
                    Item item1 = new Item();
                    item1.AmauntAvailable = oi.Amaunt;
                    _context.Item.Add(item1);
                }
            }
            var r = _context.SaveChanges();
            if (r > 0)
            {
                return string.Empty;
            }
            else
            {
                return "Error";
            }
        }

        public string PostReceipt(Receipt r)
        {
            _context.Receipt.Add(r);
            var i  = _context.SaveChanges();
            if (i > 0)
            {
                return string.Empty;
            }
            else
            {
                return "GRESKA";
            }
            
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
