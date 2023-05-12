using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using DataLayer.Models;
using Service;
using AutoMapper;
using DataLayer.Dto;

namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private SalesService _service;
        private IMapper _mapper;

        public SalesController(SalesService service, IMapper mapper)
        {
            //            _service = new SalesService(context);
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Customers
        [HttpGet("Customer")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {

            return Ok(await _service.GetAllCustomers());
        }

        // GET: api/Customers/5
        [HttpGet("Customer/{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _service.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // POST: api/Customers/
        [HttpPost("Customer/Log")]
        public async Task<ActionResult<string>> CheckCustomer(CustomerDto c)
        {
            var customer = await _service.CheckCustomer(c.Email, c.Password);
            return customer;
        }


        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Customer")]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _ = await _service.PostCustomer(customer);           

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Customer/{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            string output =  await _service.UpdateCustomer(customer);

            //try
            //{
            //    await _service.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CustomerExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            if (!string.IsNullOrEmpty(output))
            {
                return NotFound();
            }
            return NoContent();
        }


        // DELETE: api/Customers/5
        [HttpDelete("Customer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var s = await _service.DeleteCustomer(id);
            if (!string.IsNullOrEmpty(s))
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/Items
        [HttpGet("Item")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItem()
        {
            var items = await _service.GetAllItems();
            return Ok(items);
        }

        // GET: api/Items/5
        [HttpGet("Item/{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _service.GetItem(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Item/{id}")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.ItemId)
            {
                return BadRequest();
            }

            string output = await _service.UpdateItem(item);
                
            if (!string.IsNullOrEmpty(output))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Item")]
        public async Task<ActionResult<Item>> PostItem(ItemDto item)
        {
            var i = _mapper.Map<Item>(item);
            _ =  await _service.PostItem(i);
            
            return CreatedAtAction("GetItem", new { id = item.ItemId }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("Item/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _service.DeleteItem(id);
            if (!string.IsNullOrEmpty(item))
            {
                return NotFound();
            }

            return NoContent();
        }

        // order staff 
        // GET: api/Orders
        [HttpGet("Order")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            return Ok(await _service.GetAllOrders());
        }

        // GET: api/Orders/5
        [HttpGet("Order/{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _service.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Order")]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _ = await _service.PostOrder(order);

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // PUT: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Order")]
        public async Task<IActionResult> PutOrder(OrderDto order)
        {
            string s = await _service.PutOrder(order);

            if (!s.Equals(""))
            {
                return BadRequest(order.OrderId);
            }
            else
            {
                return Ok();
            }
        }

        // DELETE: api/Orders/5
        [HttpDelete("Order/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var item = await _service.DeleteOrder(id);
            if (!string.IsNullOrEmpty(item))
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
