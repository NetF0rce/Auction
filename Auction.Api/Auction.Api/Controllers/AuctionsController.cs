using Auction.Contracts.DTO;
using Auction.Core.Interfaces.Auctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionsService _auctionsService;
        private readonly IAuctionsVerificationService _auctionsVerificationService;

        public AuctionsController(IAuctionsService auctionsService, IAuctionsVerificationService auctionsVerificationService)
        {
            _auctionsService = auctionsService;
            _auctionsVerificationService = auctionsVerificationService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ListModel<AuctionResponse>>> GetAuctionsList([FromQuery] AuctionFiltersDTO filters)
        {
            return Ok(await _auctionsService.GetAuctionsListAsync(filters));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<AuctionResponse>> GetAuctionById([FromRoute] long id)
        {
            return Ok(await _auctionsService.GetAuctionByIdAsync(id));
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> PublishAuction([FromForm] PublishAuctionRequest request)
        {
            await _auctionsService.PublishAuctionAsync(request);

            return Ok(new { Message = "Auction has been successfully published." });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> ConfirmPayment([FromRoute] long id)
        {
            await _auctionsService.ConfirmPaymentForAuctionAsync(id);

            return Ok(new { Message = "Payment has been successfully confirmed." });
        }

        [HttpPut("{id}/cancel")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CancelAuction([FromRoute] long id)
        {
            await _auctionsService.CancelAuctionAsync(id);

            return Ok(new { Message = "Auction has been successfully canceled." });
        }

        //[HttpPut("{id}/recover")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> RecoverAuction([FromRoute] long id)
        //{
        //    await _auctionsService.RecoverAuctionAsync(id);

        //    return Ok(new { Message = "Auction has been successfully recovered." });
        //}

        //[HttpGet("not-appoved")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult<IEnumerable<AuctionResponse>>> GetNotApprovedAuctions()
        //{
        //    return Ok(await _auctionsVerificationService.GetNotApprovedAuctionsAsync());
        //}

        //[HttpGet("not-appoved/{id}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult<AuctionResponse>> GetNotApprovedAuctionById([FromRoute] long id)
        //{
        //    return Ok(await _auctionsVerificationService.GetNotApprovedAuctionByIdAsync(id));
        //}

        //[HttpPut("not-approved/{id}/approve")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> ApproveAuction([FromRoute] long id)
        //{
        //    await _auctionsVerificationService.ApproveAuctionAsync(id);

        //    return Ok(new { Message = "Auction has been successfully approved." });
        //}

        //[HttpPut("not-approved/{id}/reject")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> RejectAuction([FromBody] RejectAuctionRequest request)
        //{
        //    await _auctionsVerificationService.RejectAuctionAsync(request);

        //    return Ok(new { Message = "Auction has been rejected." });
        //}
    }
}