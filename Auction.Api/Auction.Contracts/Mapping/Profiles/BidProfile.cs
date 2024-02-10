using Auction.Contracts.DTO.Bid;
using Auction.Domain.Entities;
using AutoMapper;

namespace Auction.Contracts.Mapping.Profiles;

public class BidProfile: Profile
{
    public BidProfile()
    {
        CreateMap<BidAddRequest, Bid>();
        CreateMap<Bid, BidResponse>()
            .ForMember(b => b.BidderName, b => b.MapFrom(bd => bd.Bidder.Username));
    }
}