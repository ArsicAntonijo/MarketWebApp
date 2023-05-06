using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;

namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderedItemsController : ControllerBase
    {
        private readonly MarketApiContext _context;

        public OrderedItemsController(MarketApiContext context)
        {
            _context = context;
        }

        //// GET: api/OrderedItems
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<OrderedItem>>> GetOrderedItem()
        //{
        //    return await _context.OrderedItem.ToListAsync();
        //}

        //// GET: api/OrderedItems/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<OrderedItem>> GetOrderedItem(int id)
        //{
        //    var orderedItem = await _context.OrderedItem.FindAsync(id);

        //    if (orderedItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return orderedItem;
        //}

        //// PUT: api/OrderedItems/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOrderedItem(int id, OrderedItem orderedItem)
        //{
        //    if (id != orderedItem.ItemId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(orderedItem).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OrderedItemExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/OrderedItems
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<OrderedItem>> PostOrderedItem(OrderedItem orderedItem)
        //{
        //    _context.OrderedItem.Add(orderedItem);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetOrderedItem", new { id = orderedItem.ItemId }, orderedItem);
        //}

        //// DELETE: api/OrderedItems/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteOrderedItem(int id)
        //{
        //    var orderedItem = await _context.OrderedItem.FindAsync(id);
        //    if (orderedItem == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.OrderedItem.Remove(orderedItem);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool OrderedItemExists(int id)
        //{
        //    return _context.OrderedItem.Any(e => e.ItemId == id);
        //}
    }
}
