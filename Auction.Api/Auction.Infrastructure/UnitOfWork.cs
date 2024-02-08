using Auction.Core.Interfaces.Data;
using Auction.Domain.Entities;
using Auction.Infrastructure.Database;
using Auction.Infrastructure.Repositories;

namespace Auction.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private readonly IAuctionsRepository _auctionsRepository;
    public IAuctionsRepository AuctionsRepository => _auctionsRepository;

    private readonly IBidsRepository _bidsRepository;
    public IBidsRepository BidsRepository => _bidsRepository;
    
    public ICommentRepository CommentsRepository { get; init; }

    public UnitOfWork(ApplicationDbContext context,
        IAuctionsRepository auctionsRepository, 
        ICommentRepository commentsRepository,
        IBidsRepository bidsRepository)
    {
        _context = context;
        _auctionsRepository = auctionsRepository;
        _bidsRepository = bidsRepository;
        CommentsRepository = commentsRepository;
    }
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