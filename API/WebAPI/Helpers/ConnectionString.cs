using Services;

namespace WebAPI.Helpers;

public class ConnectionString(string conenctionString) : IConnectionString
{
    public string SqlConnectionString { get; private set; } = conenctionString;
}
