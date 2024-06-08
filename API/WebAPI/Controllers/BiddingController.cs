using Business.Domain.BidDomain;
using Licenta.Hubs;
using Licenta.Models.Bid;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Services.BidsModule.Commands.CreateBid;
using Services.BidsModule.Queries.GetBid;
using Services.BidsModule.Queries.GetPastBid;
using WebAPI.Models.Bid;

namespace Licenta.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BiddingController(IHubContext<BidHub, IBidsHubClient> biddingHub,ISender sender, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateBidRequest model)
    {

        var bidModel = mapper.Map<Bid>(model);

        var createBidCommand = mapper.Map<CreateBidCommand>(bidModel);

       var bidCreated = await sender.Send(createBidCommand);

        if (bidCreated)
        {
            await biddingHub.Clients.All.UpdateLatestBids();

            return Ok();
        }
        return BadRequest();
    }



    [HttpGet("{bidId}")]
    public async Task<IActionResult> GetAsync(string bidId)
    {
        var key = bidId.Replace("bid-", "");
        var getBidQuery = new GetBidQuery(key);

        var bid = await sender.Send(getBidQuery);

        if(bid == null)
        {
            var getPastBidQuery = new GetPastBidQuery(key);

            var pastBid = await sender.Send(getPastBidQuery);

            if(pastBid == null)
            {
                return NotFound();
            }

            return Ok(pastBid);
        }

        var bidModel = mapper.Map<BidResponse>(bid);

        return Ok(bidModel);
    }
}
