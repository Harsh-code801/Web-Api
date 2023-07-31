using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NzWalks.Api.Dtos
{
    public class JwtToken : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public JwtToken(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> CreateJwtToken(IdentityUser identityUser, List<string> roles)
        {
            var claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.Email, identityUser.Email));

            foreach (var role in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var credencial = new SigningCredentials(key, SecurityAlgorithms.Aes128CbcHmacSha256);

            var tocken = new JwtSecurityToken(
                issuer: configuration["JWT:IsUser"],
                audience: configuration["JWT:Audience"],
                claim,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credencial
                );
            return new JwtSecurityTokenHandler().WriteToken(tocken);
        }
    }
}
