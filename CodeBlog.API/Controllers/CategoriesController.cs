using CodeBlog.API.Data;
using CodeBlog.API.Models.Domain;
using CodeBlog.API.Models.DTO;
using CodeBlog.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlog.API.Controllers
{
    // https://localhost:xxxx/api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        // GET: api/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            // Map DTO to Domain Model
            var category = new Category
            {
                Name = request.Name,
                URLHandle = request.URLHandle
            };

            await categoryRepository.CreateAsync(category);

            // Domain model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                URLHandle = category.URLHandle
            };

            return Ok(response);
        }
    }
}
