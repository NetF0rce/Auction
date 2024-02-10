namespace Auction.Core.Exceptions;

public class AuctionExpiredException() : ArgumentException("Auction has already been expired");