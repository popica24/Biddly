namespace Licenta.Models.Bid;

public record CreateBidRequest(string CreatedBy, long StartingFrom,string ItemName, DateTime StartingAt, DateTime WonAt);

