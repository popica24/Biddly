using MediatR;

namespace Services.UserModule.Commands.UpdateUsername;

public record UpdateUsernameRequest(string userId, string newUsername) : IRequest<bool>;

