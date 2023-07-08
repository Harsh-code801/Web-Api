using Microsoft.EntityFrameworkCore;
using NzWalks.Api.Models.Domain;

namespace NzWalks.Api.Data
{
    public class NzWalksDbContext:DbContext
    {
        public NzWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Walks> walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties{ get; set; }
    }
}
