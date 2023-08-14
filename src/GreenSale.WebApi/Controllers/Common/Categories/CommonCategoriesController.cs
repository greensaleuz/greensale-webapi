using GreenSale.Application.Utils;
using GreenSale.Service.Interfaces.Categories;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Common.Categories;

[Route("api/common/categories")]
[ApiController]
public class CommonCategoriesController : ControllerBase
{
    private ICategoryService _service;
    private readonly int maxPage = 30;

    public CommonCategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPage)));

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetByIdAsync(long categoryId)
        => Ok(await _service.GetBYIdAsync(categoryId));
}
