using Licenta.Models.Bidl;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.CacheService;

namespace Licenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LatestBidsController(ICacheService cacheService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var latestBidsJson = cacheService.GetData<string>("latest-bids");
            List<string> latestBidsId = string.IsNullOrEmpty(latestBidsJson)
                ? []
                : JsonConvert.DeserializeObject<List<string>>(latestBidsJson);
            var latestBids = new List<BidModel>();
            foreach(var id in latestBidsId.TakeLast(5))
            {
                var bidJson = cacheService.GetData<string>($"bid-{id}");
                var bid = JsonConvert.DeserializeObject<BidModel>(bidJson);
                latestBids.Add(bid);

            }
            return Ok(latestBids);
        }
    }
}
