using Auction.Core.Services;
using Auction.Domain.Entities;

namespace Auction.Core.Interfaces.Data;

public interface IUnitOfWork
{
    public ICommentRepository CommentsRepository { get; init; }
    public Task CreateTransactionAsync();
    public Task CommitTransactionAsync();
    public Task RollbackTransactionAsync();
}