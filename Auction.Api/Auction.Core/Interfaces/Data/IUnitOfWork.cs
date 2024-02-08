using Auction.Core.Services;
using Auction.Domain.Entities;

namespace Auction.Core.Interfaces.Data;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IAuctionsRepository AuctionsRepository { get; }
    public IBidsRepository BidsRepository { get; }
    public ICommentRepository CommentsRepository { get; init; }

    public Task CreateTransactionAsync();
    public Task CommitTransactionAsync();
    public Task RollbackTransactionAsync();
}