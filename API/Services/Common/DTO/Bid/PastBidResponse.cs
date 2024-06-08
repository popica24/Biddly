namespace Services.Common.DTO.Bid;

public record PastBidResponse(string BidId, string ItemName, string WonAt, long HighestBid, string Username);
