using Auction.Core.Interfaces.Data;
using Auction.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Auction.Infrastructure.Repositories
{
    public class AuctionsRepository : BaseRepository<Domain.Entities.Auction>, IAuctionsRepository
    {
        public AuctionsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Domain.Entities.Auction?> GetByIdAsync(long id)
        {
            return await context.Auction
                .Include(x => x.Auctionist)
                .Include(x => x.Images)
                .Include(x => x.Scores)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
