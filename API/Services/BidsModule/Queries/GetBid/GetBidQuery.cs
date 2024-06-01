using Business.Domain.BidDomain;
using MediatR;

namespace Services.BidsModule.Queries.GetBid;

public record GetBidQuery(string BidId) : IRequest<Bid>;

