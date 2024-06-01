using MediatR;

namespace Services.UserModule.Commands.RefreshToken;

public record RefreshTokenCommand(string Email) : IRequest<string>;
