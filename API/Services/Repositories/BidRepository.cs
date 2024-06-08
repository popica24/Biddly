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
        try
        {
            var bid = GetBid(bidId);

            if (bid == null)
            {
                return false;
            }

            if (bid.HighestBid < ammount && bid.StartingFrom < ammount)
            {
                bid.HighestBid = ammount;
                bid.WonBy = userId;
                SetRunninBid(bid, DateTime.UtcNow.AddDays(7));
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public Bid? GetBid(string bidId)
    {
        if (string.IsNullOrEmpty(bidId))
        {
            return null;
        }

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

    public bool Persist(Bid bid)
    {
        return true;
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
        var newRunningBids = GetRunningBids().Where(id => id != bidId);

        var newRunningBidsSerialized = JsonConvert.SerializeObject(newRunningBids);

        TimeSpan expireTime = TimeSpan.FromDays(7);

        cacheDb.StringSet(GlobalConstants.RedisKeys.RunningBids, newRunningBidsSerialized, expireTime);

        cacheDb.KeyDelete(bidId);

        return true;
    }

    public bool SetRunninBid(Bid value, DateTimeOffset expirationTime)
    {
        try
        {
            var expireTime = expirationTime.DateTime.Subtract(DateTime.Now);

            var json = JsonConvert.SerializeObject(value);

            cacheDb.StringSet(value.BidId, json, expireTime);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
