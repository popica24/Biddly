using Business.Contracts;
using Business.Domain.BidDomain;
using MapsterMapper;
using MediatR;
using Services.Utils;

namespace Services.BidsModule.Queries.GetBid;

public sealed class GetBidQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBidQuery, Bid?>
{
    public async Task<Bid?> Handle(GetBidQuery request, CancellationToken cancellationToken)
    {
        var bid = unitOfWork.BidRepository.GetBid(GlobalConstants.RedisKeys.BidId(request.BidId));

        if (bid != null)
        {
            return await Task.FromResult(bid);
        }
        else { return null; }
        
    }
}

