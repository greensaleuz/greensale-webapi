using GreenSale.Application.Utils;
using GreenSale.Service.Interfaces.BuyerPosts;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Common.BuyerPosts
{
    [Route("api/common/buyer/posts")]
    [ApiController]
    public class CommonBuyerPostsController : BaseController
    {
        private IBuyerPostService _service;
        private readonly int maxPage = 30;
        public CommonBuyerPostsController(IBuyerPostService service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPage)));

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetByIdAsync(long postId)
            => Ok(await _service.GetBYIdAsync(postId));

        [HttpGet("count")]
        public async Task<IActionResult> CountAsync()
            => Ok(await _service.CountAsync());
    }
}
