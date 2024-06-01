using Business.Contracts;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Common.DTO.AuthRequest;
using Services.UserModule.Commands.RefreshToken;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController(IMapper mapper, ISender sender, IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequst request)
        {
            var refreshTokenCommand = mapper.Map<RefreshTokenCommand>(request);

            var result= await sender.Send(refreshTokenCommand);

            return !string.IsNullOrEmpty(result) ? Ok(result) : Problem(result);
        }
    }
}
