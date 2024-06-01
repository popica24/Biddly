using Business.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Services.Authentication;
using Services.Utils;

namespace Services.UserModule.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IAuthTokenService authTokenService) : IRequestHandler<RefreshTokenCommand, string>
{
    public async Task<string> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = httpContextAccessor.HttpContext.Request.Cookies[GlobalConstants.RefreshTokenCookieKey];

        var user = await unitOfWork.UserRepository.GetUserByEmail(request.Email);

        if (user.RefreshToken is null || !user.RefreshToken.Equals(refreshToken)) {
            return "Refresh token is invalid";
        }
        else if(user.RefreshTokenExpiryDate < DateTime.UtcNow)
        {
            return "Token expired !";
        }

        var accessToken = await authTokenService.CreateTokenAsync(user);
        return accessToken;
    }
}

