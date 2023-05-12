using Microsoft.AspNetCore.SignalR;
using Service;

namespace MarketApi.Hubs
{
    public class ItemHub : Hub
    {
        public static int Text { get; set; } = 0;
        SalesService _service;

        public ItemHub(SalesService salesService) {
            _service = salesService;
        }

        public async Task NewItemCreated()
        {
            Text++;
            // sendig to udapte
            await Clients.All.SendAsync("updateItemTable", Text);

        }
        public async Task NewItemCreated2()
        {
            var ob = await _service.GetAllItems();
            // sendig to udapte
            await Clients.All.SendAsync("updateItemTable2", ob);

        }
    }
}
