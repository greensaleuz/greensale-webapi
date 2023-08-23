using GreenSale.Application.Utils;
using GreenSale.Service.Interfaces.SellerPosts;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Common.SellerPosts;

[Route("api/common/seller/post")]
[ApiController]
public class CommonSellerPostController : BaseController
{
    private readonly ISellerPostService _postService;
    private int maxpage = 30;

    public CommonSellerPostController(ISellerPostService postService)
    {
        this._postService = postService;
    }

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _postService.CountAsync());

    [HttpGet]
    public async Task<IActionResult> GetAllasync([FromQuery] int page = 1)
        => Ok(await _postService.GetAllAsync(new PaginationParams(page, maxpage)));

    [HttpGet("all/{userId}")]
    public async Task<IActionResult> GetAllByIdAsync(long userId, [FromQuery] int page = 1)
        => Ok(await _postService.GetAllByIdAsync(userId, new PaginationParams(page, maxpage)));

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetByIdAsync(long postId)
        => Ok(await _postService.GetBYIdAsync(postId));
}
