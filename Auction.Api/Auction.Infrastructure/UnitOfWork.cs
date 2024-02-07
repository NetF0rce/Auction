using Auction.Core.Interfaces.Data;
using Auction.Infrastructure.Database;

namespace Auction.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private readonly IAuctionsRepository _auctionsRepository;
    public IAuctionsRepository AuctionsRepository => _auctionsRepository;

    private readonly IBidsRepository _bidsRepository;
    public IBidsRepository BidsRepository => _bidsRepository;

    public UnitOfWork(ApplicationDbContext context, IAuctionsRepository auctionsRepository,
        IBidsRepository bidsRepository)
    {
        _context = context;
        _auctionsRepository = auctionsRepository;
        _bidsRepository = bidsRepository;
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