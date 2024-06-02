using Business.Domain.BidDomain;
using Business.Domain.ItemDomain;
using Licenta.Models.Bid;
using Mapster;
using Services.Utils;

namespace WebAPI.Mappings;

public sealed class ConfigureMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateBidRequest, Bid>()
            .Map(dest => dest.BidId, src => GlobalConstants.RedisKeys.BidId(Guid.NewGuid().ToString()))
            .Map(dest => dest.HighestBid, src => 0)
            .Map(dest => dest.WonBy, src => string.Empty);

        config.NewConfig<Bid, Item>()
            .Map(dest => dest.ItemId, src => Guid.NewGuid().ToString());

    }
}
