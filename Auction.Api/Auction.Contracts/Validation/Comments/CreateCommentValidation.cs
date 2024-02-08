using Auction.Contracts.Dto;
using FluentValidation;

namespace Auction.Contracts.Validation.Comments;

public class CreateCommentValidation : AbstractValidator<CreateCommentDto>
{
    public CreateCommentValidation()
    {
        RuleFor(x => x.MainText).NotEmpty().MaximumLength(300);
    }
}