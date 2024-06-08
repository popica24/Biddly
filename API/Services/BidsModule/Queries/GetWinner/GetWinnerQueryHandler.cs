using Business.Contracts;
using MediatR;

namespace Services.BidsModule.Queries.GetWinner;

public sealed class GetWinnerQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetWinnerQuery, string>
{
    public async Task<string> Handle(GetWinnerQuery request, CancellationToken cancellationToken)
    {
        var bid = (await unitOfWork.ItemRepository.GetByColumnAsync("bidid",request.bidId,"wonby")).FirstOrDefault();

        if(bid == null)
        {
            return "";
        }

        var user = (await unitOfWork.UserRepository.GetByColumnAsync("id", bid.WonBy, "username")).FirstOrDefault();

        if(user == null)
        {
            return "";
        }

        return user.UserName;
    }
}
