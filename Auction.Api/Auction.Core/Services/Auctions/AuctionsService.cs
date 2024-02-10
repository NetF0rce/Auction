using Auction.Contracts.DTO;
using Auction.Core.Interfaces.Auctions;
using Auction.Core.Interfaces.Data;
using Auction.Core.Interfaces.Images;
using Auction.Core.Interfaces.UserAccessor;
using Auction.Core.Services.Abstract;
using Auction.Core.Specifications;
using Auction.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Auction.Core.Services.Auctions;

public class AuctionsService : BaseService, IAuctionsService
{
    private readonly IUserAccessor _userAccessor;
    private readonly IImagesService _imagesService;

    public AuctionsService(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor, IImagesService imagesService)
        : base(unitOfWork, mapper)
    {
        _userAccessor = userAccessor;
        _imagesService = imagesService;
    }

    public async Task CancelAuctionAsync(long id)
    {
        var auction = await UnitOfWork.AuctionsRepository.GetByIdAsync(id);

        if (auction == null || auction.Status == Domain.Enums.AuctionStatus.NotApproved)
        {
            throw new KeyNotFoundException("Auction with such id does not exist.");
        }

        if (auction.Status == Domain.Enums.AuctionStatus.Finished)
        {
            throw new InvalidOperationException("Cannot cancel already finished auction.");
        }

        if (auction.Status == Domain.Enums.AuctionStatus.Canceled)
        {
            throw new InvalidOperationException("Cannot has already been cacneled.");
        }

        auction.Status = Domain.Enums.AuctionStatus.Canceled;

        await UnitOfWork.AuctionsRepository.UpdateAsync(auction);

        // TODO: Inform auctionist of canceling auction.
    }

    public async Task ConfirmPaymentForAuctionAsync(long id)
    {
        var auction = await UnitOfWork.AuctionsRepository.GetByIdAsync(id);

        if (auction == null || auction.Status == Domain.Enums.AuctionStatus.NotApproved)
        {
            throw new KeyNotFoundException("Auction with such id does not exist.");
        }

        if (auction.Status != Domain.Enums.AuctionStatus.Finished)
        {
            throw new InvalidOperationException("Auction must be finished to confirm payment.");
        }

        long userId = _userAccessor.GetCurrentUserId();

        var specification = new SpecificationBuilder<Bid>()
            .With(x => x.AuctionId == auction.Id)
            .WithInclude(query => query.Include(x => x.Bidder))
            .Build();

        var bids = await UnitOfWork.BidsRepository.GetAllAsync(specification);

        if (!bids.Any())
        {
            throw new InvalidOperationException("There are no bids at the auction.");
        }

        var winningBid = bids.First(x => x.IsWinning);

        if (userId != winningBid.BidderId)
        {
            throw new InvalidOperationException("Cannot confirm payment for other user.");
        }

        auction.IsPaid = true;

        await UnitOfWork.AuctionsRepository.UpdateAsync(auction);

        // TODO: inform auctionist of payment
    }

    public async Task FinishAuctionAsync(long id)
    {
        var auction = await UnitOfWork.AuctionsRepository.GetByIdAsync(id);

        if (auction == null || auction.Status == Domain.Enums.AuctionStatus.NotApproved)
        {
            throw new KeyNotFoundException("Auction with such id does not exist.");
        }

        if (auction.Status == Domain.Enums.AuctionStatus.Finished)
        {
            throw new InvalidOperationException("Auction has already been finished.");
        }

        if (auction.Status == Domain.Enums.AuctionStatus.Canceled)
        {
            throw new InvalidOperationException("Cannot finish canceled auction.");
        }

        auction.Status = Domain.Enums.AuctionStatus.Finished;

        await UnitOfWork.AuctionsRepository.UpdateAsync(auction);

        var specification = new SpecificationBuilder<Bid>()
            .With(x => x.AuctionId == auction.Id)
            .WithInclude(query => query.Include(x => x.Bidder))
            .Build();

        var bids = await UnitOfWork.BidsRepository.GetAllAsync(specification);

        var winningBid = bids
            .OrderByDescending(x => x.Amout)
            .FirstOrDefault();

        if (winningBid != null)
        {
            winningBid.IsWinning = true;

            await UnitOfWork.BidsRepository.UpdateAsync(winningBid);

            // TODO: inform winner
        }

        // TODO: inform auctionist
    }

    public async Task<AuctionResponse> GetAuctionByIdAsync(long id)
    {
        var auction = await UnitOfWork.AuctionsRepository.GetByIdAsync(id);

        if (auction == null || auction.Status == Domain.Enums.AuctionStatus.NotApproved)
        {
            throw new KeyNotFoundException("Auction with such id does not exist.");
        }

        var response = Mapper.Map<AuctionResponse>(auction);
        response!.Score = auction.Scores.Average(x => x.Score);
        response.ImageUrls = auction.Images.Select(x => x.Url).ToList();
        response.AuctionistUserId = auction.AuctionistId;
        response.AuctionistUsername = auction.Auctionist.Username;

        return response;
    }

