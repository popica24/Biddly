namespace WebAPI.Models.Bid;

public record BidResponse(string BidId, string ItemName, DateTime StartingAt, DateTime WonAt, long HighestBid, long StartingFrom, bool PastBid, string CreatedBy);

