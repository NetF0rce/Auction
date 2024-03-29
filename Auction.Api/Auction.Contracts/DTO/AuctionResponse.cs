﻿using Auction.Contracts.DTO.Image;
using Auction.Domain.Enums;

namespace Auction.Contracts.DTO;

public class AuctionResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ImageDto> Images { get; set; }
    public double Score { get; set; }
    public long FinishInterval { get; set; }
    public decimal StartPrice { get; set; } 
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public long AuctionistUserId { get; set; }
    public string AuctionistUsername { get; set; }
    public AuctionStatus Status { get; set; }
    public bool IsPaied { get; set; }
}
