using MediatR;

namespace Services.UserModule.Commands.UpdatePassword;

public record UpdatePasswordRequest(string userId, string oldPassword, string newPassword):IRequest<bool>;
