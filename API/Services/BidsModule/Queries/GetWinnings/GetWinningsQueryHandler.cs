using Business.Contracts;
using Business.Domain.ItemDomain;
using MapsterMapper;
using MediatR;
using Services.Common.DTO.Bid;

namespace Services.BidsModule.Queries.GetWinnings;

public sealed class GetWinningsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetWinningsQuery, IEnumerable<PastBid>>
{
    public async Task<IEnumerable<PastBid>> Handle(GetWinningsQuery request, CancellationToken cancellationToken)
    {
        var items = await unitOfWork.ItemRepository.GetByColumnAsync("wonby", request.userId);

        return items;
    }
}
