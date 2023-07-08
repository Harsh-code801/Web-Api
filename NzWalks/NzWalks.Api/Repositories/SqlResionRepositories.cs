using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NzWalks.Api.Data;
using NzWalks.Api.Models.Domain;

namespace NzWalks.Api.Repositories
{
    public class SqlResionRepositories : IResionRepositories
    {
        private readonly NzWalksDbContext dbContext;

        public SqlResionRepositories(NzWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> AddRegion(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteRegion([FromRoute] Guid id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(region != null)
            {
                dbContext.Regions.Remove(region);
                await dbContext.SaveChangesAsync();
                return region;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Region>> GetRegions()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionsById(Guid id)
        {
            var region =  await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region!=null)
            {
                return region;
            }else
                return null;
        }

        public async Task<Region?> UpdateRegion(Guid id, [FromBody] Region region)
        {
            var regionupdate = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionupdate != null)
            {
                regionupdate.Name = region.Name;
                regionupdate.Code = region.Code;
                regionupdate.RegionImageUrl = region.RegionImageUrl;

                await dbContext.SaveChangesAsync();
                return regionupdate;
            }
            else
                return null;
        }
    }
}
