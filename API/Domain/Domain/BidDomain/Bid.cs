namespace Business.Domain.BidDomain;

public class Bid
{
    public string BidId { get; set; }

    public string CreatedBy { get; set; }

    public string ItemName { get; set; }

    public string WonBy { get; set; } = string.Empty;

    public DateTime StartingAt { get; set; }

    public DateTime WonAt { get; set; }

    public long HighestBid { get; set; } = 0;

    public long StartingFrom { get; set; }
}
