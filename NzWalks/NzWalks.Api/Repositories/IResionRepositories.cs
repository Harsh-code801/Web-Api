using Microsoft.AspNetCore.Mvc;
using NzWalks.Api.Models.Domain;

namespace NzWalks.Api.Repositories
{
    public interface IResionRepositories
    {
        Task<List<Region>> GetRegions();
        Task<Region?> GetRegionsById(Guid id);
        Task<Region> AddRegion(Region region);
        Task<Region?> UpdateRegion(Guid id, [FromBody] Region region);
        Task<Region?> DeleteRegion([FromRoute] Guid id);
    }
}
