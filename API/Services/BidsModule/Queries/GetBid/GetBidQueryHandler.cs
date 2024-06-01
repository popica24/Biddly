using Business.Contracts;
using Business.Domain.BidDomain;
using MediatR;
using Services.Utils;

namespace Services.BidsModule.Queries.GetBid;

public sealed class GetBidQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBidQuery, Bid>
{
    public async Task<Bid> Handle(GetBidQuery request, CancellationToken cancellationToken)
    {
        var bid = unitOfWork.BidRepository.GetBid(GlobalConstants.RedisKeys.BidId(request.BidId));

        return await Task.FromResult(bid);
    }
}
    
