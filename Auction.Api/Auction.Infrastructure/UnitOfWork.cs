﻿using Auction.Core.Interfaces.Data;
using Auction.Core.Interfaces.Users;
using Auction.Infrastructure.Database;

namespace Auction.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private readonly IAuctionsRepository _auctionsRepository;
    public IAuctionsRepository AuctionsRepository => _auctionsRepository;

    private readonly IBidsRepository _bidsRepository;
    public IBidsRepository BidsRepository => _bidsRepository;
    public ICommentsRepository CommentsRepository { get; init; }
    public IScoreRepository ScoreRepository { get; init; }
    public IUserRepository UserRepository { get; }
    public IAuctionImagesRepository AuctionImagesRepository { get; init; }

    public UnitOfWork(ApplicationDbContext context, 
        IAuctionsRepository auctionsRepository,
        ICommentsRepository commentsRepository,
        IUserRepository userRepository,
        IBidsRepository bidsRepository, 
        IAuctionImagesRepository auctionImagesRepository,
        IScoreRepository scoreRepository)
    {
        _context = context;
        _auctionsRepository = auctionsRepository;
        _bidsRepository = bidsRepository;
        CommentsRepository = commentsRepository;
        UserRepository = userRepository;
        AuctionImagesRepository = auctionImagesRepository;
        ScoreRepository = scoreRepository;
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