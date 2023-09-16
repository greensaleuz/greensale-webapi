using GreenSale.Persistence.Dtos.StoragDtos;
using GreenSale.Service.Interfaces.Storages;
using GreenSale.WebApi.Controllers.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Admin.Storages
{
    [Route("api/admin/storage/post/star")]
    [ApiController]
    public class AdminStoragePostStarController : AdminBaseController
    {
        public readonly IStoragePostStarService _storagePostStarService;
        public AdminStoragePostStarController(IStoragePostStarService storagePostStarService)
        {
            this._storagePostStarService = storagePostStarService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] StorageStarCreateDto dto)
            => Ok(await _storagePostStarService.CreateAsync(dto));

        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdateAsync([FromForm] long postid, [FromForm] StorageStarUpdateDto dto)
            => Ok(await _storagePostStarService.UpdateAsync(postid, dto));
    }
}