    public async Task<ListModel<AuctionResponse>> GetAuctionsListAsync(AuctionFiltersDTO filters)
    {
        ArgumentNullException.ThrowIfNull(filters);

        var specification = BuildSpecificationByFilter(filters);

        var auctions = await UnitOfWork.AuctionsRepository.GetAllAsync(specification);

        var totalCount = await UnitOfWork.AuctionsRepository.CountAsync(specification.Predicate!);

        var totalPages = (int)Math.Ceiling((double)totalCount / filters.PageSize);

        var listModel = new ListModel<AuctionResponse>()
        {
            Data = auctions
                .Select(x =>
                {
                    var response = Mapper.Map<AuctionResponse>(x);
                    response!.Score = x.Scores.Average(x => x.Score);
                    response.ImageUrls = x.Images.Select(x => x.Url).ToList();
                    response.AuctionistUserId = x.AuctionistId;
                    response.AuctionistUsername = x.Auctionist.Username;

                    return response;
                })
                .ToList(),
            CurrentPage = filters.PageNumber,
            TotalPages = totalPages
        };

        return listModel;
    }

    public async Task PublishAuctionAsync(PublishAuctionRequest auction)
    {
        ArgumentNullException.ThrowIfNull(auction);

        var auctionToInsert = Mapper.Map<Domain.Entities.Auction>(auction);
        auctionToInsert.AuctionistId = _userAccessor.GetCurrentUserId();
        auctionToInsert.FinishInterval = TimeSpan.FromTicks(auction.FinishIntervalTicks);

        await UnitOfWork.AuctionsRepository.AddAsync(auctionToInsert!);

        foreach (var image in auction.Images)
        {
            var uploadResult = await _imagesService.AddImageAsync(image);

            var auctionImage = new AuctionImage
            {
                AuctionId = auctionToInsert.Id,
                Url = uploadResult.Url.AbsoluteUri,
                PublicId = uploadResult.PublicId,
            };

            await UnitOfWork.AuctionImagesRepository.AddAsync(auctionImage);
        }

        // TODO: inform auctionist
    }

    public async Task RecoverAuctionAsync(long id)
    {
        var auction = await UnitOfWork.AuctionsRepository.GetByIdAsync(id);

        if (auction == null || auction.Status == Domain.Enums.AuctionStatus.NotApproved)
        {
            throw new KeyNotFoundException("Auction with such id does not exist.");
        }

        if (auction.Status != Domain.Enums.AuctionStatus.Canceled)
        {
            throw new InvalidOperationException("Auction has not been canceled to recover.");
        }

        auction.Status = Domain.Enums.AuctionStatus.Active;

        await UnitOfWork.AuctionsRepository.UpdateAsync(auction);

        var specification = new SpecificationBuilder<Bid>()
            .With(x => x.AuctionId == auction.Id)
            .Build();

        var bids = await UnitOfWork.BidsRepository.GetAllAsync(specification);

        await UnitOfWork.BidsRepository.DeleteRangeAsync(bids);

        await UnitOfWork.AuctionsRepository.UpdateAsync(auction);

        // TODO: Inform auctionist of recovering auction.
    }

    private ISpecification<Domain.Entities.Auction> BuildSpecificationByFilter(AuctionFiltersDTO filters)
    {
        var specificationBuilder = new SpecificationBuilder<Domain.Entities.Auction>();

        if (!string.IsNullOrEmpty(filters.Search))
        {
            specificationBuilder.With(x => x.Name.Contains(filters.Search) || x.Description.Contains(filters.Search));
        }

        if (!string.IsNullOrEmpty(filters.SortBy))
        {
            switch (filters.SortBy)
            {
                case nameof(Domain.Entities.Auction.Name):
                    specificationBuilder.OrderBy(x => x.Name, filters.SortDirection);
                    break;
                case nameof(Domain.Entities.Auction.StartDateTime):
                    specificationBuilder.OrderBy(x => x.StartDateTime, filters.SortDirection);
                    break;
            }
        }

        if (filters.Status != null)
        {
            if (filters.Status == Domain.Enums.AuctionStatus.Canceled || filters.Status == Domain.Enums.AuctionStatus.NotApproved)
            {
                throw new ArgumentException("Cannot filter by such auction status.");
            }

            specificationBuilder.With(x => x.Status == filters.Status);
        }
        else
        {
            specificationBuilder.With(x => x.Status != Domain.Enums.AuctionStatus.Canceled && x.Status != Domain.Enums.AuctionStatus.NotApproved);
        }

        specificationBuilder.WithPagination(filters.PageSize, filters.PageNumber);

        return specificationBuilder.Build();
    }
}
