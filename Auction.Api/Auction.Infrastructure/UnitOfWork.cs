using Auction.Core.Interfaces.Data;
using Auction.Domain.Entities;
using Auction.Infrastructure.Database;
using Auction.Infrastructure.Repositories;

namespace Auction.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IBaseRepository<AuctionComment> AuctionCommentRepository { get; }

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