using Auction.Contracts.Dto;
using Auction.Domain.Entities;
using AutoMapper;

namespace Auction.Contracts.Mapping.Profiles;

public class CommentsProfile : Profile
{
    public CommentsProfile()
    {
        CreateMap<AuctionComment, GetCommentDto>();
    }
    
}