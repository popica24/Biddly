using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Domain.ItemDomain;

public class PastBid
{
    [Key]
    [Column("id")]
    public required string Id { get; set; }

    [Column("bidid")]
    public string BidId { get; set; }

    [Column("createdby")]
    public string CreatedBy { get; set; }

    [Column("itemname")]
    public string ItemName { get; set; }

    [Column("wonby")]
    public string WonBy { get; set; }

    [Column("highestbid")]
    public long HighestBid { get; set; }

    [Column("wonat")]
    public DateTime WonAt { get; set; }

    [Column("username")]
    public string Username { get; set; }
}
