using Business.Contracts;
using Business.Domain.ItemDomain;
using MapsterMapper;
using MediatR;

namespace Services.BidsModule.Commands.RemoveBid;
public sealed class FinishBidCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<FinishBidCommand, bool>
{
    public async Task<bool> Handle(FinishBidCommand request, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(request.bid.WonBy) || request.bid.HighestBid == 0)
        {
            unitOfWork.BidRepository.RemoveRunningBid(request.bid.BidId);

            return false;
        }

        var item = mapper.Map<Item>(request.bid);

        var persistResult = await unitOfWork.ItemRepository.AddAsync(item);

        if (persistResult)
        {
            var result = unitOfWork.BidRepository.RemoveRunningBid(request.bid.BidId);
            return result;
        }
        return persistResult;
    }
}


