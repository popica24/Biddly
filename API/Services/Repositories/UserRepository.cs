using Business.Contracts;
using Business.Domain.UserDomain;
using Services.Context;

namespace Services.Repositories;

public sealed class UserRepository(SqlDataContext context) : GenericRepository<User>(context), IUserRepository
{
    public async Task<User> GetUserByEmail(string email)
    {
        return (await GetByColumnAsync("email", email)).AsQueryable().FirstOrDefault();
    }
}
