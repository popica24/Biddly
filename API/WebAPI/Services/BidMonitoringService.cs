using Licenta.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Services.BidsModule.Commands.RemoveBid;
using Services.BidsModule.Queries.LatestBids;

namespace Licenta.Services
{
    public class BidMonitoringService : BackgroundService
    {
        private readonly IHubContext<BidHub, IBidsHubClient> _biddingHub;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BidMonitoringService(IHubContext<BidHub, IBidsHubClient> biddingHub, IServiceScopeFactory serviceScopeFactory)
        {
            _biddingHub = biddingHub;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var sender = scope.ServiceProvider.GetRequiredService<ISender>();

                    var getRunningBidsQuery = new GetLatestBidsQuery();
                    var runningBids = await sender.Send(getRunningBidsQuery, stoppingToken);

                    var now = DateTime.Now;

                    foreach (var bidModel in runningBids.ToList())
                    {
                        if (bidModel != null && bidModel.WonAt <= now)
                        {
                            await _biddingHub.Clients.All.BidEnded(bidModel.BidId);
                            await _biddingHub.Clients.All.UpdateLatestBids();

                            var removeBidCommand = new FinishBidCommand(bidModel);
                            await sender.Send(removeBidCommand, stoppingToken);
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
