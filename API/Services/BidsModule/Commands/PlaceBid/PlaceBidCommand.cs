using MediatR;

namespace Services.BidsModule.Commands.PlaceBid;
public record PlaceBidCommand(string BidId, string BidderId, int Ammount) : IRequest<bool>;

