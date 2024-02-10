namespace Auction.Core.Exceptions;

public class UnstartedAuctionException(): ArgumentException("Current auction haven't been began");