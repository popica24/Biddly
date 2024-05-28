namespace Business.Domain;

public class Item
{
    public required string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UserId { get; set; }
    public long BidWon { get; set; }
    public DateTime WonAt { get; set; }

    public User User { get; set; }
}
