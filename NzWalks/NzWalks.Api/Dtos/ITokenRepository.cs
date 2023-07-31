using Microsoft.AspNetCore.Identity;

namespace NzWalks.Api.Dtos
{
    public interface ITokenRepository
    {
        Task<string> CreateJwtToken(IdentityUser identityUser, List<string> roles);
    }
}
