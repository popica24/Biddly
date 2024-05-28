namespace Business.Domain;

public class Bid
{
    public required string Id { get; set; }
    public string ItemId { get; set; }
    public string UserId { get; set; }
    public long Ammount { get; set; }

    public User User { get; set; }
    public Item Item { get; set; }
}
