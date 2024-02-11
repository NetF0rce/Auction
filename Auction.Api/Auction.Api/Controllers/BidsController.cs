using Auction.Contracts.DTO.Bid;
using Auction.Core.Interfaces.Bids;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BidsController(IBidService bidService) : ControllerBase
{
    [HttpGet("{actionId}")]
    public async Task<ActionResult<IEnumerable<BidResponse>>> GetBitsByAuction([FromRoute]long auctionId)
    {
        return Ok(await bidService.GetBidsByAuctionIdAsync(auctionId));
    }
    
    [HttpPost]
    public async Task<ActionResult<IEnumerable<BidResponse>>> MakeBit(BidAddRequest bidAddRequest)
    {
        return Ok(await bidService.SetNewBid(bidAddRequest));
    }
}