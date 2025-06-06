﻿using CodeBlog.API.Models.Domain;
using CodeBlog.API.Models.DTO;
using CodeBlog.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace CodeBlog.API.Controllers
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

        // GET: {apibaseurl}/api/images
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            // Call image repository to get all images
            var images = await imageRepository.GetAllImages();

            // Convert Domain Models to DTOs
            var response = new List<BlogImageDto>();
            foreach (var image in images)
            {
                response.Add(new BlogImageDto
                {
                    Id = image.Id,
                    FileName = image.FileName,
                    FileExtension = image.FileExtension,
                    Title = image.Title,
                    DateCreated = image.DateCreated,
                    Url = image.Url
                });
            }

            return Ok(response);
        }

        // POST: {apibaseurl}/api/images
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file,[FromForm] string fileName,[FromForm] string title)
        {
            ValidateFileUpload(file);

            if (ModelState.IsValid)
            {
                // File Upload
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now
                };

                blogImage = await imageRepository.Upload(file, blogImage);

                // Convert Domain Model to DTO
                var response = new BlogImageDto
                {
                    Id = blogImage.Id,
                    FileName = blogImage.FileName,
                    FileExtension = blogImage.FileExtension,
                    Title = blogImage.Title,
                    DateCreated = blogImage.DateCreated,
                    Url = blogImage.Url
                };

                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Invalid file type. Only .jpg, .jpeg, and .png files are allowed.");
            }
            if (file.Length > 10485760) // 10 MB limit
            {
                ModelState.AddModelError("file", "File size exceeds the maximum limit of 10 MB.");
            }
        }

    }
}
