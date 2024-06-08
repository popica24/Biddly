using Business.Contracts;
using MapsterMapper;
using MediatR;
using Services.Common.DTO.Bid;
using Services.Utils;

namespace Services.BidsModule.Queries.GetPastBid;

internal class GetPastBidQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetPastBidQuery, PastBidResponse?>
{
    public async Task<PastBidResponse?> Handle(GetPastBidQuery request, CancellationToken cancellationToken)
    {
        var pastBid = await unitOfWork.ItemRepository.Get(GlobalConstants.RedisKeys.BidId(request.bidId));

        if (pastBid == null)
        {
            return null;
        }

        var pastBidModel = mapper.Map<PastBidResponse>(pastBid);

        return pastBidModel;
    }
}
