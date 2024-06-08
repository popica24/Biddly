using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.BidsModule.Queries.LatestPastBids;
using Services.Common.DTO.Bid;
using WebAPI.Models.Bid;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PastBidsController(ISender sender, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var getLatestPastBidsQuery = new GetLatestPastBidsQuery();

            var pastBids = await sender.Send(getLatestPastBidsQuery);

            var pastBidsResponse = new List<PastBidResponse>();

            foreach (var pastBid in pastBids)
            {
                var bidResponse = mapper.Map<PastBidResponse>(pastBid);
                pastBidsResponse.Add(bidResponse);
            }

            return Ok(pastBidsResponse);
        }
    }
}
