using Auction.Core.Interfaces.Data;
using Auction.Core.Interfaces.Users;
using Auction.Infrastructure.Database;

namespace Auction.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository)
    {
        _context = context;
        UserRepository = userRepository;
    }
    
    public IUserRepository UserRepository { get; }
    
    public Task CreateTransactionAsync()
    {
        return _context.Database.BeginTransactionAsync();
    }

    public Task CommitTransactionAsync()
    {
        return _context.Database.CommitTransactionAsync();
    }

    public Task RollbackTransactionAsync()
    {
        return _context.Database.RollbackTransactionAsync();
    }
}