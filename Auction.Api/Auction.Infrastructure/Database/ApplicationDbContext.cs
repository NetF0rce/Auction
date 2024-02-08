using Microsoft.EntityFrameworkCore;
using Auction.Domain.Entities;

namespace Auction.Infrastructure.Database;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<Domain.Entities.Auction> Auction { get; set; }
    public virtual DbSet<AuctionImage> AuctionImage { get; set; }
    public virtual DbSet<AuctionComment> AuctionComment { get; set; }
    public virtual DbSet<AuctionScore> AuctionScore { get; set; }
    public virtual DbSet<Bid> Bid { get; set; }
    public virtual DbSet<User> User { get; set; }
}