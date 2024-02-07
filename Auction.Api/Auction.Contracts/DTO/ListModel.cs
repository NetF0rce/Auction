namespace Auction.Contracts.DTO;

public class ListModel<T>
{
    public List<T> Data { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
