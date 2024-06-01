using MediatR;

namespace Services.ItemsModule.Commands;

public record AddItemQuery(string Id, string Name, string UserId, long BidWon, DateTime WonAt) : IRequest<bool>;

