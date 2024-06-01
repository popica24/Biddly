using MediatR;
using Services.Common.DTO;

namespace Services.UserModule.Queries.LoginUser;

public record LoginUserQuery(string Email, string Password) : IRequest<string>;

