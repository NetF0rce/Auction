using Auction.Contracts.DTO;

namespace Auction.Core.Interfaces;

public interface IScoreService
{
    Task<GetAuctionScoreDto> GetScoreAsync(long auctionId);
    Task<GetAuctionScoreDto> CreateOrUpdateScoreAsync(CreateOrUpdateScoreDto dto);
}