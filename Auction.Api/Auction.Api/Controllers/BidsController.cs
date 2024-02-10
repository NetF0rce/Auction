using Auction.Contracts.DTO.Bid;
using Auction.Core.Interfaces.Bids;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BidsController(IBidService bidService) : ControllerBase
{
    [HttpGet("auction/{actionId}/bids")]
    public async Task<ActionResult<IEnumerable<BidResponse>>> GetBitsByAuction([FromRoute]long auctionId)
    {
        return Ok(await bidService.GetBidsByAuctionIdAsync(auctionId));
    }
    
    [HttpPost("auction/{actionId}/bids")]
    public async Task<ActionResult<IEnumerable<BidResponse>>> GetBitsByAuction([FromBody]BidAddRequest bidAddRequest)
    {
        return Ok(await bidService.SetNewBid(bidAddRequest));
    }
}