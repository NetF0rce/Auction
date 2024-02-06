namespace Auction.Core.Interfaces.Data;

public interface IUnitOfWork
{
    public Task CreateTransactionAsync();
    public Task CommitTransactionAsync();
    public Task RollbackTransactionAsync();
}