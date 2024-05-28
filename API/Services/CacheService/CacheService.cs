using Newtonsoft.Json;
using StackExchange.Redis;

namespace Services.CacheService;

public class CacheService : ICacheService
{
    private readonly IDatabase _cacheDb;
    public CacheService()
    {
       /* string connectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION")!;*/
        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        _cacheDb = redis.GetDatabase();
    }

    public T GetData<T>(string key)
    {
        var value = _cacheDb.StringGet(key);
        if (!string.IsNullOrEmpty(value))
            return JsonConvert.DeserializeObject<T>(value);
        return default;

    }

    public object RemoveData(string key)
    {
        var _exist = _cacheDb.KeyExists(key);

        return _exist ? _cacheDb.KeyDelete(key) : false;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);

        return _cacheDb.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
    }
}
