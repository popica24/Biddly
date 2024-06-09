using Licenta.Hubs;
using Licenta.Models.Bid;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Services.BidsModule.Commands.PlaceBid;

namespace Licenta.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class BidController(IHubContext<BidHub, IBidsHubClient> biddingHub, ISender sender, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public IActionResult Index (PlaceBidRequest model)
        {
            var placeBidCommand = mapper.Map<PlaceBidCommand>(model);

            var response = sender.Send(placeBidCommand);

            var result = response.Result;

            if (result)
            {
               biddingHub.Clients.All.UpdateHighestBid();
               biddingHub.Clients.All.UpdateLatestBids();
            }

            return Ok(result);
        }
    }
}
