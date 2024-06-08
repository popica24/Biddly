namespace Services.Utils;

public static class GlobalConstants
{
    public const string RefreshTokenCookieKey = "refreshToken";

    public const string AccessTokenCookieKey = "Bearer";

    public static class RedisKeys
    {
        public const string RunningBids = "running-bids";

        public static string BidId(string bidId)
        {
            return $"bid-{bidId}";
        }
    }

    public static class ConfigurationSections
    {
        public const string Jwt = "Jwt";
    }
}
