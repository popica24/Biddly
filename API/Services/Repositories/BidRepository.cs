using Business.Contracts;
using Business.Domain.BidDomain;
using Newtonsoft.Json;
using Services.Utils;
using StackExchange.Redis;

namespace Services.Repositories;

public class BidRepository : IBidRepository
{
    private readonly IDatabase cacheDb;
    public BidRepository()
    {
        string connectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION")!;
        var redis = ConnectionMultiplexer.Connect(connectionString);
        cacheDb = redis.GetDatabase();
    }

    public bool BidToItem(string bidId, string userId, long ammount)
    {
        var bidExists = cacheDb.KeyExists(bidId);
        if (!bidExists)
        {
            return false;
        }

        var bid = cacheDb.StringGet(GlobalConstants.RedisKeys.BidId(bidId));
    }

    public Bid? GetBid(string bidId)
    {
        var bidExists = cacheDb.KeyExists(bidId);

        if (!bidExists)
        {
            return null;
        }
        
        var serializedBid = cacheDb.StringGet(bidId);

        var bid = JsonConvert.DeserializeObject<Bid>(serializedBid);

        return bid;

    }

    public IEnumerable<string> GetRunningBids()
    {
        var bids = cacheDb.StringGet(GlobalConstants.RedisKeys.RunningBids);
        if(!string.IsNullOrEmpty(bids))
        {
            return JsonConvert.DeserializeObject<IEnumerable<string>>(bids);
        }
        return [];
    }

    public bool PushRunningBid(string value)
    {
        var serializedBids = cacheDb.StringGet(GlobalConstants.RedisKeys.RunningBids);

        IEnumerable<string> bidIds;

        if (string.IsNullOrEmpty(serializedBids))
        {
            bidIds = [];
        }
        else
        {
            bidIds = JsonConvert.DeserializeObject<IEnumerable<string>>(serializedBids);
        }

        IEnumerable<string> newBids = bidIds.Append(value);

        var serializedNewBids = JsonConvert.SerializeObject(newBids);

        TimeSpan expireTime = TimeSpan.FromHours(24);

        return cacheDb.StringSet(GlobalConstants.RedisKeys.RunningBids, serializedNewBids, expireTime);

    }

    public bool RemoveRunningBid(string bidId)
    {
        throw new NotImplementedException();
    }

    public bool SetRunninBid(Bid value, DateTimeOffset expirationTime)
    {
        try
        {
            var expireTime = expirationTime.DateTime.Subtract(DateTime.Now);

            var json = JsonConvert.SerializeObject(value);

            var key = GlobalConstants.RedisKeys.BidId(value.BidId);

            cacheDb.StringSet(key, json, expireTime);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
