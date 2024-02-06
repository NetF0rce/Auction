using Microsoft.EntityFrameworkCore;
using Auction.Domain.Entities;

namespace Auction.Infrastructure.Database;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<Domain.Entities.Auction> Auctions { get; set; }
    public virtual DbSet<AuctionImage> AuctionImages { get; set; }
    public virtual DbSet<AuctionComment> AuctionComments { get; set; }
    public virtual DbSet<AuctionScore> AuctionScores { get; set; }
    public virtual DbSet<Bid> Bids { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}