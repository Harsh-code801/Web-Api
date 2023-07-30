using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NzWalks.Api.Data
{
    public class NzWalksAuthDbContext:IdentityDbContext
    {
        public NzWalksAuthDbContext(DbContextOptions<NzWalksAuthDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoalId = "98465f8b-7276-4642-b083-eaf720938234";
            var writerRoalId = "e4823352-c2d9-4d4c-afd6-8c135873a383";

            var auth = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = readerRoalId,
                    ConcurrencyStamp = readerRoalId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                },
                new IdentityRole()
                {
                    Id = writerRoalId,
                    ConcurrencyStamp = writerRoalId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(auth);
        }
    }
}
