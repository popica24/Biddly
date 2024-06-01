using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Utils;

namespace Services.Common.ApplicationConfigOptions;

public class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
   public void Configure(JwtOptions options)
    {
        configuration.GetSection(GlobalConstants.ConfigurationSections.Jwt).Bind(options);
    }
}
