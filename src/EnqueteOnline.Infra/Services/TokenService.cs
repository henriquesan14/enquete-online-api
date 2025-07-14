using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Contracts.Services.Response;
using EnqueteOnline.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EnqueteOnline.Infra.Services
{
    public class TokenService(IConfiguration _configuration) : ITokenService
    {
        public AuthTokenResult GenerateAccessToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["TokenSettings:Secret"]!);
            var accessTokenExpiration = DateTime.Now.AddHours(1);
            var refreshTokenExpiration = DateTime.Now.AddDays(7);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.Value.ToString()),
                    new Claim(ClaimTypes.Name, user.Nome),
                }),
                NotBefore = DateTime.Now,
                Expires = accessTokenExpiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            var refreshToken = Guid.NewGuid().ToString();

            return new AuthTokenResult(accessToken, refreshToken, accessTokenExpiration, refreshTokenExpiration);
        }
    }
}
