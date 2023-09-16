using GreenSale.Persistence.Dtos.BuyerPostsDto;
using GreenSale.Service.Interfaces.BuyerPosts;
using GreenSale.WebApi.Controllers.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Admin.BuyerPosts
{
    [Route("api/admin/buyer/star")]
    [ApiController]
    public class AdminBuyerPostStarController : AdminBaseController
    {
        private readonly IBuyerPostStarService _buyerPostStarService;
        public AdminBuyerPostStarController(IBuyerPostStarService buyerPostStarService)
        {
            _buyerPostStarService = buyerPostStarService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] BuyerPostStarCreateDto dto)
            => Ok(await _buyerPostStarService.CreateAsync(dto));

        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdateAsync([FromForm] long postId, [FromForm] BuyerPostStarUpdateDto dto)
            => Ok(await _buyerPostStarService.UpdateAsync(postId, dto));
    }
}
