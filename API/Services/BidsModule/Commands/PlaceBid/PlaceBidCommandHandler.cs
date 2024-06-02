using Business.Contracts;
using MediatR;

namespace Services.BidsModule.Commands.PlaceBid;

public sealed class PlaceBidCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<PlaceBidCommand, bool>
{

    public Task<bool> Handle(PlaceBidCommand request, CancellationToken cancellationToken)
    {
        var result = unitOfWork.BidRepository.BidToItem(request.BidId, request.BidderId, request.Ammount);

        return Task.FromResult(result);
    }
}

