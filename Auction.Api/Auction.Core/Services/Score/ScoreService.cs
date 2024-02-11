using Auction.Contracts.DTO;
using Auction.Core.Interfaces;
using Auction.Core.Interfaces.Data;
using Auction.Core.Interfaces.UserAccessor;
using Auction.Core.Services.Abstract;
using Auction.Core.Specifications;
using Auction.Domain.Entities;
using AutoMapper;

namespace Auction.Core.Services.Score;

public class ScoreService : BaseService, IScoreService
{
    private readonly IUserAccessor _userAccessor;

    public ScoreService(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
    {
        _userAccessor = userAccessor;
    }

    public async Task<GetAuctionScoreDto> GetScoreAsync(long auctionId)
    {
        var specBuilder = new SpecificationBuilder<AuctionScore>()
            .With(s => s.AuctionId == auctionId && s.UserId == _userAccessor.GetCurrentUserId());
        var spec = specBuilder.Build();
        var score = (await UnitOfWork.ScoreRepository.GetAllAsync(spec)).FirstOrDefault();
        if (score is null)
        {
            throw new ArgumentException("Score not found");
        }
        var result = Mapper.Map<GetAuctionScoreDto>(score);
        return result;
    }

    public async Task<GetAuctionScoreDto> CreateOrUpdateScoreAsync(CreateOrUpdateScoreDto dto)
    {
        var score = Mapper.Map<AuctionScore>(dto);
        score.UserId = _userAccessor.GetCurrentUserId();
        var specBuilder = new SpecificationBuilder<AuctionScore>();
        var spec = specBuilder
            .With(x => x.UserId == score.UserId && x.AuctionId == score.AuctionId)
            .Build();
        var existingScore = (await UnitOfWork.ScoreRepository.GetAllAsync(spec)).FirstOrDefault();
        if (existingScore is not null)
        {
            existingScore.Score = score.Score;
            await UnitOfWork.ScoreRepository.UpdateAsync(existingScore);
            return Mapper.Map<GetAuctionScoreDto>(existingScore);
        }

        await UnitOfWork.ScoreRepository.AddAsync(score);
        return Mapper.Map<GetAuctionScoreDto>(score);
    }
}