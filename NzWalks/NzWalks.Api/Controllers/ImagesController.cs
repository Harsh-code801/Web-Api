using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NzWalks.Api.Models.Domain;

namespace NzWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] Image image)
        {
            validateImage(image);
            if (ModelState.IsValid)
            {

            }
            return BadRequest(ModelState);

        }
        private void validateImage(Image image)
        {
            string[] supportedExtension = new string[] { ".png", ".tif", ".jpeg",".pdf" };

            if(string.IsNullOrWhiteSpace(image.File.FileName) && !supportedExtension.Contains(Path.GetExtension(image.File.FileName)))
            {
                ModelState.AddModelError("File", "File Formate Not Supported");
            }
            if(image.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File Not Exist Or Size Is Larger");
            }
        }
    }
}
