using DataLayer.Data;
using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SalesService
    {
        private readonly SalesRepository _repo;

        public SalesService(SalesRepository repo)
        {
            _repo = repo;
        }

        public MarketApiContext getContext()
        {
            return _repo.GetContext();
        }

        // Customer work 
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _repo.GetAllCustomers();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _repo.GetCustomer(id);
        }

        public async Task<string> PostCustomer(Customer c)
        {

            return await _repo.PostCustomer(c);
        }
        public async Task<string> CheckCustomer(string email, string pass)
        {
            return await _repo.CheckCustomer(email, pass);
        }
        public async Task<string> UpdateCustomer(Customer c)
        {
            return await _repo.UpdateCustomer(c);
        }
        public async Task<string> DeleteCustomer(int id)
        {
            return await _repo.DeleteCustomer(id);
        }

        // Items work
        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _repo.GetAllItems();
        }

        public async Task<Item> GetItem(int id)
        {
            return await _repo.GetItem(id);
        }

        public async Task<string> PostItem(Item i)
        {
            return await _repo.PostItem(i);
        }
        public async Task<string> UpdateItem(Item i)
        {
            return await _repo.UpdateItem(i);
        }
        public async Task<string> DeleteItem(int id)
        {
            return await _repo.DeleteItem(id);
        }

        // order staff
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _repo.GetAllOrders();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _repo.GetOrder(id);
        }

        public async Task<string> PostOrder(Order i)
        {
            return await _repo.PostOrder(i);
        }

        public async Task<string> DeleteOrder(int id)
        {
            return await _repo.DeleteOrder(id);
        }
    }
}
