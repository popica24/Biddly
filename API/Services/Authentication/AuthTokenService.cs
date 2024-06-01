using Business.Contracts;
using Business.Domain.UserDomain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Common.ApplicationConfigOptions;
using Services.Common.DTO.Token;
using Services.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services.Authentication;

public sealed class AuthTokenService(IOptions<JwtOptions> jwtOptions, IHttpContextAccessor httpContextAccessor,  IUnitOfWork unitOfWork) : IAuthTokenService
{
    private readonly JwtOptions options = jwtOptions.Value;

    public async Task<string> CreateTokenAsync(User user)
    {
        var accessToken = GenerateToken(user);
        var newRefreshToken = GenerateRefreshToken();
        SetRefreshTokenAsHttpOnlyCookie(newRefreshToken);

        unitOfWork.BeginTransaction();
        user.RefreshToken = newRefreshToken.TokenValue;
        user.RefreshTokenExpiryDate = newRefreshToken.Expires;
        await unitOfWork.UserRepository.UpdateAsync(user);
        unitOfWork.CommitAndCloseConnection();

        return accessToken;
    }

    private static RefreshToken GenerateRefreshToken()
    {
        return new RefreshToken
        {
            TokenValue = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddDays(7)
        };
    }

    private string GenerateToken(User user)
    {
        var claims = new List<Claim>
       {
           new(JwtRegisteredClaimNames.Sub, user.Id),
           new(JwtRegisteredClaimNames.Email, user.Email),
       };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
            SecurityAlgorithms.HmacSha512Signature
            );
        var token = new JwtSecurityToken(
            options.Issuer,
            options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            signingCredentials
            );
         
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public void SetRefreshTokenAsHttpOnlyCookie(RefreshToken refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.Expires,
        };

        var httpContext = httpContextAccessor.HttpContext;
        httpContext?.Response.Cookies.Append(GlobalConstants.RefreshTokenCookieKey, refreshToken.TokenValue, cookieOptions);
    }
}
