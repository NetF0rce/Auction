using Auction.Contracts.DTO;
using AutoMapper;

namespace Auction.Contracts.Mapping.Profiles;

public class AuctionProfile : Profile
{
    public AuctionProfile()
    {
        CreateMap<Domain.Entities.Auction, AuctionResponse>();
        CreateMap<PublishAuctionRequest, Domain.Entities.Auction>();
        CreateMap<EditAuctionRequest, Domain.Entities.Auction>();
    }
}
