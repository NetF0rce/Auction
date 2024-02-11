using Auction.Core.Interfaces.Data;
using Auction.Domain.Entities;
using Auction.Infrastructure.Database;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Auction.Infrastructure.Repositories;

public class BidsRepository(ApplicationDbContext context) : BaseRepository<Bid>(context), IBidsRepository
{
    public async Task<IEnumerable<Bid>> GetBidsByAuctionIdAsync(long id)
    {
        return await context.Bid.Where(a => a.AuctionId == id).OrderByDescending(a => a.Amount).ToListAsync();
    }
}
