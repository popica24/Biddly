using MediatR;
using Services.Common.DTO.Bid;

namespace Services.BidsModule.Queries.GetPastBid;

public record GetPastBidQuery(string bidId) : IRequest<PastBidResponse?>;

