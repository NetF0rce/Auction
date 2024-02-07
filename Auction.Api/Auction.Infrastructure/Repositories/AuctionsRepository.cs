using Auction.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Auction.Infrastructure.Repositories
{
    public class AuctionsRepository : BaseRepository<Domain.Entities.Auction>
    {
        public AuctionsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Domain.Entities.Auction?> GetByIdAsync(long id)
        {
            return await context.Auctions.Include(x => x.Auctionist).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
