using Business.Domain.BidDomain;
using MediatR;

namespace Services.BidsModule.Queries.LatestBids;

public record GetLatestBidsQuery(List<Bid>? RunningBids = null) : IRequest<List<Bid>>;

