using Auction.Core.Interfaces.Data;
using Auction.Domain.Entities;
using Auction.Infrastructure.Database;
using Auction.Infrastructure.Repositories;

namespace Auction.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private readonly IAuctionsRepository _auctionsRepository;
    public IAuctionsRepository AuctionsRepository { get => _auctionsRepository; }

    public UnitOfWork(ApplicationDbContext context, IAuctionsRepository auctionsRepository)
    {
        _context = context;
        _auctionsRepository = auctionsRepository;
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