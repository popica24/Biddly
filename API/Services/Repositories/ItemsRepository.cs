using Business.Contracts;
using Business.Domain.ItemDomain;
using Services.Context;

namespace Services.Repositories;

public sealed class ItemsRepository(SqlDataContext context) : GenericRepository<Item>(context), IItemRepository
{
}
