using System.IdentityModel.Tokens.Jwt;
using WebAPI.Models.Token;
using System.Linq;

namespace WebAPI.Utils
{
    public static class TokenDecoder
    {
        public static DecodedToken Decode(JwtSecurityToken token)
        {
            var keyId = token.Header.Kid;
            var audience = token.Audiences.ToList();
            var claims = token.Claims.Select(claim => (claim.Type, claim.Value)).ToList();

            return new DecodedToken
            {
                KeyId = keyId,
                Issuer = token.Issuer,
                Audience = audience,
                Claims = claims,
                ValidTo = token.ValidTo,
                SignatureAlgorithm = token.SignatureAlgorithm,
                RawData = token.RawData,
                Subject = token.Subject,
                ValidFrom = token.ValidFrom,
                EncodedHeader = token.EncodedHeader,
                EncodedPayload = token.EncodedPayload
            };
        }
    }
}
