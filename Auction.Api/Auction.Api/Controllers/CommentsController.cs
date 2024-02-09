using Auction.Contracts.Dto;
using Auction.Contracts.DTO;
using Auction.Core.Interfaces.Comments;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController(ICommentsService commentsService) : ControllerBase 
{
    [HttpGet("{auctionId}")]
    public async Task<IActionResult> GetComments(long auctionId, [FromQuery]CommentFilterDto filter)
    {
        var comments = await commentsService.GetCommentsByAuctionId(auctionId, filter);
        return Ok(comments);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto)
    {
        var comment = await commentsService.CreateComment(createCommentDto);
        return Ok(comment);
    }
    
    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteComment(long commentId)
    {
        await commentsService.DeleteComment(commentId);
        return Ok();
    }
}