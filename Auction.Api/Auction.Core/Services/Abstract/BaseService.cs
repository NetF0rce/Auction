using Auction.Core.Interfaces.Data;
using AutoMapper;

namespace Auction.Core.Services.Abstract;

public abstract class BaseService
{
    protected readonly IUnitOfWork UnitOfWork;
    protected readonly IMapper Mapper;

    protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
}