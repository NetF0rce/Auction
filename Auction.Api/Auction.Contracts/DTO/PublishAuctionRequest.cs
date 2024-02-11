using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Auction.Contracts.DTO;

public class PublishAuctionRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public long FinishIntervalTicks { get; set; }
    
    [Required]
    public long FinishInterval { get; set; }

    [Required]
    public decimal StartPrice { get; set; }

    [Required]
    [MinLength(1)]
    public List<IFormFile> Images { get; set; }
}
