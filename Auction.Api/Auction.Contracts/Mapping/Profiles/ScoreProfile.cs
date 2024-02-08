using Auction.Contracts.DTO;
using Auction.Domain.Entities;
using AutoMapper;

namespace Auction.Contracts.Mapping.Profiles;

public class ScoreProfile : Profile
{
    public ScoreProfile()
    {
        CreateMap<CreateOrUpdateScoreDto, AuctionScore>();
    }
}