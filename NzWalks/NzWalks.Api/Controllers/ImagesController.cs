using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NzWalks.Api.Dtos;
using NzWalks.Api.Models.Domain;
using NzWalks.Api.Repositories;

namespace NzWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDto request)
        {
            throw new Exception("a bhai");
            validateImage(request);
            if (ModelState.IsValid)
            {
                Image image = new Image()
                {
                    File = request.File,
                    FileSize = request.File.Length,
                    FileDescription = request.FileDescription,
                    FileName = request.FileName,
                    FileExtension = Path.GetExtension(request.File.FileName)
                };
                imageRepository.UploadImage(image);
                return Ok(image);
            }
            return BadRequest(ModelState);

        }
        private void validateImage(ImageUploadDto image)
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
