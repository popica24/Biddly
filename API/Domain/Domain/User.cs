namespace Business.Domain;

public class User
{
    public required string Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public List<Item> Items { get; set; } = new();
}
