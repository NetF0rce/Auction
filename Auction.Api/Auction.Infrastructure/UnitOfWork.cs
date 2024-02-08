﻿using Auction.Core.Interfaces.Data;
using Auction.Core.Interfaces.Users;
using Auction.Domain.Entities;
using Auction.Core.Interfaces.Users;
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

    public ICommentsRepository CommentsRepository { get; init; }
    public IUserRepository UserRepository { get; }

    public UnitOfWork(ApplicationDbContext context, IAuctionsRepository auctionsRepository,
        ICommentsRepository commentsRepository,
        IUserRepository userRepository,
        IBidsRepository bidsRepository)
    {
        _context = context;
        _auctionsRepository = auctionsRepository;
        _bidsRepository = bidsRepository;
        CommentsRepository = commentsRepository;
        UserRepository = userRepository;
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