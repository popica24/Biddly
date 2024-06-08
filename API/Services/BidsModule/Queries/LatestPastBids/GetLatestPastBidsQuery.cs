using Business.Domain.ItemDomain;
using MediatR;

namespace Services.BidsModule.Queries.LatestPastBids;

public record GetLatestPastBidsQuery(List<PastBid>? PastBids = null) : IRequest<List<PastBid>>;

