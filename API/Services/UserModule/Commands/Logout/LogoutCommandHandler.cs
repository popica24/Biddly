using Business.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Services.Utils;

namespace Services.UserModule.Commands.Logout;

public sealed class LogoutCommandHandler(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork) : IRequestHandler<LogoutCommand, bool>
{
    public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var httpContext = httpContextAccessor.HttpContext;

            var refreshToken = GetRefreshToken(httpContext);

            var tokenRemoved = await RemoveRefreshTokenFromDatabase(refreshToken, request.userId, unitOfWork);

            var cookieRemoved = RemoveRefreshTokenCookie(httpContext);

            return true;
        }
        catch (Exception) { 
            return false;
        }   

    }

    private static string GetRefreshToken(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            return "";
        }
        httpContext.Request.Cookies.TryGetValue("refreshToken", out string? refreshToken);
        if (string.IsNullOrEmpty(refreshToken))
        {
            return "";
        }
        return refreshToken;
    }

    private static bool RemoveRefreshTokenCookie(HttpContext httpContext)
    {
        try
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };

            httpContext.Response.Cookies.Append(
                GlobalConstants.RefreshTokenCookieKey,
                "",
                cookieOptions
                    );

            return true;
        }
        catch (Exception) {
            return false;
        }
    }

    private async static Task<bool> RemoveRefreshTokenFromDatabase(string refreshToken, string userId, IUnitOfWork unitOfWork)
    {
        var user = (await unitOfWork.UserRepository.GetByColumnAsync("id", userId)).First();

        if (user == null || user.RefreshToken != refreshToken)
        {
            return false;
        }
        unitOfWork.BeginTransaction();
        user.RefreshToken = "";
        user.RefreshTokenExpiryDate = new DateTime();

        var result = await unitOfWork.UserRepository.UpdateAsync(user);
        unitOfWork.CommitAndCloseConnection();
        return result;
    }
}
