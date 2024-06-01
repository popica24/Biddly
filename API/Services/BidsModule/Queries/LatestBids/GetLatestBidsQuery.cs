using Business.Domain.BidDomain;
using MediatR;
using Services.Common.DTO.Bid;

namespace Services.BidsModule.Queries.LatestBids;

public record GetLatestBidsQuery(List<Bid>? RunningBids = null) : IRequest<List<Bid>>;

