using Business.Contracts;
using Business.Domain;
using Services.Context;

namespace Services.Repositories;

public sealed class UserRepository(SqlDataContext context) : GenericRepository<User>(context), IUserRepository
{

}
