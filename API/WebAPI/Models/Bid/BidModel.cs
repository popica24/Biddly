using Licenta.Models.Bid;

namespace Licenta.Models.Bidl;

public class BidModel
{
    public string BidId{ get; set; }
    public string BidName { get; set;}
    public string UserId { get; set; }
    public long HighestBid { get; set; }
    public long StartsFrom { get; set; }
    public DateTime StartsAt{ get; set; }
    public DateTime EndsAt{ get; set; }

    public BidModel()
    {
        
    }

    public BidModel(CreateBid model)
    {
        BidId = Guid.NewGuid().ToString();
        BidName = model.ItemName;
        UserId = model.UserId;
        HighestBid = 0;
        StartsFrom = model.StartsFrom;
        StartsAt = model.StartsAt;
        EndsAt = model.EndsAt;
    }
}
