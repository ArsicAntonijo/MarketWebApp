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

        public async Task<string> PostReceipt(Receipt r)
        {
            _context.Receipt.Add(r);
            await _context.SaveChangesAsync();

            return string.Empty;
        }
    }
}
