using CodeBlog.API.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        // POST: {apibaseurl}/api/images
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] string title)
        {
            ValidateFileUpload(file);
            if (ModelState.IsValid)
            {
                // File upload
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.UtcNow
                };
            }
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("File", "Invalid file type. Allowed types are: .jpg, .jpeg, .png");
            }

            if (file.Length > 10485760) // 10 MB limit
            {
                ModelState.AddModelError("File", "File size exceeds the maximum limit of 10 MB.");
            }
        }
    }
}
