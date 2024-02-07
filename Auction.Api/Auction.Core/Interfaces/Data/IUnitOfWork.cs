using Auction.Core.Interfaces.Users;

namespace Auction.Core.Interfaces.Data;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public Task CreateTransactionAsync();
    public Task CommitTransactionAsync();
    public Task RollbackTransactionAsync();
}