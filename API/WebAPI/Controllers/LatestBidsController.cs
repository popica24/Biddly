using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.BidsModule.Queries.LatestBids;

namespace Licenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LatestBidsController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            GetLatestBidsQuery query = new();
            var latestBids = await sender.Send(query);
            return Ok(latestBids);
        }
    }
}
