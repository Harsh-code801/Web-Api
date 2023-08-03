using NzWalks.Api.Data;
using NzWalks.Api.Models.Domain;

namespace NzWalks.Api.Repositories
{
    public class ImageUpload : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NzWalksDbContext nzWalksDbContext;

        public ImageUpload(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,NzWalksDbContext nzWalksDbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.nzWalksDbContext = nzWalksDbContext;
        }
        public async Task<Image> UploadImage(Image image)
        {
            var UploadPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", image.FileName + image.FileExtension);

            using (Stream stream = new FileStream(UploadPath, FileMode.Create))
            {
                await image.File.CopyToAsync(stream);
            }

            var webAccessPath = $"{httpContextAccessor.HttpContext.Request.Scheme}:/{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = webAccessPath;
           // image.Id = Guid.NewGuid();
            try
            {
                await nzWalksDbContext.Images.AddAsync(image);
                await nzWalksDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                
            }
            return image;
        }
    }
}
