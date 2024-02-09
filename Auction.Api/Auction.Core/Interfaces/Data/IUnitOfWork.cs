using Auction.Core.Interfaces.Users;

namespace Auction.Core.Interfaces.Data;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IAuctionsRepository AuctionsRepository { get; }
    public IBidsRepository BidsRepository { get; }
    public ICommentRepository CommentsRepository { get; init; }
    public IAuctionImagesRepository AuctionImagesRepository { get; }

    public Task CreateTransactionAsync();
    public Task CommitTransactionAsync();
    public Task RollbackTransactionAsync();
}