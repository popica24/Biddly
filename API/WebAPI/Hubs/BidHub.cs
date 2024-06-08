using Microsoft.AspNetCore.SignalR;

namespace Licenta.Hubs;

public class BidHub : Hub<IBidsHubClient>
{
    public async Task UpdateLatestBids()
    {
        await Clients.All.UpdateLatestBids();
    }

    public async Task UpdateHighstBid()
    {
        await Clients.All.UpdateHighestBid();
    }

    public async Task BidEnded(string bidId)
    {
        await Clients.All.BidEnded(bidId);
    }

    public async Task AnnounceWinner(string bidId)
    {
        await Clients.All.AnnounceWinner(bidId);
    }
}
