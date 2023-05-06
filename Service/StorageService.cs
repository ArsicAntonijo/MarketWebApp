using DataLayer.Models;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class StorageService
    {

        private readonly StorageRepository _storageRepo;

        public StorageService(StorageRepository srepo)
        {
            _storageRepo = srepo;
        }

        public Task<IEnumerable<Receipt>> GetAll()
        {
            return _storageRepo.GetAll();
        }

        public Task<string> PostReceipt(Receipt receipt)
        {
            return _storageRepo.PostReceipt(receipt);
        }
    }
}
