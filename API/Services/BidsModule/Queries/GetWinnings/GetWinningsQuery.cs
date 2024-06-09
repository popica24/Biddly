using Business.Domain.ItemDomain;
using MediatR;

namespace Services.BidsModule.Queries.GetWinnings;

public record GetWinningsQuery(string userId) : IRequest<IEnumerable<PastBid>>;
