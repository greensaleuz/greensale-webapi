using GreenSale.Application.Utils;
using GreenSale.Persistence.Dtos.CategoryDtos;
using GreenSale.Persistence.Validators.Categories;
using GreenSale.Service.Interfaces.Categories;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Admin.Categories
{
    [Route("api/admin/categories")]
    [ApiController]
    public class AdminCategoriesController : AdminBaseController
    {
        private ICategoryService _service;
        private readonly int maxPage = 30;

        public AdminCategoriesController(ICategoryService service)
        {
            _service = service;
        }


        [HttpGet("count")]
        public async Task<IActionResult> CountAsync()
            => Ok(await _service.CountAsync());

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteAsync(long categoryId)
            => Ok(await _service.DeleteAsync(categoryId));

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CategoryCreateDto dto)
        {
            CategoryCreateValidator validations = new CategoryCreateValidator();
            var result = validations.Validate(dto);

            if (result.IsValid)
            {
                return Ok(await _service.CreateAsync(dto));
            }

            return BadRequest(result.Errors);
        }

        [HttpPatch("{categoryId}")]
        public async Task<IActionResult> UpdateAsync(long categoryId, [FromForm] CategoryCreateDto dto)
        {
            CategoryCreateValidator update = new CategoryCreateValidator();
            var validationResult = update.Validate(dto);
            if (validationResult.IsValid)
            {
                return Ok(await _service.UpdateAsync(categoryId, dto));
            }
            return BadRequest(validationResult.Errors);
        }
    }
}
