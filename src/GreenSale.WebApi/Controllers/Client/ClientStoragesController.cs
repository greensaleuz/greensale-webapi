using GreenSale.Persistence.Dtos.StoragDtos;
using GreenSale.Persistence.Validators.Storages;
using GreenSale.Service.Interfaces.Storages;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Client;

[Route("api/client/storages")]
[ApiController]
public class ClientStoragesController : ControllerBase
{
    private IStoragesService _service;
    private int maxPageSize = 30;

    public ClientStoragesController(IStoragesService service)
    {
        this._service = service;
    }
    [HttpPost("{storageId}")]

    public async Task<IActionResult> UpdateAsync([FromForm] StoragUpdateDto dto, long storageId)
    {
        var validator = new StorageUpdateValidator();
        var valid = await validator.ValidateAsync(dto);
        if (valid.IsValid)
        {
            return Ok(await _service.UpdateAsync(storageId, dto));
        }

        return BadRequest(valid.Errors);
    }

    [HttpDelete("storageId")]
    public async Task<IActionResult> DeleteAsync(long storageId)
        => Ok(await _service.DeleteAsync(storageId));
}
