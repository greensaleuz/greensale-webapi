using GreenSale.Persistence.Dtos.SellerPostsDtos;
using GreenSale.Service.Interfaces.SellerPosts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenSale.WebApi.Controllers.Client.SellerPosts
{
    [Route("api/client/seller/post/star")]
    [ApiController]
    public class ClientSellerPostStarController : BaseClientController
    {
        private readonly ISellerPostStarService _sellerPostStarService;
        public ClientSellerPostStarController(ISellerPostStarService sellerPostStarService)
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
