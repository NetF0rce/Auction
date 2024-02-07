using Auction.Contracts.Dto;
using Auction.Core.Interfaces.Comments;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Api;

[ApiController]
[Route("api/[controller]")]
public class CommentsController(ICommentsService commentsService) : ControllerBase 
{
    [HttpGet("{auctionId}")]
    public async Task<IActionResult> GetComments(long auctionId)
    {
        var comments = await commentsService.GetCommentsByAuctionId(auctionId);
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