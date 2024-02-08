using System.Reflection.Metadata.Ecma335;
using Auction.Contracts.Dto;
using Auction.Contracts.DTO;
using Auction.Core.Services.Abstract;

namespace Auction.Core.Interfaces.Comments;

public interface ICommentsService
{
    Task<ListModel<GetCommentDto>> GetCommentsByAuctionId(long auctionId, CommentFilterDto filter);
    Task<GetCommentDto> CreateComment(CreateCommentDto createCommentDto);
    Task DeleteComment(long commentId);
}