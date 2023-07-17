using System.ComponentModel.DataAnnotations;

namespace NzWalks.Api.Dtos
{
    public class WalksInput
    {
        [Required]
        [MaxLength(20,ErrorMessage = "Name length should be smaller then 20")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Description length should be smaller then 100")]
        public string Description { get; set; }
        [Required]
        public string LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
