using Auction.Contracts.Dto;
using Auction.Core.Interfaces.Comments;
using Auction.Core.Interfaces.Data;
using Auction.Core.Services.Abstract;
using Auction.Core.Specifications;
using Auction.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Auction.Core.Services;

public class CommentsService(IUnitOfWork unitOfWork, IMapper mapper) : BaseService(unitOfWork, mapper), ICommentsService
{
    public async Task<List<GetCommentDto>> GetCommentsByAuctionId(long auctionId)
    {
        ArgumentNullException.ThrowIfNull(auctionId);
        var spec = new SpecificationBuilder<AuctionComment>()
            .With(ac => ac.AuctionId == auctionId)
            .WithInclude(query => query.Include(ac => ac.User))
            .Build();
        var comments = 
            (await UnitOfWork.AuctionCommentRepository.GetAllAsync(spec))
            .Select(c => new GetCommentDto 
            {
                Id = c.Id,
                AuctionId = c.AuctionId,
                UserId = c.UserId,
                UserName = c.User.Username,
                DateTime = c.DateTime,
                MainText = c.MainText
            }).ToList();
        return comments;
    }

    public async Task<GetCommentDto> CreateComment(CreateCommentDto createCommentDto)
    {
        ArgumentNullException.ThrowIfNull(createCommentDto);
        var comment = Mapper.Map<AuctionComment>(createCommentDto);
        var result = await UnitOfWork.AuctionCommentRepository.AddAsync(comment);
        return Mapper.Map<GetCommentDto>(result);
    }

    public Task DeleteComment(long commentId)
    {
        ArgumentNullException.ThrowIfNull(commentId);
        return UnitOfWork.AuctionCommentRepository.DeleteByIdAsync(commentId);
    }
}