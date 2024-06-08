namespace WebAPI.Models.Token;

public class DecodedToken
{
    public string KeyId { get; set; }
    public string Issuer { get; set; }
    public IEnumerable<string> Audience { get; set; }
    public IEnumerable<(string Type, string Value)> Claims { get; set; }
    public DateTime ValidTo { get; set; }
    public string SignatureAlgorithm { get; set; }
    public string RawData { get; set; }
    public string Subject { get; set; }
    public DateTime ValidFrom { get; set; }
    public string EncodedHeader { get; set; }
    public string EncodedPayload { get; set; }
}
