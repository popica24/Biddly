using Licenta.Hubs;
using Licenta.Models.Bid;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Services.CacheService;
using Services.Common.DTO.Bid;
using Services.Utils;

namespace Licenta.Controllers
{
    [Route("api/[controller]")]
    [Authorize] 
    [ApiController]
    public class BidController(IHubContext<BidHub, IBidsHubClient> biddingHub, ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Index (PlaceBidRequest model)
        {
            return Ok();
        }
    }
}
