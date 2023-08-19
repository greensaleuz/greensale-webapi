using GreenSale.Persistence.Dtos.SellerPostImageUpdateDtos;
using GreenSale.Persistence.Dtos.SellerPostsDtos;
using GreenSale.Service.Interfaces.SellerPosts;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Client.SellerPosts;

[Route("api/client/seller/post")]
[ApiController]
public class ClientSellerPostController : BaseClientController
{
    private readonly ISellerPostService _postService;
    private int maxpage = 30;

    public ClientSellerPostController(ISellerPostService postService)
    {
        this._postService = postService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] SellerPostCreateDto dto)
    {
        var result = await _postService.CreateAsync(dto);

        return Ok(result);
    }

    [HttpPut("{postId}")]
    public async Task<IActionResult> UpdateAsync(long postId, [FromForm] SellerPostUpdateDto dto)
    {
        var result = await _postService.UpdateAsync(postId, dto);

        return Ok(result);
    }

    [HttpPut("image")]
    public async Task<IActionResult> ImageUpdateAsync([FromForm] SellerPostImageUpdateDto dto)
    {
        var result = await _postService.ImageUpdateAsync(dto);

        return Ok(result);
    }

    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeleteAsync(long postId)
        => Ok(await _postService.DeleteAsync(postId));
}
