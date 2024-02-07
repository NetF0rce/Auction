using Auction.Domain.Entities;

namespace Auction.Core.Interfaces.Data;

public interface IUnitOfWork
{
    public IAuctionsRepository AuctionsRepository { get; }

    public Task CreateTransactionAsync();
    public Task CommitTransactionAsync();
    public Task RollbackTransactionAsync();
}