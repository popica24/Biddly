
using Licenta.Hubs;
using Licenta.Models.Bidl;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Services.CacheService;

namespace Licenta.Services;

public class BidMonitoringService(IHubContext<BidHub, IBidsHubClient> biddingHub, ICacheService cacheService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            string _bidsIdJson = cacheService.GetData<string>("latest-bids");

            var _bidsId = new List<string>();

            if (!string.IsNullOrEmpty(_bidsIdJson))
            {
                _bidsId = JsonConvert.DeserializeObject<List<string>>(_bidsIdJson);
            }

            var now = DateTime.UtcNow;

            foreach(var bidId in _bidsId.ToList())
            {
                var bidModelJson = cacheService.GetData<string>($"bid-{bidId}");
                var bidModel = JsonConvert.DeserializeObject<BidModel>(bidModelJson);
                if (bidModel.EndsAt <= now) {

                    await biddingHub.Clients.All.BidEnded($"bid-{bidId}");
                    await biddingHub.Clients.All.UpdateLatestBids();
                    cacheService.RemoveData($"bid-{bidId}");
                    _bidsId.Remove(bidId);
                    var newBidsJson = JsonConvert.SerializeObject(_bidsId);
                    cacheService.SetData("latest-bids", newBidsJson, DateTime.Now.AddDays(7));
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
