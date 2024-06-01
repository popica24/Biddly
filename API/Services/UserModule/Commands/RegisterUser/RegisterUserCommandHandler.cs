using Business.Contracts;
using Business.Domain.UserDomain;
using MediatR;

namespace Services.UserModule.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RegisterUserCommand, bool>
{
    public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        unitOfWork.BeginTransaction();

        var userCreated = await unitOfWork.UserRepository.AddAsync(new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = request.Username,
            Email = request.Email,
            PasswordHash = passwordHash
        });

        unitOfWork.CommitAndCloseConnection();

        return userCreated;
    }
}
