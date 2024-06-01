using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Domain.UserDomain;

[Table("Users")]
public class User
{
    [Key]
    [Column("id")]
    public required string Id { get; set; }
    [Column("username")]
    public string UserName { get; set; } = string.Empty;
    [Column("email")]
    public string Email { get; set; }
    [Column("passwordhash")]
    public string PasswordHash { get; set; }
    [Column("refreshtoken")]
    public string RefreshToken { get; set; }
    [Column("refreshtokenexpirydate")]
    public DateTime RefreshTokenExpiryDate { get; set; }
    [Column("items")]
    public string[] Items { get; set; } = [];
}
