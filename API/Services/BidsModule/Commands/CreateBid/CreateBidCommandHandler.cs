using Business.Contracts;
using Business.Domain.BidDomain;
using MediatR;

namespace Services.BidsModule.Commands.CreateBid;

public sealed class CreateBidCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateBidCommand, bool>
{
    public Task<bool> Handle(CreateBidCommand request, CancellationToken cancellationToken)
    {
        var bidModel = new Bid
        {
            BidId = request.BidId,
            CreatedBy = request.CreatedBy,
            HighestBid = request.HighestBid,
            ItemName = request.ItemName,
            StartingFrom = request.StartingFrom,
            WonAt = request.WonAt,
            WonBy = request.WonBy,
        };

        unitOfWork.BidRepository.SetRunninBid(bidModel, DateTime.Now.AddDays(7));

        unitOfWork.BidRepository.PushRunningBid(bidModel.BidId);

        return Task.FromResult(true);
    }
}

