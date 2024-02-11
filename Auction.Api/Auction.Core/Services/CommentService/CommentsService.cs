using Auction.Contracts.Dto;
using Auction.Contracts.DTO;
using Auction.Core.Interfaces.Comments;
using Auction.Core.Interfaces.Data;
using Auction.Core.Interfaces.UserAccessor;
using Auction.Core.Services.Abstract;
using Auction.Core.Specifications;
using Auction.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Auction.Core.Services.CommentService;

public class CommentsService(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : BaseService(unitOfWork, mapper), ICommentsService
{
    public async Task<ListModel<GetCommentDto>> GetCommentsByAuctionId(long auctionId,
        CommentFilterDto commentFilterDto)
    {
        ArgumentNullException.ThrowIfNull(auctionId);
        var spec = GetSpecification(auctionId, commentFilterDto);
        var comments = await UnitOfWork.CommentsRepository.GetAllAsync(spec);
        
        var totalComments = await UnitOfWork.CommentsRepository.CountAsync(spec.Predicate!);
        
        var totalPages = (int)Math.Ceiling((double)totalComments / commentFilterDto.PageSize);

        var result = new ListModel<GetCommentDto>
        {
            Data = comments.Select(c => Mapper.Map<GetCommentDto>(comments)).ToList(),
            TotalPages = totalPages,
            CurrentPage = commentFilterDto.PageNumber,
        };

        return result;
    }

    private ISpecification<AuctionComment> GetSpecification(long auctionId, CommentFilterDto commentFilterDto)
    {
        var specBuilder = new SpecificationBuilder<AuctionComment>()
            .With(ac => ac.AuctionId == auctionId)
            .WithPagination(commentFilterDto.PageSize, commentFilterDto.PageNumber)
            .WithInclude(query => query.Include(ac => ac.User));
        if (!string.IsNullOrEmpty(commentFilterDto.SortBy))
        {
            specBuilder.OrderBy(x => x.DateTime, commentFilterDto.SortDirection);
        }

        var spec = specBuilder.Build();
        return spec;
    }

    public async Task<GetCommentDto> CreateComment(CreateCommentDto createCommentDto)
    {
        ArgumentNullException.ThrowIfNull(createCommentDto);
        
        var comment = Mapper.Map<AuctionComment>(createCommentDto);
        comment.UserId = userAccessor.GetCurrentUserId();
        
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

        comment!.IsDeleted = true;
        await UnitOfWork.CommentsRepository.UpdateAsync(comment);
    }
}