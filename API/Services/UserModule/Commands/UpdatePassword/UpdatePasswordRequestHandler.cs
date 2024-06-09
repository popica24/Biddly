using Business.Contracts;
using MediatR;

namespace Services.UserModule.Commands.UpdatePassword;

public sealed class UpdatePasswordRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdatePasswordRequest, bool>
{
    public async Task<bool> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
    {
        var user = (await unitOfWork.UserRepository.GetByColumnAsync("id", request.userId)).First();

        if (user == null)
        {
            return false;
        }

        if (BCrypt.Net.BCrypt.Verify(request.oldPassword, user.PasswordHash)){
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.newPassword);
            user.RefreshToken = "";
            user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(-1);
            unitOfWork.BeginTransaction();
            var result = await unitOfWork.UserRepository.UpdateAsync(user);
            unitOfWork.CommitAndCloseConnection();
            return result;
        }
        return false;
    }
}
