using Business.Domain.ItemDomain;

namespace Business.Contracts;

public interface IItemRepository : IGenericRepository<PastBid>
{
    Task<List<PastBid>> GetLatest(int items = 5);
    Task<PastBid> Get(string bidId);
}

