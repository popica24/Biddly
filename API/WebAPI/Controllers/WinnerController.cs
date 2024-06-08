using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.BidsModule.Queries.GetWinner;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WinnerController(ISender sender) : ControllerBase
{
    [HttpGet("{bidId}")]
    public async Task<IActionResult> Index(string bidId)
    {
        var getWinnerQuery = new GetWinnerQuery(bidId);

        var userName = await sender.Send(getWinnerQuery);

        return Ok(userName);
    }
}
