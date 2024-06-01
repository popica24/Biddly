using Business.Domain.BidDomain;

namespace Business.Contracts;

public interface IBidRepository
{
    IEnumerable<string> GetRunningBids();

    bool SetRunninBid(Bid value, DateTimeOffset expirationTime);

    bool RemoveRunningBid(string bidId);

    bool PushRunningBid(string value);

    bool BidToItem(string bidId, string userId, long ammount);

    Bid? GetBid(string bidId);
}
