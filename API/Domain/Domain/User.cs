using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Domain;

[Table("Users")]
public class User
{
    [Key]
    public required string Id { get; set; } 
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public List<string> Items { get; set; } = [];
}
