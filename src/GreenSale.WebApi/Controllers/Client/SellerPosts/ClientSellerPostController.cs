﻿using FluentValidation;
using GreenSale.Persistence.Dtos.SellerPostImageUpdateDtos;
using GreenSale.Persistence.Dtos.SellerPostsDtos;
using GreenSale.Persistence.Validators;
using GreenSale.Persistence.Validators.BuyerPosts;
using GreenSale.Persistence.Validators.SellerPostValidators;
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
        var validator = new SellerPostCreatedValidators();
        var isValidator = validator.Validate(dto);

        if (isValidator.IsValid)
        {
            return Ok(await _postService.CreateAsync(dto));
        }

        return BadRequest(isValidator.Errors);
    }

    [HttpPut("{postId}")]
    public async Task<IActionResult> UpdateAsync(long postId, [FromForm] SellerPostUpdateDto dto)
    {
        var validator = new SellerPostUpatedValidators();
        var isValidator = validator.Validate(dto);

        if (isValidator.IsValid)
        {
            return Ok(await _postService.UpdateAsync(postId, dto));
        }

        return BadRequest(isValidator.Errors);
    }

    [HttpPut("image/{imageId}")]
    public async Task<IActionResult> ImageUpdateAsync(long imageId, [FromForm] SellerPostImageUpdateDto dto)
    {
        var validator = new SellerImageValidator();
        var isValidator = validator.Validate(dto);
        if (isValidator.IsValid)
        {
            var result = await _postService.ImageUpdateAsync(imageId, dto);

            return Ok(result);
        }
        return BadRequest(isValidator.Errors);

    }

    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeleteAsync(long postId)
        => Ok(await _postService.DeleteAsync(postId));
}
