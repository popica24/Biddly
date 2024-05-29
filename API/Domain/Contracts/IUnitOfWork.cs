namespace Business.Contracts;

public interface IUnitOfWork : IDisposable
{
    public IUserRepository UserRepository{ get; }

    void BeginTransaction();

    void Commit();

    void CommitAndCloseConnection();

    void Rollback();
}
