namespace Licenta.Models.Bid;

public class CreateBidRequest
{
    public string CreatedBy { get; set; }
    public long StartingFrom { get; set; }
    public string ItemName { get; set; }
    public string StartingAt { get; set; }
    public string WonAt { get; set; }
}
