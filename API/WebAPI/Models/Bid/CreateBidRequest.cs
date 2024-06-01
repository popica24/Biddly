namespace Licenta.Models.Bid;

public record CreateBidRequest(string CreatedBy, string WonBy, DateTime WonAt, long StartingFrom, DateTime StartingAt, DateTime EndingAt);

