using DataLayer.Data;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class SalesRepository : Repository
    {
        public SalesRepository(MarketApiContext context)
           : base(context) { }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _context.Customer.FirstAsync(x => x.Id == id);
        }

        public async Task<string> PostCustomer(Customer c)
        {
            _context.Customer.Add(c);
            await _context.SaveChangesAsync();

            return string.Empty;
        }

        public async Task<string> CheckCustomer(string email, string pass)
        {
            var customer = _context.Customer.Where(x => x.Email == email && x.Password == pass).FirstOrDefault();

            
            if (customer == null)
            {
                return string.Empty;
            }

            return "" + customer.Id;
        }

        public async Task<string> DeleteCustomer(int id) 
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return "NotFound";
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return string.Empty;
        }

        public async Task<string> UpdateCustomer(Customer c) 
        { 
            if (_context.Customer.Any(e => e.Id == c.Id))
            {
                _context.Entry(c).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return string.Empty;
            }
            else
            {
                return "customer not found";
            }
        }

        // items
        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _context.Item.ToListAsync();
        }

        public async Task<Item> GetItem(int id)
        {
            return await _context.Item.FirstAsync(x => x.ItemId == id);
        }

        public async Task<string> PostItem(Item c)
        {
            _context.Item.Add(c);
            await _context.SaveChangesAsync();

            return string.Empty;
        }

        public async Task<string> DeleteItem(int id)
        {
            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return "NotFound";
            }

            _context.Item.Remove(item);
            await _context.SaveChangesAsync();

            return string.Empty;
        }

        public async Task<string> UpdateItem(Item c)
        {
            if (_context.Item.Any(e => e.ItemId == c.ItemId))
            {
                _context.Entry(c).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return string.Empty;
            }
            else
            {
                return "Not Found";
            }
        }

        // order staff
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Order.Include(c => c.Customer).Include(o => o.OrderedItems).ToListAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            return  await _context.Order.Include(c => c.Customer).Include(o => o.OrderedItems).FirstAsync(x => x.OrderId == id);
        }

        public async Task<string> PostOrder(Order o)
        {
            var customer = _context.Customer.Where(c => c.Id == o.Customer.Id).FirstOrDefault();
            o.Customer = customer;

            var ois = o.OrderedItems;
            o.OrderedItems = null;

            foreach (var oi in ois)
            {
                decimal amaunt = oi.Amaunt;
                var item = _context.Item.Where(i => i.ItemId == oi.ItemId).FirstOrDefault();               

                var orderedItem = new OrderedItem()
                {
                    Order = o,
                    Item = item,
                    Amaunt = amaunt
                };
                _context.OrderedItem.Add(orderedItem);
            }

            _context.Order.Add(o);
            await _context.SaveChangesAsync();
            return string.Empty;
        }

        public async Task<string> DeleteOrder(int id)
        {
            var orderedItems = _context.OrderedItem.Where(o => o.OrderId == id).ToList();
            var order = await _context.Order.FindAsync(id);

            foreach (var oi in orderedItems)
            {
                _context.OrderedItem.Remove(oi);
            }

            if (order == null)
            {
                return "NotFound";
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return string.Empty;
        }
    }
}
