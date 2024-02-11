using Auction.Core.Interfaces.Users;

namespace Auction.Core.Interfaces.Data;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IAuctionsRepository AuctionsRepository { get; }
    public IBidsRepository BidsRepository { get; }
    public IAuctionImagesRepository AuctionImagesRepository { get; }
    public ICommentsRepository CommentsRepository { get; init; }
    public IScoreRepository ScoreRepository { get; init; }

    public Task CreateTransactionAsync();
    public Task CommitTransactionAsync();
    public Task RollbackTransactionAsync();
}