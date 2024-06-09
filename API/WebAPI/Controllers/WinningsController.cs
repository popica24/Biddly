using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.BidsModule.Queries.GetWinnings;
using System.IdentityModel.Tokens.Jwt;
using WebAPI.Utils;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WinningsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var idToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

        JwtSecurityToken token = new(idToken);

        var userId = TokenDecoder.Decode(token).Subject;

        var getWinningsQuery = new GetWinningsQuery(userId);

        var winnings = await sender.Send(getWinningsQuery);

        return Ok(winnings);
    }
}
