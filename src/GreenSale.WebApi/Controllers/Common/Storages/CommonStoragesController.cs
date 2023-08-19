using GreenSale.Application.Utils;
using GreenSale.Service.Interfaces.Storages;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Common.Storages;

[Route("api/common/storage")]
[ApiController]
public class CommonStoragesController : BaseController
{
    private IStoragesService _service;
    private int maxPageSize = 30;

    public CommonStoragesController(IStoragesService service)
    {
        this._service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{storageId}")]
    public async Task<IActionResult> GetByIdAsync(long storageId)
        => Ok(await _service.GetBYIdAsync(storageId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());
}
