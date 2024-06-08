using Business.Domain.BidDomain;
using Business.Domain.ItemDomain;
using Business.Domain.UserDomain;
using Licenta.Models.Bid;
using Mapster;
using Services.Common.DTO;
using Services.Common.DTO.Bid;
using Services.Utils;

namespace WebAPI.Mappings;

public sealed class ConfigureMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateBidRequest, Bid>()
            .Map(dest => dest.BidId, src => GlobalConstants.RedisKeys.BidId(Guid.NewGuid().ToString()))
            .Map(dest => dest.StartingAt, src => DateTime.Parse(src.StartingAt))
            .Map(dest => dest.WonAt, src => DateTime.Parse(src.WonAt))
            .Map(dest => dest.HighestBid, src => 0)
            .Map(dest => dest.WonBy, src => string.Empty);

        config.NewConfig<Bid, PastBid>()
            .Map(dest => dest.Id, src => Guid.NewGuid().ToString());

        config.NewConfig<PastBid, PastBidResponse>()
            .Map(dest => dest.WonAt, src => src.WonAt.ToShortDateString());

        config.NewConfig<User, UserResponse>()
            .Map(dest => dest.id, src => src.Id)
            .Map(dest => dest.Username, src => src.UserName)
            .Map(dest => dest.Email, src => src.Email);

    }
}
