
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.UserModule.Commands.Logout;
using System.IdentityModel.Tokens.Jwt;
using WebAPI.Utils;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(ISender sender)
        {
            var idToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            JwtSecurityToken token = new(idToken);

            var userId = TokenDecoder.Decode(token).Subject;

            var logoutCommand = new LogoutCommand(userId);

            var result = await sender.Send(logoutCommand);

            return result ? Ok() : BadRequest();
        }
    }
}
