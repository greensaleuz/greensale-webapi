using GreenSale.Persistence.Dtos.BuyerPostsDto;
using GreenSale.Service.Interfaces.BuyerPosts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Client.BuyerPosts
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientBuyerPostStarController : BaseClientController
    {
        private readonly IBuyerPostStarService _buyerPostStarService;
        public ClientBuyerPostStarController(IBuyerPostStarService buyerPostStarService)
        {
            _buyerPostStarService = buyerPostStarService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BuyerPostStarCreateDto dto)
            => Ok(await _buyerPostStarService.CreateAsync(dto));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(long postId, BuyerPostStarUpdateDto dto)
            => Ok(await _buyerPostStarService.UpdateAsync(postId, dto));

        // [HttpGet]
    }
}
