namespace Business.Contracts;

public interface IUnitOfWork : IDisposable
{
    public IUserRepository UserRepository{ get; }
    public IItemRepository ItemRepository { get; }
    public IBidRepository BidRepository { get; }

    void BeginTransaction();

    void Commit();

    void CommitAndCloseConnection();

    void Rollback();
}
