using Business.Contracts;
using Business.Domain.BidDomain;
using MediatR;

namespace Services.BidsModule.Queries.LatestBids;

public sealed class GetLatestBidsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLatestBidsQuery, List<Bid>>
{
    public Task<List<Bid>> Handle(GetLatestBidsQuery request, CancellationToken cancellationToken)
    {
        var runningBids = unitOfWork.BidRepository.GetRunningBids();

        List<string> latestBidsIds = runningBids.TakeLast(5).ToList();

        List<Bid> latestBids = [];

        foreach (var id in latestBidsIds)
        {
            var bid = unitOfWork.BidRepository.GetBid(id);

            if (bid != null) {
                latestBids.Add(bid);
            }
        }

        return Task.FromResult(latestBids);
    }
}
