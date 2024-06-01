using Licenta.Models.Bid;
using Mapster;
using Services.Common.DTO.Bid;

namespace WebAPI.Mappings;

public sealed class ConfigureMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateBidRequest, RunningBid>()
            .Map(dest => dest.BidId, src => Guid.NewGuid().ToString())
            .Map(dest => dest.HighestBid, src => 0)
            .Map(dest => dest.WonBy, src => string.Empty);
    }
}
