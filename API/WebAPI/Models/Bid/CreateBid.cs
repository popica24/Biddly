namespace Licenta.Models.Bid;

public class CreateBid
{
    public string ItemName { get; set; }
    public DateTime EndsAt { get; set; }
    public DateTime StartsAt { get; set; }
    public long StartsFrom { get; set; }
    public string UserId { get; set; }
}
