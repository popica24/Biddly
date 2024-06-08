using MediatR;

namespace Services.BidsModule.Queries.GetWinner;

public record GetWinnerQuery(string bidId) : IRequest<string>;
