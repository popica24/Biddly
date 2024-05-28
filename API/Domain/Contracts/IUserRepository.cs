using Business.Domain;

namespace Business.Contracts;

public interface IUserRepository
{
    public bool Create(User user);
}
