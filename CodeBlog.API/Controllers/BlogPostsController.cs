using CodeBlog.API.Models.Domain;
using CodeBlog.API.Models.DTO;
using CodeBlog.API.Repositories.Implementation;
using CodeBlog.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }

        // POST: {apibaseurl}/api/blogposts
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {
            // Convert DTO to Domain
            var blogPost = new BlogPost
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageURL = request.FeaturedImageURL,
                URLHandle = request.URLHandle,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                IsVisible = request.IsVisible,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);
                if (existingCategory != null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            blogPost = await blogPostRepository.CreateAsync(blogPost);

            // Convert Domain Model back to DTO
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageURL = blogPost.FeaturedImageURL,
                URLHandle = blogPost.URLHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    URLHandle = c.URLHandle
                }).ToList()

            };

            return Ok(response);
        }

        // GET: {apibaseurl}/api/blogposts
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogposts = await blogPostRepository.GetAllAsync();

            // Convert Domain Model to DTO
            var response = new List<BlogPostDto>();

            foreach (var blogPost in blogposts)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    ShortDescription = blogPost.ShortDescription,
                    Content = blogPost.Content,
                    FeaturedImageURL = blogPost.FeaturedImageURL,
                    URLHandle = blogPost.URLHandle,
                    PublishedDate = blogPost.PublishedDate,
                    Author = blogPost.Author,
                    IsVisible = blogPost.IsVisible,
                    Categories = blogPost.Categories.Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        URLHandle = c.URLHandle
                    }).ToList()
                });
            }

            return Ok(response);
        }

        // GET: {apibaseurl}/api/blogposts/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var blogPost = await blogPostRepository.GetByIdAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            // Convert Domain Model to DTO
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageURL = blogPost.FeaturedImageURL,
                URLHandle = blogPost.URLHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    URLHandle = c.URLHandle
                }).ToList()
            };
            return Ok(response);
        }

        // PUT: {apibaseurl}/api/blogposts/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBlogPostById([FromRoute] Guid id, UpdateBlogPostRequestDto request)
        {
            // Convert DTO to Domain
            var blogPost = new BlogPost
            {
                Id = id,
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageURL = request.FeaturedImageURL,
                URLHandle = request.URLHandle,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                IsVisible = request.IsVisible,
                Categories = new List<Category>()
            };

            // Foreach categoryGuid in request.Categories
            foreach (var categoryGuid in request.Categories)
            {
                var existing = await categoryRepository.GetById(categoryGuid);

                if (existing != null)
                {
                    blogPost.Categories.Add(existing);
                }
            }

            // Call Repository to update Domain Model
            var updatedBlogPost = await blogPostRepository.UpdateAsync(blogPost);

            if (updatedBlogPost == null)
            {
                return NotFound();
            }

            // Convert Domain Model back to DTO
            var response = new BlogPostDto
            {
                Id = updatedBlogPost.Id,
                Title = updatedBlogPost.Title,
                ShortDescription = updatedBlogPost.ShortDescription,
                Content = updatedBlogPost.Content,
                FeaturedImageURL = updatedBlogPost.FeaturedImageURL,
                URLHandle = updatedBlogPost.URLHandle,
                PublishedDate = updatedBlogPost.PublishedDate,
                Author = updatedBlogPost.Author,
                IsVisible = updatedBlogPost.IsVisible,
                Categories = updatedBlogPost.Categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    URLHandle = c.URLHandle
                }).ToList()
            };

            return Ok(response);
        }

        // DELETE: {apibaseurl}/api/blogposts/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBlogPostById([FromRoute] Guid id)
        {
            var deleted = await blogPostRepository.DeleteAsync(id);

            if (deleted == null)
            {
                return NotFound();
            }

            // Convert Domain Model back to DTO
            var response = new BlogPostDto
            {
                Id = deleted.Id,
                Title = deleted.Title,
                ShortDescription = deleted.ShortDescription,
                Content = deleted.Content,
                FeaturedImageURL = deleted.FeaturedImageURL,
                URLHandle = deleted.URLHandle,
                PublishedDate = deleted.PublishedDate,
                Author = deleted.Author,
                IsVisible = deleted.IsVisible
            };

            return Ok(response);
        }
    }
}
