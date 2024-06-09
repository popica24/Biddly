using Business.Contracts;
using MediatR;

namespace Services.UserModule.Commands.UpdateUsername;

public sealed class UpdateUsernameRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateUsernameRequest, bool>
{
    public async Task<bool> Handle(UpdateUsernameRequest request, CancellationToken cancellationToken)
    {
        var user = (await unitOfWork.UserRepository.GetByColumnAsync("id", request.userId)).First();

        if (user == null)
        {
            return false;
        }

        user.UserName = request.newUsername;

        unitOfWork.BeginTransaction();

        var result = await unitOfWork.UserRepository.UpdateAsync(user);

        unitOfWork.CommitAndCloseConnection();

        return result;
    }
}

