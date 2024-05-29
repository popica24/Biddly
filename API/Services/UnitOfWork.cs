using Business.Contracts;
using Services.Context;
using Services.Repositories;

namespace Services;

public class UnitOfWork : IUnitOfWork
{
    private bool _disposed;

    private readonly SqlDataContext _context;

    public IUserRepository UserRepository { get; private set; }

    public UnitOfWork(SqlDataContext context)
    {
        _context = context;
        Init();
    }

    private void Init()
    {
        UserRepository = new UserRepository(_context);
    }

    public void BeginTransaction()
    {
        _context.Connection?.Open();
        _context.Transaction = _context.Connection?.BeginTransaction();
    }

    public void Commit()
    {
        _context.Transaction?.Commit();
        _context.Transaction?.Dispose();
        _context.Transaction = null;
    }

    public void CommitAndCloseConnection()
    {
        _context.Transaction?.Commit();
        _context.Transaction?.Dispose();
        _context.Transaction = null;
        _context.Connection?.Close();
        _context.Connection?.Dispose();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Transaction?.Dispose();
                _context.Connection?.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Rollback()
    {
        _context.Transaction?.Rollback();
        _context.Transaction?.Dispose();
        _context.Transaction = null;
    }
}
