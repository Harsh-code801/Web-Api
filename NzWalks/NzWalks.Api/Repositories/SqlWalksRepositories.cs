using Microsoft.EntityFrameworkCore;
using NzWalks.Api.Data;
using NzWalks.Api.Models.Domain;

namespace NzWalks.Api.Repositories
{
    public class SqlWalksRepositories : IWalks
    {
        private readonly NzWalksDbContext dbContext;

        public SqlWalksRepositories(NzWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Walks>> GetWalks()
        {
            return (await dbContext.walks.Include(x => x.Difficulty).Include(x => x.Region).ToListAsync());
        }
        public async Task<Walks?> GetWalksById(Guid id)
        {
            return await dbContext.walks.Include(x => x.Region).Include(x => x.Difficulty).FirstOrDefaultAsync(x=>x.Id == id);
        }
        public async Task<Walks> AddWalk(Walks walks)
        {
            await dbContext.walks.AddAsync(walks);
            await dbContext.SaveChangesAsync();
            return walks;
        }
        public async Task<Walks?> UpdateWalk(Guid id, Walks walks)
        {
            var walk = await dbContext.walks.Include(x => x.Region).Include(x => x.Difficulty).FirstOrDefaultAsync(x => x.Id == id);
            if (walk != null)
            {
                walk.WalkImageUrl = walks.WalkImageUrl;
                walk.DifficultyId = walks.DifficultyId;
                walk.RegionId = walks.RegionId;
                walk.Name = walks.Name;
                walk.Description = walks.Description;
                walk.LengthInKm = walks.LengthInKm;
                await dbContext.SaveChangesAsync();
                return walks;
            }
            return null;
        }
        public async Task<Walks?> DeleteWalk(Guid id)
        {
            var walkData = await dbContext.walks.Include(x => x.Region).Include(x => x.Difficulty).FirstOrDefaultAsync(x => x.Id == id);
            if (walkData != null)
            {
                dbContext.walks.Remove(walkData);
                await dbContext.SaveChangesAsync();
                return walkData;
            }
            return null;
        }
    }
}
