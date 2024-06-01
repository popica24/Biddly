
using Licenta.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Services.CacheService;
using Services.Common.DTO.Bid;
using Services.Utils;

namespace Licenta.Services;

public class BidMonitoringService(IHubContext<BidHub, IBidsHubClient> biddingHub, ICacheService cacheService, ISender sender) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
       /* while(!stoppingToken.IsCancellationRequested)
        {
            string _bidsIdJson = cacheService.GetData<string>(GlobalConstants.RedisKeys.RunningBids);

            var _bidsId = new List<string>();

            if (!string.IsNullOrEmpty(_bidsIdJson))
            {
                _bidsId = JsonConvert.DeserializeObject<List<string>>(_bidsIdJson);
            }

            var now = DateTime.UtcNow;

            foreach(var bidId in _bidsId.ToList())
            {
                var bidModelJson = cacheService.GetData<string>(GlobalConstants.RedisKeys.BidId(bidId));

                var bidModel = JsonConvert.DeserializeObject<RunningBid>(bidModelJson);

                if (bidModel.EndingAt <= now) {
                    await biddingHub.Clients.All.BidEnded(GlobalConstants.RedisKeys.BidId(bidId));
                    await biddingHub.Clients.All.UpdateLatestBids();

                    cacheService.RemoveData(GlobalConstants.RedisKeys.BidId(bidId));

                    _bidsId.Remove(bidId);
                    var newBidsJson = JsonConvert.SerializeObject(_bidsId);
                    cacheService.SetData(GlobalConstants.RedisKeys.RunningBids, newBidsJson, DateTime.Now.AddDays(7));
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }*/
    }
}
