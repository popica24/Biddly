using Business.Contracts;
using Business.Domain.ItemDomain;
using Dapper;
using Services.Context;

namespace Services.Repositories;

public sealed class ItemsRepository(SqlDataContext context) : GenericRepository<PastBid>(context), IItemRepository
{
    //Metode specifice pentru operatiuni pe entitati de tip Item
    public async Task<PastBid> Get(string bidId)
    {
        try
        {
            string sql = @"
            SELECT pb.*, u.username
            FROM (
                SELECT bidid, itemname, wonat, highestbid, wonby
                FROM pastbid
                WHERE bidid = @bidId
            ) pb
            INNER JOIN users u ON pb.wonby = u.id;
        ";

            var parameters = new { bidId };

            PastBid? pastbid = await context.Connection.QueryFirstOrDefaultAsync<PastBid>(sql, parameters);

            return pastbid;
        }
        catch (Exception ex)
        {

        }
        return null;
    }

    public async Task<List<PastBid>> GetLatest(int items = 5)
    {
        string sql = $@"
                        SELECT pb.*, u.username
                        FROM (
                        SELECT bidid, itemname, wonat, highestbid, wonby
                        FROM pastbid
                        ORDER BY wonat DESC
                        LIMIT {items}
                        ) pb
                        INNER JOIN users u ON pb.wonby = u.id;
                        ";

        var pastbids = await context.Connection.QueryAsync<PastBid>(sql);

        return pastbids.ToList();
    }
}
