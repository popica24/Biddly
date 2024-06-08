using Business.Contracts;
using Business.Domain.ItemDomain;
using MediatR;

namespace Services.BidsModule.Queries.LatestPastBids;

public sealed class GetLatestPastBidsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLatestPastBidsQuery, List<PastBid>>
{
    public async Task<List<PastBid>> Handle(GetLatestPastBidsQuery request, CancellationToken cancellationToken)
    {
        var pastBids = await unitOfWork.ItemRepository.GetLatest();

        return pastBids;
    }
}
