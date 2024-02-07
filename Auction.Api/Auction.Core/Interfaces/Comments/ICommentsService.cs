using System.Reflection.Metadata.Ecma335;
using Auction.Contracts.Dto;
using Auction.Core.Services.Abstract;

namespace Auction.Core.Interfaces.Comments;

public interface ICommentsService
{
    Task<List<GetCommentDto?>> GetCommentsByAuctionId(long auctionId);
    Task<GetCommentDto> CreateComment(CreateCommentDto createCommentDto);
    Task DeleteComment(long commentId);
}