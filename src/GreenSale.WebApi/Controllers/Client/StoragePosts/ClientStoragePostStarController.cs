using GreenSale.Persistence.Dtos.StoragDtos;
using GreenSale.Service.Interfaces.Storages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Client.StoragePosts
{
    [Route("api/client/storage/post/star")]
    [ApiController]
    public class ClientStoragePostStarController : BaseClientController
    {
        public readonly IStoragePostStarService _storagePostStarService;
        public ClientStoragePostStarController(IStoragePostStarService storagePostStarService)
        {
            this._storagePostStarService=storagePostStarService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] StorageStarCreateDto dto)
            => Ok(await _storagePostStarService.CreateAsync(dto));

        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdateAsync([FromForm] long postid, [FromForm] StorageStarUpdateDto dto)
            => Ok(await _storagePostStarService.UpdateAsync(postid, dto));
    }
}
