using Business.Contracts;
using MapsterMapper;
using MediatR;
using Services.Common.DTO;

namespace Services.UserModule.Queries.GetUser;

public sealed class GetUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUserQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var response = (await unitOfWork.UserRepository.GetByColumnAsync("id", request.userId, "id", "username", "email")).FirstOrDefault();

        if(response == null)
        {
            return null;
        }
        var userResponse = mapper.Map<UserResponse>(response);

        return userResponse;
    }
}
