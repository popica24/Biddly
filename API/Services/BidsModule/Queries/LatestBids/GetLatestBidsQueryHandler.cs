using Business.Contracts;
using Business.Domain.BidDomain;
using MediatR;
using Newtonsoft.Json;
using Services.CacheService;
using Services.Common.DTO.Bid;
using Services.Utils;

namespace Services.BidsModule.Queries.LatestBids;

public sealed class GetLatestBidsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLatestBidsQuery, List<Bid>>
{
    public async Task<List<Bid>> Handle(GetLatestBidsQuery request, CancellationToken cancellationToken)
    {
        var runningBids = unitOfWork.BidRepository.GetRunningBids();

        List<string> latestBidsIds = runningBids.TakeLast(5).ToList();

        List<Bid> latestBids = new List<Bid>();

        foreach (var id in latestBidsIds)
        {
            var bid = unitOfWork.BidRepository.GetBid(GlobalConstants.RedisKeys.BidId(id));

            if (bid != null) {
                latestBids.Add(bid);
            }
        }

        return latestBids;
    }
}
