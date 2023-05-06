using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class Repository
    {
        protected MarketApiContext _context;

        public Repository(MarketApiContext context)
        {
            _context = context;
        }

        public MarketApiContext GetContext()
        {
            return _context;
        }
    }
}
