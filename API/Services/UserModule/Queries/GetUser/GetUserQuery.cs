using MediatR;
using Services.Common.DTO;

namespace Services.UserModule.Queries.GetUser;

public record GetUserQuery(string userId) : IRequest<UserResponse>;
