using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.UserModule.Queries.GetUser;
using System.IdentityModel.Tokens.Jwt;
using WebAPI.Utils;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class UserController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        var idToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

        JwtSecurityToken token = new(idToken);

        var userId = TokenDecoder.Decode(token).Subject;

        var getUserQuery = new GetUserQuery(userId);

        var user = await sender.Send(getUserQuery);

        return Ok(user);
    }
}
