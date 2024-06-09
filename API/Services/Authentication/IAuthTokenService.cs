using Business.Domain.UserDomain;

namespace Services.Authentication;

public interface IAuthTokenService
{
    Task<string> CreateTokenAsync(User user);
}
