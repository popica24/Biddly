namespace Licenta.Hubs;

public interface IBidsHubClient
{
    Task UpdateLatestBids();
    Task UpdateHighestBid();
    Task BidEnded(string bidId);
}
