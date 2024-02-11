using Auction.Contracts.DTO;
using Auction.Contracts.DTO.Image;
using Auction.Contracts.Enums;
using Auction.Core.Interfaces.Auctions;
using Auction.Core.Interfaces.Data;
using Auction.Core.Interfaces.Images;
using Auction.Core.Interfaces.UserAccessor;
using Auction.Core.Services.Abstract;
using Auction.Core.Specifications;
using Auction.Domain.Entities;
using Auction.Domain.Enums;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Auction.Core.Services.Auctions;

public class AuctionsService : BaseService, IAuctionsService
{
    private readonly IUserAccessor _userAccessor;
    private readonly IImagesService _imagesService;

    public AuctionsService(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor,
        IImagesService imagesService)
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
    }

    public async Task FinishAuctionAsync(long id)
    {
        var auction = await UnitOfWork.AuctionsRepository.GetByIdAsync(id);

        if (auction == null || auction.Status == AuctionStatus.NotApproved)
        {
            throw new KeyNotFoundException("Auction with such id does not exist.");
        }

        if (auction.Status == AuctionStatus.Finished)
        {
            throw new InvalidOperationException("Auction has already been finished.");
        }

        if (auction.Status == AuctionStatus.Canceled)
        {
            throw new InvalidOperationException("Cannot finish canceled auction.");
        }

        auction.Status = AuctionStatus.Finished;

        await UnitOfWork.AuctionsRepository.UpdateAsync(auction);

        var specification = new SpecificationBuilder<Bid>()
            .With(x => x.AuctionId == auction.Id)
            .WithInclude(query => query.Include(x => x.Bidder))
            .Build();

        var bids = await UnitOfWork.BidsRepository.GetAllAsync(specification);

        var winningBid = bids.MaxBy(x => x.Amount);

        if (winningBid != null)
        {
            winningBid.IsWinning = true;

            await UnitOfWork.BidsRepository.UpdateAsync(winningBid);
        }
    }

    public async Task<AuctionResponse> GetAuctionByIdAsync(long id)
    {
        var auction = await UnitOfWork.AuctionsRepository.GetByIdAsync(id);

        if (auction == null || auction.Status == Domain.Enums.AuctionStatus.NotApproved)
        {
            throw new KeyNotFoundException("Auction with such id does not exist.");
        }

        var response = new AuctionResponse()
        {
            Name = auction.Name,
            Description = auction.Description,
            Id = auction.Id,
            Status = auction.Status,
            StartDateTime = auction.StartDateTime,
            Score = auction.Scores.Any() ? auction.Scores.Average(score => score.Score) : 0,
            IsPaied = auction.IsPaid,
            Images = auction.Images.Select(x => new ImageDto { PublicId = x.PublicId, ImageUrl = x.Url }).ToList(),
            AuctionistUserId = auction.AuctionistId,
            AuctionistUsername = auction.Auctionist.Username,
            EndDateTime = auction.FinishDateTime,
            StartPrice = auction.StartPrice,
            FinishInterval = auction.FinishInterval.Hours
        };

        return response;
    }

    public async Task<ListModel<AuctionResponse>> GetAuctionsListAsync()
    {
        var filters = new AuctionFiltersDTO
        {
            PageSize = 50,
            PageNumber = 1,
            SortDirection = SortDirection.ASC
        };

        var specification = BuildSpecificationByFilter(filters);

        var auctions = await UnitOfWork.AuctionsRepository.GetAllAsync(specification);

        var totalCount = await UnitOfWork.AuctionsRepository.CountAsync(specification.Predicate!);

        var totalPages = (int)Math.Ceiling((double)totalCount / filters.PageSize);

        var listModel = new ListModel<AuctionResponse>()
        {
            Data = auctions
                .Select(x =>
                {
                    var response = new AuctionResponse()
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Id = x.Id,
                        Status = x.Status,
                        StartDateTime = x.StartDateTime,
                        Score = x.Scores.Any() ? x.Scores.Average(score => score.Score) : 0,
                        IsPaied = x.IsPaid,
                        Images =
                            x.Images.Select(x => new ImageDto { PublicId = x.PublicId, ImageUrl = x.Url }).ToList(),
                        AuctionistUserId = x.AuctionistId,
                        AuctionistUsername = x.Auctionist.Username,
                        EndDateTime = x.FinishDateTime,
                        StartPrice = x.StartPrice,
                        FinishInterval = x.FinishInterval.Hours
                    };

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

        var auctionToInsert = new Domain.Entities.Auction
        {
            Name = auction.Name,
            Status = AuctionStatus.NotApproved,
            Description = auction.Description,
            StartPrice = auction.StartPrice,
            AuctionistId = _userAccessor.GetCurrentUserId(),
            FinishInterval = TimeSpan.FromTicks(auction.FinishInterval),
            StartDateTime = DateTime.UtcNow
        };
        auctionToInsert.FinishDateTime = auctionToInsert.StartDateTime.Add(auctionToInsert.FinishInterval);
        auctionToInsert.Status = AuctionStatus.Active;

        var createdAuction = await UnitOfWork.AuctionsRepository.AddAsync(auctionToInsert);
        ArgumentNullException.ThrowIfNull(createdAuction);
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
            createdAuction.Images.Add(auctionImage);
        }
    }

    public async Task EditAuctionAsync(long id, EditAuctionRequest auction)
    {
        ArgumentNullException.ThrowIfNull(auction);
        var existentAuction = await UnitOfWork.AuctionsRepository.GetByIdAsync(id);
        if (existentAuction is null)
        {
            throw new KeyNotFoundException("Auction with such id does not exist.");
        }

        var currentAuctionist = _userAccessor.GetCurrentUserId();
        if (existentAuction.AuctionistId != currentAuctionist)
        {
            throw new InvalidOperationException("You are not the owner of this auction.");
        }

        existentAuction.Name = auction.Name;
        existentAuction.Description = auction.Description;
        existentAuction.StartPrice = auction.StartPrice;
        existentAuction.FinishInterval = TimeSpan.FromHours(auction.FinishInterval);
        existentAuction.FinishDateTime = existentAuction.StartDateTime.Add(existentAuction.FinishInterval);

        if (auction.OldImages is not null)
        {
            var photosToDelete = existentAuction.Images.Select(image => new { image.PublicId, image.Id })
                .Where(im => !auction.OldImages?.Contains(im.PublicId) ?? false);

            foreach (var photo in photosToDelete)
            {
                await _imagesService.DeleteImageAsync(photo.PublicId);
                await UnitOfWork.AuctionImagesRepository.DeleteByIdAsync(photo.Id);
            }
        }

        foreach (var image in auction.Images)
        {
            var uploadResult = await _imagesService.AddImageAsync(image);

            var auctionImage = new AuctionImage
            {
                AuctionId = existentAuction.Id,
                Url = uploadResult.Url.AbsoluteUri,
                PublicId = uploadResult.PublicId,
            };

            await UnitOfWork.AuctionImagesRepository.AddAsync(auctionImage);
        }

        await UnitOfWork.AuctionsRepository.UpdateAsync(existentAuction);
    }

    public async Task RecoverAuctionAsync(long id)
    {
        var auction = await UnitOfWork.AuctionsRepository.GetByIdAsync(id);

        if (auction == null || auction.Status == AuctionStatus.NotApproved)
        {
            throw new KeyNotFoundException("Auction with such id does not exist.");
        }

        if (auction.Status != AuctionStatus.Canceled)
        {
            throw new InvalidOperationException("Auction has not been canceled to recover.");
        }

        auction.Status = AuctionStatus.Active;

        await UnitOfWork.AuctionsRepository.UpdateAsync(auction);

        var specification = new SpecificationBuilder<Bid>()
            .With(x => x.AuctionId == auction.Id)
            .Build();

        var bids = await UnitOfWork.BidsRepository.GetAllAsync(specification);

        await UnitOfWork.BidsRepository.DeleteRangeAsync(bids);

        await UnitOfWork.AuctionsRepository.UpdateAsync(auction);
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
            if (filters.Status == Domain.Enums.AuctionStatus.Canceled ||
                filters.Status == Domain.Enums.AuctionStatus.NotApproved)
            {
                throw new ArgumentException("Cannot filter by such auction status.");
            }

            specificationBuilder.With(x => x.Status == filters.Status);
        }
        else
        {
            specificationBuilder.With(x =>
                x.Status != Domain.Enums.AuctionStatus.Canceled && x.Status != Domain.Enums.AuctionStatus.NotApproved);
        }

        specificationBuilder.WithInclude(
            x => x.Include(a => a.Images)
                .Include(a => a.Scores)
                .Include(a => a.Auctionist));

        specificationBuilder.WithPagination(filters.PageSize, filters.PageNumber);

        return specificationBuilder.Build();
    }
}