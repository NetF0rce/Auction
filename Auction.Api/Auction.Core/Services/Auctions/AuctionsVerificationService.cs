using Auction.Contracts.DTO;
using Auction.Core.Interfaces.Auctions;
using Auction.Core.Interfaces.Data;
using Auction.Core.Services.Abstract;
using Auction.Core.Specifications;
using Auction.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Auction.Core.Services.Auctions;

public class AuctionsVerificationService : BaseService, IAuctionsVerificationService
{
    public AuctionsVerificationService(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
    }

    public async Task ApproveAuctionAsync(long id)
    {
        var auction = await UnitOfWork.AuctionsRepository.GetByIdAsync(id);

        if (auction == null || auction.Status != Domain.Enums.AuctionStatus.NotApproved)
        {
            throw new KeyNotFoundException("Not approved auction with such id does not exist.");
        }

        auction.Status = Domain.Enums.AuctionStatus.Active;
        auction.StartDateTime = DateTime.UtcNow;
        auction.FinishDateTime = auction.StartDateTime + auction.FinishInterval;

        await UnitOfWork.AuctionsRepository.UpdateAsync(auction);

        // TODO: add email sending to user
    }

    public async Task<AuctionResponse> GetNotApprovedAuctionByIdAsync(long id)
    {
        var auction = await UnitOfWork.AuctionsRepository.GetByIdAsync(id);

        if (auction == null || auction.Status != Domain.Enums.AuctionStatus.NotApproved)
        {
            throw new KeyNotFoundException("Not approved auction with such id does not exist.");
        }

        var response = Mapper.Map<AuctionResponse>(auction);
        response.ImageUrls = auction.Images.Select(x => x.Url).ToList();
        response.AuctionistUserId = auction.AuctionistId;
        response.AuctionistUsername = auction.Auctionist.Username;

        return response;
    }

    public async Task<IEnumerable<AuctionResponse>> GetNotApprovedAuctionsAsync()
    {
        var specification = new SpecificationBuilder<Domain.Entities.Auction>()
            .With(x => x.Status == Domain.Enums.AuctionStatus.NotApproved)
            .OrderBy(x => x.Id)
            .WithInclude(query => query.Include(x => x.Auctionist))
            .Build();

        var auctions = await UnitOfWork.AuctionsRepository.GetAllAsync(specification);

        var result = auctions.Select(x =>
            {
                var response = Mapper.Map<AuctionResponse>(x);
                response.ImageUrls = x.Images.Select(x => x.Url).ToList();
                response.AuctionistUserId = x.AuctionistId;
                response.AuctionistUsername = x.Auctionist.Username;

                return response;
            })
            .ToList();

        return result;
    }

    public async Task RejectAuctionAsync(RejectAuctionRequest request)
    {
        var auction = await UnitOfWork.AuctionsRepository.GetByIdAsync(request.AuctionId);

        if (auction == null || auction.Status != Domain.Enums.AuctionStatus.NotApproved)
        {
            throw new KeyNotFoundException("Not approved auction with such id does not exist.");
        }

        await UnitOfWork.AuctionsRepository.DeleteByIdAsync(request.AuctionId);

        // TODO: Send email to user with rejection reason
    }
}
