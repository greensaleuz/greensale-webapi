using GreenSale.Persistence.Dtos.StoragDtos;
using GreenSale.Service.Interfaces.Storages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Client.StoragePosts
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientStoragePostStarController : BaseClientController
    {
        public readonly IStoragePostStarService _storagePostStarService;
        public ClientStoragePostStarController(IStoragePostStarService storagePostStarService)
        {
            this._storagePostStarService=storagePostStarService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(StorageStarCreateDto dto)
            => Ok(await _storagePostStarService.CreateAsync(dto));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(long postid, StorageStarUpdateDto dto)
            => Ok(await _storagePostStarService.UpdateAsync(postid, dto));
    }
}
