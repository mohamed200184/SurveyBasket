
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyBasket.api.Authentication
{
    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly IOptions<JwtOptions> _options = options;

        public (string token, int expiresIn) GenrateToken(ApplicationUser user)
        {
            Claim[] claims = [
                new(JwtRegisteredClaimNames.Sub,user.Id)
                ,new(JwtRegisteredClaimNames.Email,user.Email!)
                ,new(JwtRegisteredClaimNames.GivenName,user.FirstName)
                ,new(JwtRegisteredClaimNames.FamilyName,user.LastName)
                ,new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                ];


            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var expiresIn = _options.Value.ExpirationInMinutes;
            var expirationDate = DateTime.UtcNow.AddMinutes(expiresIn);
            var token = new JwtSecurityToken(issuer: _options.Value.Issuer, audience: _options.Value.Audience,claims: claims, expires: DateTime.UtcNow.AddMinutes(expiresIn), signingCredentials: signingCredentials);

            return (token:new JwtSecurityTokenHandler().WriteToken(token), expiresIn *  60);
        }

        public string? ValidateToken(string token)
        {
           var tokenHandler = new JwtSecurityTokenHandler();
           var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));

             try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                  
                    IssuerSigningKey = symmetricSecurityKey,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                  
                 
                   
                  
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                return jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
            }
            catch
            {
                return null;
            }
        }
    }
}
