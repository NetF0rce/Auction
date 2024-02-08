using Auction.Contracts.Enums;

namespace Auction.Contracts.DTO;

public class BaseFiltersDTO
{
    public string? Search { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}
