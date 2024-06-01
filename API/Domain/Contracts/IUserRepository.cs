using Business.Domain.UserDomain;

namespace Business.Contracts;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<User> GetUserByEmail(string email);
}
