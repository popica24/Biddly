using Business.Domain.UserDomain;
using Services.Common.DTO.Token;

namespace Services.Authentication;

public interface IAuthTokenService
{
    Task<string> CreateTokenAsync(User user);
}
