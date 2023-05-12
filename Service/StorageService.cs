using DataLayer.Models;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service
{
    public class StorageService
    {

        private readonly StorageRepository _storageRepo;

        public StorageService(StorageRepository srepo, SalesRepository salesRepo)
        {
            _storageRepo = srepo;
        }

        public Task<IEnumerable<Receipt>> GetAll()
        {
            return _storageRepo.GetAll();
        }

        public async Task<string> PostReceipt(Receipt receipt)
        {
            string izlaz = string.Empty;
            string s = receipt.StringItems;
            izlaz = _storageRepo.PostReceipt(receipt);
            if (izlaz == string.Empty)
            {

                izlaz = _storageRepo.NewItemsRecieved(ConvertToList(s));
            }
            return izlaz;
        }

        private List<OrderedItem> ConvertToList(string s)
        {
            List<OrderedItem> orderedItems = new List<OrderedItem>();
            Regex re = new Regex("(?<id>[0-9]+)-(?<amaunt>[0-9.]+)-(?<price>[0-9.]+)");

            try
            {
                foreach (Match match in re.Matches(s))
                {
                    var item = new OrderedItem();
                    item.ItemId = Convert.ToInt32(match.Groups[1].Value);
                    item.Amaunt = Convert.ToDecimal(match.Groups[2].Value);

                    orderedItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                //nista za sad
            }


            return orderedItems;
        }
    }
}
