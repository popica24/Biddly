using Licenta.Hubs;
using Licenta.Models.Bid;
using Licenta.Models.Bidl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Services.CacheService;

namespace Licenta.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BiddingController(IHubContext<BidHub, IBidsHubClient> biddingHub, ICacheService cacheService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateBid model)
    {
        var latestBidsJson = cacheService.GetData<string>("latest-bids");
        List<string> latestBidsId = string.IsNullOrEmpty(latestBidsJson)
            ? []
            : JsonConvert.DeserializeObject<List<string>>(latestBidsJson);

        var bidModel = new BidModel(model);
        latestBidsId.Add(bidModel.BidId);

        var updatedBidsJson = JsonConvert.SerializeObject(latestBidsId);
        var createdBidJson = JsonConvert.SerializeObject(bidModel);
        cacheService.SetData("latest-bids", updatedBidsJson, DateTime.UtcNow.AddDays(7));
        cacheService.SetData($"bid-{bidModel.BidId}", createdBidJson, DateTime.UtcNow.AddDays(7));
        await biddingHub.Clients.All.UpdateLatestBids();
        return Ok();
    }



    [HttpGet("{bidId}")]
    public async Task<IActionResult> GetAsync(string bidId)
    {
        var bidJson = cacheService.GetData<string>($"bid-{bidId}");
        if (string.IsNullOrEmpty(bidJson))
        {
            return NotFound();
        }

        var bid = JsonConvert.DeserializeObject<BidModel>(bidJson);
        return Ok(bid);
    }
}
