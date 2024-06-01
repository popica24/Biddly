namespace Services.Common.DTO.Bid;

public record RunningBid(string BidId, string CreatedBy, string WonBy, DateTime WonAt, long HighestBid, long StartingFrom, DateTime StartingAt, DateTime EndingAt);

