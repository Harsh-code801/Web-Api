using NzWalks.Api.Models.Domain;

namespace NzWalks.Api.Dtos
{
    public class WalksDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
