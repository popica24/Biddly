using Business.Contracts;
using Business.Domain.ItemDomain;
using MapsterMapper;
using MediatR;

namespace Services.ItemsModule.Commands;

public sealed class AddItemQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AddItemQuery, bool>
{
    public async Task<bool> Handle(AddItemQuery request, CancellationToken cancellationToken)
    {
        unitOfWork.BeginTransaction();
        var item = mapper.Map<Item>(request);
        var added = await unitOfWork.ItemRepository.AddAsync(item);
        unitOfWork.CommitAndCloseConnection();
        return added;
    }
}
