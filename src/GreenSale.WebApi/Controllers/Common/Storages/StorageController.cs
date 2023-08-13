using GreenSale.Application.Utils;
using GreenSale.Persistence.Dtos.StoragDtos;
using GreenSale.Persistence.Validators.Storages;
using GreenSale.Service.Interfaces.Storages;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Common.Storages
{
    [Route("api/storage")]
    [ApiController]
    public class StorageController : BaseController
    {
        private IStoragesService _service;
        private int maxPageSize = 30;

        public StorageController(IStoragesService service)
        {
            this._service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] StoragCreateDto dto)
        {
            var validator = new StorageCreateValidator();
            var valid = await validator.ValidateAsync(dto);

            if (valid.IsValid)
            {
                return Ok(await _service.CreateAsync(dto));
            }

            return BadRequest(valid.Errors);
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
}
