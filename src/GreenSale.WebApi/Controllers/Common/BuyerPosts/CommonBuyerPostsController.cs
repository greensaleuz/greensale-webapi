using GreenSale.Application.Utils;
using GreenSale.Service.Interfaces.BuyerPosts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Common.BuyerPosts
{
    [Route("api/common/buyerposts")]
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

        [HttpGet("{buyerpostid}")]
        public async Task<IActionResult> GetByIdAsync(long buyerpostid)
            => Ok(await _service.GetBYIdAsync(buyerpostid));

        [HttpGet("count")]
        public async Task<IActionResult> CountAsync()
            => Ok(await _service.CountAsync());
    }
}
