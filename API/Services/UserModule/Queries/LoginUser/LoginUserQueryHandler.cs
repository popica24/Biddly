using Business.Contracts;
using MapsterMapper;
using MediatR;
using Services.Authentication;

namespace Services.UserModule.Queries.LoginUser;

internal class LoginUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthTokenService authTokenService) : IRequestHandler<LoginUserQuery, string>
{
    public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserByEmail(request.Email);

        if (user is not null && BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            var accessToken = await authTokenService.CreateTokenAsync(user);
            return accessToken;
        }

        return "Invalid email or password";

    }
}
