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
    public async Task<List<GetCommentDto?>> GetCommentsByAuctionId(long auctionId)
    {
        ArgumentNullException.ThrowIfNull(auctionId);
        var spec = new SpecificationBuilder<AuctionComment>()
            .With(ac => ac.AuctionId == auctionId)
            .WithInclude(query => query.Include(ac => ac.User))
            .Build();
        var comments =
            await UnitOfWork.CommentsRepository.GetAllAsync(spec);
        return comments.Select(c => Mapper.Map<GetCommentDto>(c)).ToList();
    }

    public async Task<GetCommentDto> CreateComment(CreateCommentDto createCommentDto)
    {
        ArgumentNullException.ThrowIfNull(createCommentDto);
        var comment = Mapper.Map<AuctionComment>(createCommentDto);
        var result = await UnitOfWork.CommentsRepository.AddAsync(comment);
        return Mapper.Map<GetCommentDto>(result);
    }

    public async Task DeleteComment(long commentId)
    {
        ArgumentNullException.ThrowIfNull(commentId);
        var isExists = await UnitOfWork.CommentsRepository.IsExistAsync(commentId);
        if (!isExists)
        {
            throw new ArgumentException("Comment not found");
        }
        var comment = await UnitOfWork.CommentsRepository.GetByIdAsync(commentId);
        comment.IsDeleted = true;
        await UnitOfWork.CommentsRepository.UpdateAsync(comment);
    }
}