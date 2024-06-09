using MediatR;

namespace Services.UserModule.Commands.Logout;

public record LogoutCommand(string userId) : IRequest<bool>;

