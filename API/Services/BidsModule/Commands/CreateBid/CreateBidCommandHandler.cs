using Business.Contracts;
using Business.Domain.BidDomain;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Services.BidsModule.Commands.CreateBid;

public sealed class CreateBidCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateBidCommand, bool>
{
    public async Task<bool> Handle(CreateBidCommand request, CancellationToken cancellationToken)
    {
        var bidModel = new Bid
        {
            BidId = request.BidId,
            CreatedBy = request.CreatedBy,
            HighestBid = request.HighestBid,
            StartingFrom = request.StartingFrom,
            WonAt = request.WonAt,
            WonBy = request.WonBy,
        };

        unitOfWork.BidRepository.SetRunninBid(bidModel, DateTime.Now.AddDays(7));

        unitOfWork.BidRepository.PushRunningBid(bidModel.BidId);

        return true;
    }
}

