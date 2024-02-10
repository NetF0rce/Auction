using Auction.Contracts.DTO.Bid;
using Auction.Core.Exceptions;
using Auction.Core.Interfaces.Bids;
using Auction.Core.Interfaces.Data;
using Auction.Core.Interfaces.UserAccessor;
using Auction.Domain.Entities;
using AutoMapper;

namespace Auction.Core.Services.Bids;

public class BidService(IAuctionsRepository auctionsRepository, IBidsRepository bidsRepository, IMapper mapper, IUserAccessor userAccessor)
    : IBidService
{
    public async Task<BidResponse> SetNewBid(BidAddRequest bidAddRequest)
    {
        if (bidAddRequest.BidderId != userAccessor.GetCurrentUserId())
            throw new ArgumentException("You should make bids from your own account");
        
        var auction = await auctionsRepository.GetByIdAsync(bidAddRequest.AuctionId);

        if (auction is null)
            throw new KeyNotFoundException("Cannot find auction with such id");

        if (auction.FinishDateTime is not null && auction.FinishDateTime.Value.ToUniversalTime() < DateTime.UtcNow)
            throw new AuctionExpiredException();

        if (auction.AuctionistId == bidAddRequest.BidderId)
            throw new ArgumentException("You cannot bid on your own auction");

        var bids = (await bidsRepository.GetBidsByAuctionIdAsync(auction.Id)).ToList();

        if (bids.Count == 0 && bidAddRequest.Amount <= auction.StartPrice)
            throw new ArgumentException("Bid amount should be bigger than auction start price");
        else if (bids.Count > 1 && bidAddRequest.Amount <= bids.Max(x => x.Amount))
            throw new ArgumentException("Bid amount should be bigger than last bid ammount");

        var bid = mapper.Map<Bid>(bidAddRequest);

        if (bid is null)
            throw new InvalidOperationException("Cannot map bid request");

        bid = await bidsRepository.AddAsync(bid);

        var response = mapper.Map<BidResponse>(bid);

        if (response is null)
            throw new InvalidOperationException("Cannot map bid");

        return response;
    }

    public async Task<IEnumerable<BidResponse>> GetBidsByAuctionIdAsync(long id)
    {
        var auction = await auctionsRepository.GetByIdAsync(id);

        if (auction is null)
            throw new KeyNotFoundException("Cannot find auction with such id");

        return mapper.Map<IEnumerable<BidResponse>>(await bidsRepository.GetBidsByAuctionIdAsync(id)) ??
               new List<BidResponse>();
    }
}