using Auction.Domain.Entities;

namespace Auction.Core.Interfaces.Data;

public interface IUnitOfWork
{
    public IBaseRepository<AuctionComment> AuctionCommentRepository { get; }
    public Task CreateTransactionAsync();
    public Task CommitTransactionAsync();
    public Task RollbackTransactionAsync();
}