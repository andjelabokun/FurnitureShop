using Microsoft.AspNetCore.Mvc;

namespace SalonNamestajaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadsController : ControllerBase
    {
        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Slika nije poslata.");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                return BadRequest("Dozvoljene su samo slike: jpg, jpeg, png, webp.");

            var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (!Directory.Exists(imagesFolder))
                Directory.CreateDirectory(imagesFolder);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(imagesFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{fileName}";

            return Ok(new { slikaUrl = imageUrl });
        }
    }
}
