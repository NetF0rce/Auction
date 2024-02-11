using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Auction.Contracts.DTO;

public class EditAuctionRequest : PublishAuctionRequest
{
    public List<string>? OldPhotos { get; set; }
}