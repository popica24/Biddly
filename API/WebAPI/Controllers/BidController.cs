using Licenta.Hubs;
using Licenta.Models.Bid;
using Licenta.Models.Bidl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Services.CacheService;

namespace Licenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController(IHubContext<BidHub, IBidsHubClient> biddingHub, ICacheService cacheService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Bid (PlaceBid model)
        {
            var bidModelJson = cacheService.GetData<string>($"bid-{model.BidId}");
            var bidModel = JsonConvert.DeserializeObject<BidModel>(bidModelJson);
            if(bidModel.HighestBid < model.Ammount && bidModel.StartsFrom < model.Ammount)
            {
                bidModel.HighestBid = model.Ammount;
                var newBidModelJson = JsonConvert.SerializeObject(bidModel);
                cacheService.SetData($"bid-{model.BidId}", newBidModelJson, DateTime.Now.AddDays(7));
                await biddingHub.Clients.All.UpdateHighestBid();
                return Ok();
            }
            return NoContent();
        }
    }
}
