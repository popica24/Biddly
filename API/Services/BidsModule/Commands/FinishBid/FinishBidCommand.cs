using Business.Domain.BidDomain;
using MediatR;

namespace Services.BidsModule.Commands.RemoveBid;

public record FinishBidCommand(Bid bid) : IRequest<bool>;

