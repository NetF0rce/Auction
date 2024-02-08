using Auction.Contracts.DTO;
using Auction.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Customer")]
public class ScoreController : ControllerBase
{
    private readonly IScoreService _scoreService;

    public ScoreController(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }
    
    [HttpGet("{auctionId}")]
    public async Task<ActionResult> GetScore(long auctionId)
    {
        var result = await _scoreService.GetScoreAsync(auctionId);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateOrUpdateScore(CreateOrUpdateScoreDto dto)
    {
        var result = await _scoreService.CreateOrUpdateScoreAsync(dto);
        return Ok(result);
    }
}