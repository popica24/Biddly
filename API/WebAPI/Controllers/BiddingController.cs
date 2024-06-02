using Business.Domain.BidDomain;
using Licenta.Hubs;
using Licenta.Models.Bid;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Services.BidsModule.Commands.CreateBid;
using Services.BidsModule.Queries.GetBid;

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
        var getBidQuery = new GetBidQuery(bidId);

        var bid = await sender.Send(getBidQuery);

        return Ok(bid);
    }
}
