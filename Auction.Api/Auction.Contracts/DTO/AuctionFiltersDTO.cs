using Auction.Domain.Enums;

namespace Auction.Contracts.DTO;

public class AuctionFiltersDTO : BaseFiltersDTO 
{
    public AuctionStatus? Status { get; set; }
}
