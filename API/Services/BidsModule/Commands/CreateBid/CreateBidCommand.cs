using MediatR;

namespace Services.BidsModule.Commands.CreateBid;

public record CreateBidCommand(string BidId, string CreatedBy, string WonBy, DateTime WonAt, long HighestBid, long StartingFrom) : IRequest<bool>;


