using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace NzWalks.Api.Dtos
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string User { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
