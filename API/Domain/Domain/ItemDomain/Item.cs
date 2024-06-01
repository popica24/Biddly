using Business.Domain.UserDomain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Domain.ItemDomain;

public class Item
{
    [Key]
    public required string Id { get; set; }
    [Column]
    public string Name { get; set; } = string.Empty;
    [Column]
    public string UserId { get; set; }
    [Column]
    public long BidWon { get; set; }
    [Column]
    public DateTime WonAt { get; set; }
}
