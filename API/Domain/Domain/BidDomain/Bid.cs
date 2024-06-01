namespace Business.Domain.BidDomain;

public class Bid
{
    public string BidId { get; set; }

    public string CreatedBy { get; set; }

    public string WonBy { get; set; } = string.Empty;

    public DateTime WonAt { get; set; }

    public long HighestBid { get; set; } = 0;

    public string HighestUserBid { get; set; } = string.Empty;

    public long StartingFrom { get; set; }
}
