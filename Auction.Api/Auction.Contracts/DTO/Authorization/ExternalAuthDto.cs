﻿namespace Auction.Contracts.DTO.Authorization;

public class ExternalAuthDto
{
    public string? Provider { get; set; }
    public string? IdToken { get; set; }
}