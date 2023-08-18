using GreenSale.Persistence.Dtos.BuyerPostImageUpdateDtos;
using GreenSale.Persistence.Dtos.BuyerPostsDto;
using GreenSale.Persistence.Dtos.SellerPostImageUpdateDtos;
using GreenSale.Persistence.Validators.BuyerPosts;
using GreenSale.Service.Interfaces.BuyerPosts;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Client.BuyerPosts;

[Route("api/client/buyer/post")]
[ApiController]
public class ClientBuyerPostsController : BaseClientController
{
    private IBuyerPostService _service;
    public readonly int maxPage = 30;

    public ClientBuyerPostsController(IBuyerPostService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] BuyerPostCreateDto dto)
    {
        var validator = new BuyerPostCreateValidator();
        var isValidator = validator.Validate(dto);

        if (isValidator.IsValid)
        {
            return Ok(await _service.CreateAsync(dto));
        }

        return BadRequest(isValidator.Errors);
    }

    [HttpPut("{postId}")]
    public async Task<IActionResult> UpdateAsync([FromForm] BuyerPostUpdateDto dto, long postId)
    {
        var validator = new BuyerPostUpdateValidator();
        var isValidator = validator.Validate(dto);
        if (isValidator.IsValid)
        {
            return Ok(await _service.UpdateAsync(postId, dto));
        }

        return BadRequest(isValidator.Errors);
    }

    [HttpPut("image")]
    public async Task<IActionResult> ImageUpdateAsync([FromForm] BuyerPostImageDto dto)
    {
        var result = await _service.ImageUpdateAsync(dto);

        return Ok(result);
    }

    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeleteAsync(long postId)
        => Ok(await _service.DeleteAsync(postId));
}

