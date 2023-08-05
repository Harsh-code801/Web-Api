using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NzWalks.Api.Dtos
{
    public class ImageUploadDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
    public enum test
    {
        [Description("String 1")]
        A =0,
        [Description("String 2")]
        B =1,C=2,D=3,E=4,F=5
    }
}
