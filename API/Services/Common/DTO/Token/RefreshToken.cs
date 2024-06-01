namespace Services.Common.DTO.Token;

public class RefreshToken
{
    public string TokenValue { get; set; }

    public DateTime Expires { get; set; }
}
