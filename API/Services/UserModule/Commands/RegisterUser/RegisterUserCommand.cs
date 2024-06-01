using MediatR;

namespace Services.UserModule.Commands.RegisterUser;

public record RegisterUserCommand(string Username, string Email, string Password) : IRequest<bool>;
