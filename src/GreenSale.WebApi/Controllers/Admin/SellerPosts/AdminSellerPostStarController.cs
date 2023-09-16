using GreenSale.Persistence.Dtos.SellerPostsDtos;
using GreenSale.Service.Interfaces.SellerPosts;
using GreenSale.WebApi.Controllers.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Admin.SellerPosts
{
    [Route("api/admin/seller/post/star")]
    [ApiController]
    public class AdminSellerPostStarController : AdminBaseController
    {
        private readonly ISellerPostStarService _sellerPostStarService;
        public AdminSellerPostStarController(ISellerPostStarService sellerPostStarService)
        {
            _sellerPostStarService = sellerPostStarService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] SellerPostStarCreateDto dto)
            => Ok(await _sellerPostStarService.CreateAsync(dto));

        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdateAsync([FromForm] long postid, [FromForm] SellerPostStarUpdateDto dto)
            => Ok(await _sellerPostStarService.UpdateAsync(postid, dto));
    }
}
